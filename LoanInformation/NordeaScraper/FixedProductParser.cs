using System;
using System.Collections.Generic;
using System.Globalization;
using Newtonsoft.Json.Linq;
using Shared;

namespace NordeaScraper
{
    public class FixedProductParser
    {
        public IEnumerable<Product> Parse(string exchangeData)
        {
            var products = JArray.Parse(exchangeData);
            foreach (var product in products)
            {
                var productType = ((string)product["repaymentFreedomMax"]).Equals("Nej") ? ProductType.FixedRate : ProductType.FixedRateInterestOnly;
                var period = (int)product["loanPeriodMax"];
                var interestRate = Decimal.Parse(((string)product["fundName"]).Split(' ')[0].Replace("%", "").Trim(), new CultureInfo("da-dk"));
                var exchangeRate = Decimal.Parse(((string)product["rate"]).Replace("*&nbsp;", ""), new CultureInfo("da-dk"));
                yield return new Product(
                    productType,
                    period,
                    interestRate,
                    exchangeRate);
            }
        }
    }
}
