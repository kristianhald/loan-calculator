using HtmlAgilityPack;
using Shared;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace BrfKreditScraper
{
    public class ProductParser
    {
        public IEnumerable<Product> Parse(string exchangeData)
        {
            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(exchangeData);
            var productTables = htmlDocument.DocumentNode.SelectNodes("//table[@class='table table--numeric table--text is-responsive-v2']");

            var fixedRateProducts = ParseFixedRateProducts(productTables.ElementAt(0));
            var fshortProducts = ParseFShortProducts(productTables.ElementAt(1));
            var flexProducts = ParseFlexProducts(productTables.ElementAt(2));

            return fixedRateProducts
                .Union(fshortProducts)
                .Union(flexProducts)
                .ToList();
        }

        private IEnumerable<Product> ParseFixedRateProducts(HtmlNode fixedRateTable)
        {
            var products = fixedRateTable.SelectNodes("./tbody/tr");
            foreach (var product in products)
            {
                var productTypeText = product.SelectSingleNode("./td/span").InnerText;

                yield return new Product(
                    productTypeText.Contains("afdragsfri") ? ProductType.FixedRateInterestOnly : ProductType.FixedRate,
                    Int32.Parse(productTypeText.Split(new[] { "-" }, StringSplitOptions.RemoveEmptyEntries)[0].Replace("år", "").Trim(), new CultureInfo("da-dk")),
                    Decimal.Parse(productTypeText.Split(new[] { "-" }, StringSplitOptions.RemoveEmptyEntries)[1].Replace("%", "").Trim(), new CultureInfo("da-dk")),
                    Decimal.Parse(product.ChildNodes[3].InnerText.Trim(), new CultureInfo("da-dk")));
            }
        }

        private IEnumerable<Product> ParseFlexProducts(HtmlNode flexTable)
        {
            var products = flexTable.SelectNodes("./tbody/tr");
            foreach (var product in products)
            {
                yield return new Product(
                    ProductType.Flex,
                    Int32.Parse(product.ChildNodes[1].InnerText.Replace("årig", "").Trim(), new CultureInfo("da-dk")),
                    Decimal.Parse(product.ChildNodes[3].InnerText.Trim(), new CultureInfo("da-dk")),
                    Decimal.Parse(product.ChildNodes[5].InnerText.Trim(), new CultureInfo("da-dk")));
            }
        }

        private IEnumerable<Product> ParseFShortProducts(HtmlNode fshortTable)
        {
            var products = fshortTable.SelectNodes("./tbody/tr");
            foreach (var product in products)
            {
                var productTypeText = product.SelectSingleNode("./td/span").InnerText;

                yield return new Product(
                    ProductType.FShort,
                    Int32.Parse(productTypeText.Split(new[] { "-" }, StringSplitOptions.RemoveEmptyEntries)[0].Replace("år", "").Trim(), new CultureInfo("da-dk")),
                    Decimal.Parse(productTypeText.Substring(productTypeText.IndexOf('-') + 1).Replace("%", "").Trim(), new CultureInfo("da-dk")),
                    Decimal.Parse(product.ChildNodes[3].InnerText.Trim(), new CultureInfo("da-dk")));
            }
        }
    }
}
