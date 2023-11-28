using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;

namespace NykreditScraper
{
    class Program
    {
        static void Main(string[] args)
        {
            var httpClient = new HttpClient();
            var exchangeList = httpClient.GetStringAsync("http://www.nykredit.dk/api/kurslister/udbetaling/privat/").Result;

            var flexProducts = new[]
            {
                new Product(ProductType.Flex, 3, 0.09m, 100m),
                new Product(ProductType.Flex, 5, 0.31m, 100m),
            };

            var products = new ProductParser()
                .Parse(exchangeList)
                .Union(flexProducts)
                .Where(p => p.InterestRate > 0.0m)
                .Where(p => p.ExchangeRate <= 100m)
                .OrderBy(p => p.ProductType)
                .ThenByDescending(p => p.Period);

            var result = httpClient.GetAsync("http://configurationbutler-externalapi.azurewebsites.net/v0/loandata/201608221134/companies").Result; // Needs to be provided via configuration
            var allCompanies = JArray.Parse(result.Content.ReadAsStringAsync().Result);
            var companyId = 0;
            foreach (var company in allCompanies)
            {
                var id = (int)company["id"];
                var name = (string)company["name"];
                if (name.Equals("Nykredit", StringComparison.OrdinalIgnoreCase))
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
                if (new[] { ProductType.FixedRate, ProductType.FixedRateInterestOnly }.Contains(product.ProductType))
                {
                    jsonProducts.Add(
                        new
                        {
                            id = productId++,
                            name = TranslateProductType(product, productTypes.ToDictionary(t => (string)t["type"], t => (string)t["name"])),
                            interestRate = product.InterestRate,
                            period = product.Period,
                            exchangeRate = product.ExchangeRate,
                            contributionRateStairCaseId =
                                TranslateContributionRateStairCaseId(product,
                                    contributionRateStairCases.SelectMany(
                                        c => c["type"].Select(t => new Tuple<string, int>((string)t, (int)c["id"])))
                                        .ToDictionary(c => c.Item1, c => c.Item2))
                        });
                }
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
                                contributionRateStairCaseId =
                                    TranslateContributionRateStairCaseId(product,
                                        contributionRateStairCases.SelectMany(
                                            c =>
                                                c["type"].Select(t => new Tuple<string, int>((string)t, (int)c["id"])))
                                            .ToDictionary(c => c.Item1, c => c.Item2))
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
