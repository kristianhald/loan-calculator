using HtmlAgilityPack;
using Shared;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace RealkreditDanmarkScraper
{
    public class ProductParser
    {
        public IEnumerable<Product> Parse(string exchangeData)
        {
            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(exchangeData);
            var productTables = htmlDocument.DocumentNode.SelectNodes("//table[@id='ctl00_m_g_9b8515b8_2d37_490f_b66a_07b62fae35b3_tlbRenteOgKurser']//table");

            var fixedRateProducts = ParseFixedRateProducts(productTables.ElementAt(0));
            var flexProducts = ParseFlexProducts(productTables.ElementAt(1));
            var fshortProducts = ParseFShortProducts(productTables.ElementAt(2));

            return fixedRateProducts
                .Union(flexProducts)
                .Union(fshortProducts)
                .ToList();
        }

        private IEnumerable<Product> ParseFixedRateProducts(HtmlNode fixedRateTable)
        {
            var products = fixedRateTable.SelectNodes("./tr").Skip(1);
            foreach (var product in products)
            {
                var nodes = product.SelectNodes("./td");
                if (!nodes[0].ChildNodes.Any())
                    continue;
                var valuta = nodes[1].InnerText;
                if (valuta.Contains("EUR"))
                    continue;
                var productTypeText = nodes[0].ChildNodes[0].InnerText;

                yield return new Product(
                    productTypeText.Contains("afdr") ? ProductType.FixedRateInterestOnly : ProductType.FixedRate,
                    Int32.Parse(productTypeText.Split(new[] { "obl." }, StringSplitOptions.RemoveEmptyEntries)[1].Replace("u.afdr.", "").Replace("&#229;r", "").Trim(), new CultureInfo("da-dk")),
                    Decimal.Parse(productTypeText.Split(new[] { "obl." }, StringSplitOptions.RemoveEmptyEntries)[0].Replace("u.afdr.", "").Replace("%", "").Trim(), new CultureInfo("da-dk")),
                    Decimal.Parse(nodes[3].ChildNodes[0].InnerText.Trim(), new CultureInfo("da-dk")));
            }
        }

        private IEnumerable<Product> ParseFlexProducts(HtmlNode flexTable)
        {
            var products = flexTable.SelectNodes("./tr").Skip(1);
            foreach (var product in products)
            {
                var nodes = product.SelectNodes("./td");
                if (!nodes[0].ChildNodes.Any())
                    continue;
                var productTypeText = nodes[0].ChildNodes[0].InnerText;
                if (productTypeText.Contains("u.afdr."))
                    continue;
                var valuta = nodes[1].InnerText;
                if (valuta.Contains("EUR"))
                    continue;

                yield return new Product(
                    ProductType.Flex,
                    Int32.Parse(productTypeText.Replace("FlexL&#229;n&#174; F", "").Replace("K                         30 &#229;r", "").Trim(), new CultureInfo("da-dk")),
                    Decimal.Parse(nodes[2].ChildNodes[0].InnerText.Trim(), new CultureInfo("da-dk")),
                    Decimal.Parse(nodes[3].ChildNodes[0].InnerText.Trim(), new CultureInfo("da-dk")));
            }
        }

        private IEnumerable<Product> ParseFShortProducts(HtmlNode fshortTable)
        {
            var products = fshortTable.SelectNodes("./tr").Skip(1);
            foreach (var product in products)
            {
                var nodes = product.SelectNodes("./td");
                if (!nodes[0].ChildNodes.Any())
                    continue;
                var productTypeText = nodes[0].ChildNodes[0].InnerText;
                if (productTypeText.Contains("u.afdr."))
                    continue;
                var valuta = nodes[1].InnerText;
                if (valuta.Contains("EUR"))
                    continue;

                yield return new Product(
                    ProductType.FShort,
                    Int32.Parse(productTypeText.Replace("FlexKort&#174;", "").Replace("&#229;r", "").Trim(), new CultureInfo("da-dk")),
                    Decimal.Parse(nodes[2].ChildNodes[0].InnerText.Trim(), new CultureInfo("da-dk")),
                    Decimal.Parse(nodes[3].ChildNodes[0].InnerText.Trim(), new CultureInfo("da-dk")));
            }
        }
    }
}