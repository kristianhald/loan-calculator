using Shared;
using Xunit;

namespace NykreditScraper.Tests
{
    public class ProductParserTest
    {
        [Fact]
        public void GivenTheExchangeList_ThenAllProductsAreParsed()
        {
            var expected = new[]
            {
                new Product(ProductType.FixedRate, 10, 0.5m, 98.42m),
                new Product(ProductType.FixedRate, 10, 1.0m, 100.85m),
                new Product(ProductType.FixedRate, 15, 1.0m, 98.047m),
                new Product(ProductType.FixedRate, 15, 1.5m, 100.653m),
                new Product(ProductType.FixedRate, 15, 2.0m, 102.35m),
                new Product(ProductType.FixedRate, 20, 1.5m, 97.623m),
                new Product(ProductType.FixedRate, 20, 2.0m, 100.803m),
                new Product(ProductType.FixedRate, 20, 2.5m, 102.35m),
                new Product(ProductType.FixedRate, 30, 1.5m, 93.08m),
                new Product(ProductType.FixedRate, 30, 2.0m, 97.374m),
                new Product(ProductType.FixedRate, 30, 2.5m, 100.734m),
                new Product(ProductType.FixedRate, 30, 3.0m, 102.523m),
                new Product(ProductType.FixedRate, 30, 3.5m, 103.8m),
                new Product(ProductType.FixedRateInterestOnly, 30, 2.0m, 95.891m),
                new Product(ProductType.FixedRateInterestOnly, 30, 2.5m, 99.449m),
                new Product(ProductType.FixedRateInterestOnly, 30, 3.0m, 101.995m),
                new Product(ProductType.FixedRateInterestOnly, 30, 3.5m, 103.081m),
                new Product(ProductType.FShort, 3, 0.14m, 100m),

            };

            var parser = new ProductParser();
            var actual = parser.Parse(TestData.TestData.Kursliste);

            Assert.Equal(expected, actual);
        }
    }
}