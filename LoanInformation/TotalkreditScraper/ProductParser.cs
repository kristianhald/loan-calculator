using HtmlAgilityPack;
using Shared;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace TotalkreditScraper
{
    public class ProductParser
    {
        public IEnumerable<Product> Parse(string exchangeData)
        {
            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(exchangeData);
            var productTables = htmlDocument.DocumentNode.SelectNodes("//table[@class='printtable']");

            var fixedRateProducts = ParseFixedRateProducts(productTables.ElementAt(0));
            var fixedRateInterestOnlyProducts = ParseFixedRateInterestOnlyProducts(productTables.ElementAt(1));
            var flexProducts = ParseFlexProducts(productTables.ElementAt(2));
            var fshortProducts = ParseFShortProducts(productTables.ElementAt(3));

            return fixedRateProducts
                .Union(fixedRateInterestOnlyProducts)
                .Union(flexProducts)
                .Union(fshortProducts)
                .ToList();
        }

        private IEnumerable<Product> ParseFixedRateProducts(HtmlNode fixedRateTable)
        {
            var products = fixedRateTable.SelectNodes("./tr");
            foreach (var product in products)
            {
                yield return new Product(
                    ProductType.FixedRate,
                    Int32.Parse(product.ChildNodes[1].InnerText.Replace(" år", ""), new CultureInfo("da-dk")),
                    Decimal.Parse(product.ChildNodes[3].InnerText.Replace(" %", ""), new CultureInfo("da-dk")),
                    Decimal.Parse(product.ChildNodes[9].InnerText, new CultureInfo("da-dk")));
            }
        }

        private IEnumerable<Product> ParseFixedRateInterestOnlyProducts(HtmlNode fixedRateInterestOnlyTable)
        {
            var products = fixedRateInterestOnlyTable.SelectNodes("./tr");
            foreach (var product in products)
            {
                yield return new Product(
                    ProductType.FixedRateInterestOnly,
                    Int32.Parse(product.ChildNodes[1].InnerText.Replace(" år", ""), new CultureInfo("da-dk")),
                    Decimal.Parse(product.ChildNodes[3].InnerText.Replace(" %", ""), new CultureInfo("da-dk")),
                    Decimal.Parse(product.ChildNodes[9].InnerText, new CultureInfo("da-dk")));
            }
        }

        private IEnumerable<Product> ParseFlexProducts(HtmlNode flexTable)
        {
            var products = flexTable.SelectNodes("./tr");
            foreach (var product in products.Where(p => p.ChildNodes[1].InnerText.Contains(". år i januar")))
            {
                yield return new Product(
                    ProductType.Flex,
                    Int32.Parse(product.ChildNodes[1].InnerText.Replace("Hvert ", "").Replace(". år i januar", ""), new CultureInfo("da-dk")),
                    Decimal.Parse(product.ChildNodes[3].InnerText.Replace(" %", ""), new CultureInfo("da-dk")),
                    Decimal.Parse(product.ChildNodes[9].InnerText, new CultureInfo("da-dk")));
            }
        }

        private IEnumerable<Product> ParseFShortProducts(HtmlNode fshortTable)
        {
            var products = fshortTable.SelectNodes("./tr");
            foreach (var product in products)
            {
                yield return new Product(
                    ProductType.FShort,
                    Int32.Parse(product.ChildNodes[3].InnerText.Replace(" år", ""), new CultureInfo("da-dk")),
                    Decimal.Parse(product.ChildNodes[5].InnerText.Replace(" %", ""), new CultureInfo("da-dk")),
                    Decimal.Parse(product.ChildNodes[9].InnerText, new CultureInfo("da-dk")));
            }
        }
    }
}
