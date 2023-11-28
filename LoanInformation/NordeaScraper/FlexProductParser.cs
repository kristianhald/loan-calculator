using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using HtmlAgilityPack;
using Shared;

namespace NordeaScraper
{
    public class FlexProductParser
    {
        public IEnumerable<Product> Parse(string exchangeData)
        {
            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(exchangeData);

            var products = htmlDocument
                .DocumentNode
                .SelectNodes("//table/tbody")
                .Last()
                .ChildNodes;
            foreach (var product in products)
            {
                yield return new Product(
                    ProductType.Flex,
                    Int32.Parse(product.ChildNodes[0].InnerText, new CultureInfo("da-dk")),
                    Decimal.Parse(product.ChildNodes[1].InnerText.Replace("%", ""), new CultureInfo("da-dk")),
                    100m);
            }
        }
    }
}
