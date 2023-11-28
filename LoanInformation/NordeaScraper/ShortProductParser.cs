using Newtonsoft.Json.Linq;
using Shared;
using System.Collections.Generic;
using System.Linq;

namespace NordeaScraper
{
    public class ShortProductParser
    {
        public IEnumerable<Product> Parse(string exchangeData)
        {
            var productData = JArray.Parse(exchangeData).First();
            if (productData["amortisations"].First()["instalment"].ToObject<decimal>() == 0m)
                return new Product[0];

            var productType = ProductType.FShort;
            var period = 3; // TODO: Set because the refinancing happens around every 3 years. This is not entirely correct and the correct refinancing time can be found here: https://www.nordea.dk/privat/lan/bolig/kortrente.html#tab=Fakta_Refinansiering-af-lan

            var interestRate = productData["keyFigures"]["interestRate"].ToObject<decimal>();
            var exchangeRate = 100m;
            return new[]
            {
                new Product(
                    productType,
                    period,
                    interestRate,
                    exchangeRate)
            };
        }
    }
}