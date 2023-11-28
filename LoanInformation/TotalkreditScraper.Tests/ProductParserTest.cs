using Shared;
using Xunit;

namespace TotalkreditScraper.Tests
{
    public class ProductParserTest
    {
        [Fact]
        public void GivenTheExchangeList_ThenAllProductsAreParsed()
        {
            var expected = new[]
            {
                new Product(ProductType.FixedRate, 10, 0.5m, 98.7340m),
                new Product(ProductType.FixedRate, 15, 1.0m, 98.0750m),
                new Product(ProductType.FixedRate, 20, 1.5m, 97.7480m),
                new Product(ProductType.FixedRate, 20, 1.5m, 96.7000m),
                new Product(ProductType.FixedRate, 30, 2.0m, 97.2710m),
                new Product(ProductType.FixedRate, 30, 1.5m, 92.9750m),
                new Product(ProductType.FixedRate, 30, 2.0m, 96.4000m),
                new Product(ProductType.FixedRateInterestOnly, 30, 2.5m, 99.7790m),
                new Product(ProductType.FixedRateInterestOnly, 30, 2.0m, 95.5990m),
                new Product(ProductType.FixedRateInterestOnly, 30, 2.5m, 98.0530m),
                new Product(ProductType.Flex, 3, -0.1428m, 102.9720m),
                new Product(ProductType.Flex, 4,  0.0601m, 103.3740m),
                new Product(ProductType.Flex, 5,  0.2515m, 103.4160m),
                new Product(ProductType.Flex, 6,  0.4918m, 102.8000m),
                new Product(ProductType.Flex, 7,  0.7200m, 101.8000m),
                new Product(ProductType.Flex, 8,  0.8601m, 108.3500m),
                new Product(ProductType.Flex, 9,  1.0704m, 107.6000m),
                new Product(ProductType.Flex, 10, 1.1735m, 98.4744m),
                new Product(ProductType.FShort, 30, 0.2259m, 101.0700m)
            };


            var parser = new ProductParser();
            var actual = parser.Parse(TestData.TestData.Kursliste);

            Assert.Equal(expected, actual);
        }
    }
}
