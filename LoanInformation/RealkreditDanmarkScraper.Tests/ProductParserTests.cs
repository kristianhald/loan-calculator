using Shared;
using Xunit;

namespace RealkreditDanmarkScraper.Tests
{
    public class ProductParserTests
    {
        [Fact]
        public void GivenTheExchangeList_ThenAllProductsAreParsed()
        {
            var expected = new[]
            {
                new Product(ProductType.FixedRate, 30, 2.0m, 98.943m),
                new Product(ProductType.FixedRate, 30, 1.5m, 94.621m),
                new Product(ProductType.FixedRate, 20, 1.5m, 99.296m),
                new Product(ProductType.FixedRate, 15, 1.0m, 98.918m),
                new Product(ProductType.FixedRate, 10, 0.5m, 99.325m),
                new Product(ProductType.FixedRateInterestOnly, 30, 2.0m, 97.449m),
                new Product(ProductType.Flex, 1, -0.12m, 100.628m),
                new Product(ProductType.Flex, 2, -0.22m, 101.884m),
                new Product(ProductType.Flex, 3, -0.13m, 102.825m),
                new Product(ProductType.Flex, 5,  0.08m, 103.988m),
                new Product(ProductType.FShort, 30, 0.00m, 100.291m),
            };

            var parser = new ProductParser();
            var actual = parser.Parse(TestData.TestData.Kursliste);

            Assert.Equal(expected, actual);
        }
    }
}