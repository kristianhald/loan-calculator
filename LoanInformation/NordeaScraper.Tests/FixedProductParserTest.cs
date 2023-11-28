using Shared;
using Xunit;

namespace NordeaScraper.Tests
{
    public class FixedProductParserTest
    {
        [Fact]
        public void GivenTheExchangeList_ThenAllProductsAreParsed()
        {
            var expected = new[]
            {
                new Product(ProductType.FixedRateInterestOnly, 30, 3.5m, 102.650m),
                new Product(ProductType.FixedRateInterestOnly, 30, 2.0m,  97.125m),
                new Product(ProductType.FixedRate, 30, 3.5m, 102.855m),
                new Product(ProductType.FixedRate, 30, 2.0m,  98.500m),
                new Product(ProductType.FixedRate, 20, 1.5m,  99.306m),
                new Product(ProductType.FixedRate, 15, 1.5m,  99.306m),
                new Product(ProductType.FixedRate, 10, 1.5m,  99.306m),
                new Product(ProductType.FixedRateInterestOnly, 30, 3.0m, 102.250m),
                new Product(ProductType.FixedRateInterestOnly, 30, 2.5m, 100.550m),
                new Product(ProductType.FixedRate, 30, 3.0m, 103.175m),
                new Product(ProductType.FixedRate, 30, 2.5m, 101.700m),
                new Product(ProductType.FixedRate, 20, 2.5m, 103.000m),
                new Product(ProductType.FixedRate, 20, 2.0m, 101.625m),
                new Product(ProductType.FixedRate, 15, 2.0m, 102.950m),
                new Product(ProductType.FixedRate, 15, 2.0m, 101.625m),
                new Product(ProductType.FixedRate, 10, 2.0m, 102.950m),
                new Product(ProductType.FixedRate, 10, 2.0m, 101.625m),
            };

            var parser = new FixedProductParser();
            var actual = parser.Parse(TestData.TestData.FastRenteKursliste);

            Assert.Equal(expected, actual);
        }
    }
}