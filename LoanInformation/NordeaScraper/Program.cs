using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;

namespace NordeaScraper
{
    class Program
    {
        private static readonly FixedProductParser FixedProductParser = new FixedProductParser();
        private static readonly FlexProductParser FlexProductParser = new FlexProductParser();
        private static readonly ShortProductParser ShortProductParser = new ShortProductParser();
        private static readonly IEnumerable<int> SupportedPeriods = new[] { 1, 3, 5 };

        static void Main(string[] args)
        {
            var httpClient = new HttpClient();
            var fixedRateExchangeList = httpClient.GetStringAsync("http://www.nordea.dk/wemapp/api/credit/fixedrate/bonds.json").Result;
            var flexRateExchangeList = httpClient.GetStringAsync("http://www.nordea.dk/privat/lan/bolig/historiske-refinansieringsrenter.html").Result;
            var shortRateExchangeLists = new[]
            {
                httpClient.GetStringAsync("https://www.nordea.dk/wemapp/api/loanguide/loanguide.json?loantype=OBLC&permanentResidence=true&loanDuration=20&installmentfree=0&amount=3000000&countyId=998&retailOrCorporate=Retail").Result,
                httpClient.GetStringAsync("https://www.nordea.dk/wemapp/api/loanguide/loanguide.json?loantype=OBLCA&permanentResidence=true&loanDuration=20&installmentfree=0&amount=3000000&countyId=998&retailOrCorporate=Retail").Result,
            };

            var shortProducts = shortRateExchangeLists
                .Select(ShortProductParser.Parse)
                .SelectMany(p => p)
                .ToList();

            var flexProducts = FlexProductParser
                .Parse(flexRateExchangeList)
                .Where(p => SupportedPeriods.Contains(p.Period))
                .ToList();

            var fixedProducts = FixedProductParser
                .Parse(fixedRateExchangeList)
                .Where(p => p.ExchangeRate <= 100m)
                .ToList();

            var products = fixedProducts
                .Union(flexProducts)
                .Union(shortProducts)
                .OrderBy(p => p.ProductType)
                .ThenByDescending(p => p.Period);

            var result = httpClient.GetAsync("http://configurationbutler-externalapi.azurewebsites.net/v0/loandata/201608221134/").Result; // Needs to be provided via configuration
            var allCompanies = JArray.Parse(result.Content.ReadAsStringAsync().Result);
            var companyId = 0;
            foreach (var company in allCompanies)
            {
                var id = (int)company["id"];
                var name = (string)company["name"];
                if (name.Equals("Nordea Kredit", StringComparison.OrdinalIgnoreCase))
                    companyId = id;
            }

            var currentCompanyProductTypes = httpClient.GetStringAsync($"http://configurationbutler-externalapi.azurewebsites.net/v0/loandata/201608221134/producttypes").Result;
            var productTypes = JArray.Parse(currentCompanyProductTypes); // Needs to be provided via configuration and via routing metadata

            var currentContributionRateStairCase = httpClient.GetStringAsync($"http://configurationbutler-externalapi.azurewebsites.net/v0/loandata/201608221134/contributionratestaircase/{companyId}").Result;
            var contributionRateStairCases = JArray.Parse(currentContributionRateStairCase); // Needs to be provided via configuration and via routing metadata);

            var productId = 1;
            var jsonProducts = new List<dynamic>();
            foreach (var product in products)
            {
                /*
                    "Id": 1,
                    "Name": "Fastrente",
                    "InterestRate": 1.234567,
                    "Period": 30,
                    "ExchangeRate": 101.987,
                    "ContributionRateStairCaseId": 1
                */

                if (new[] { ProductType.FixedRate, ProductType.FixedRateInterestOnly }.Contains(product.ProductType))
                    jsonProducts.Add(
                        new
                        {
                            id = productId++,
                            name = TranslateProductType(product, productTypes.ToDictionary(t => (string)t["type"], t => (string)t["name"])),
                            interestRate = product.InterestRate,
                            period = product.Period,
                            exchangeRate = product.ExchangeRate,
                            contributionRateStairCaseId = TranslateContributionRateStairCaseId(product, contributionRateStairCases.SelectMany(c => c["type"].Select(t => new Tuple<string, int>((string)t, (int)c["id"]))).ToDictionary(c => c.Item1, c => c.Item2))
                        });
                else
                {
                    foreach (var period in new[] { 30, 20, 15, 10 })
                    {
                        jsonProducts.Add(
                            new
                            {
                                id = productId++,
                                name = TranslateProductType(product, productTypes.ToDictionary(t => (string)t["type"], t => (string)t["name"])),
                                interestRate = product.InterestRate,
                                period = period,
                                exchangeRate = 100m,
                                contributionRateStairCaseId = TranslateContributionRateStairCaseId(product, contributionRateStairCases.SelectMany(c => c["type"].Select(t => new Tuple<string, int>((string)t, (int)c["id"]))).ToDictionary(c => c.Item1, c => c.Item2))
                            });
                    }
                }
            }

            var serializedProducts = JsonConvert.SerializeObject(jsonProducts, Formatting.Indented);
            Console.WriteLine(serializedProducts);

            httpClient.PutAsync(
                $"http://configurationbutler-externalapi.azurewebsites.net/v0/loandata/201608221134/{companyId}/products", // Needs to be provided via configuration and via routing metadata
                new StringContent(serializedProducts, Encoding.UTF8, "application/json")).Wait();
        }

        private static string TranslateProductType(Product product, Dictionary<string, string> productTypes)
        {
            var productType = Enum.GetName(typeof(ProductType), product.ProductType);
            if (productTypes.ContainsKey(productType))
                return productTypes[productType];

            return productTypes
                .Single(p => p.Key.StartsWith(productType) && p.Key.EndsWith("x" + product.Period.ToString()))
                .Value;
        }

        private static int TranslateContributionRateStairCaseId(Product product, Dictionary<string, int> contributionRateStairCases)
        {
            var productType = Enum.GetName(typeof(ProductType), product.ProductType);
            if (contributionRateStairCases.ContainsKey(productType))
                return contributionRateStairCases[productType];

            return contributionRateStairCases
                .Single(p => p.Key.StartsWith(productType) && p.Key.EndsWith("x" + product.Period.ToString()))
                .Value;
        }
    }
}
