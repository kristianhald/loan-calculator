using Newtonsoft.Json.Linq;
using Shared;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace NykreditScraper
{
    public class ProductParser
    {
        public IEnumerable<Product> Parse(string exchangeData)
        {
            var products = JObject.Parse(exchangeData);
            foreach (var product in products["kursliste"])
            {
                var productType = GetProductType(product);
                if (!productType.HasValue)
                    continue;

                IEnumerable<JToken> productVariants = product["obligationer"];
                if (productType.Value == ProductType.FShort)
                    productVariants = productVariants.Skip(productVariants.Count() - 1);

                foreach (var productVariant in productVariants)
                {
                    var period = 3; // TODO: A bit incorrect, but should be fine for now
                    var exchangeRate = 100m;
                    if (productType.Value != ProductType.FShort)
                    {
                        period = (int)productVariant["loebetid"];
                        exchangeRate = Decimal.Parse((string)productVariant["tilbudskurs"], CultureInfo.InvariantCulture);
                    }

                    var interestRate = Decimal.Parse((string)productVariant["rente"], new CultureInfo("da-dk"));

                    yield return new Product(
                        productType.Value,
                        period,
                        interestRate,
                        exchangeRate);
                }
            }
        }

        private ProductType? GetProductType(JToken product)
        {
            var productName = (string)product["navn"];
            switch (productName)
            {
                case "Fastforrentede obligations- og kontantlån":
                    return ProductType.FixedRate;

                case "Fastforrentede obligationslån med afdragsfrihed":
                    return ProductType.FixedRateInterestOnly;

                case "F-kort":
                    return ProductType.FShort;

                default:
                    return null;
            }
        }
    }
}