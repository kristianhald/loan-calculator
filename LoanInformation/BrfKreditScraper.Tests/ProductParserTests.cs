using Shared;
using System.Linq;
using Xunit;

namespace BrfKreditScraper.Tests
{
    public class ProductParserTests
    {
        [Fact]
        public void GivenTheExchangeList_ThenAllProductsAreParsed()
        {
            var expected = new[]
            {
                new Product(ProductType.FixedRate, 10, 0.5m,  99.17m),
                new Product(ProductType.FixedRate, 10, 1.0m, 100.95m),
                new Product(ProductType.FixedRate, 10, 1.5m, 102.22m),
                new Product(ProductType.FixedRate, 15, 1.0m,  98.22m),
                new Product(ProductType.FixedRate, 15, 2.0m, 103.24m),
                new Product(ProductType.FixedRate, 20, 1.5m,  98.11m),
                new Product(ProductType.FixedRate, 20, 2.0m, 101.14m),
                new Product(ProductType.FixedRate, 20, 2.5m, 103.50m),
                new Product(ProductType.FixedRate, 30, 1.5m,  93.18m),
                new Product(ProductType.FixedRateInterestOnly, 30, 2.0m,  95.75m),
                new Product(ProductType.FixedRate, 30, 2.0m,  97.47m),
                new Product(ProductType.FixedRate, 30, 2.5m, 101.40m),
                new Product(ProductType.FixedRateInterestOnly, 30, 2.5m, 100.28m),
                new Product(ProductType.FixedRateInterestOnly, 30, 3.0m, 103.24m),
                new Product(ProductType.FixedRate, 30, 3.0m,  104.00m),
                new Product(ProductType.FixedRate, 30, 3.5m,  105.45m),
                new Product(ProductType.FixedRateInterestOnly, 30, 3.5m, 105.40m),
                new Product(ProductType.FShort, 30, -0.13m, 100.62m),
                new Product(ProductType.Flex, 1, -0.49m, 100.51m),
                new Product(ProductType.Flex, 2, -0.18m, 102.16m),
                new Product(ProductType.Flex, 3, -0.16m, 103.22m),
                new Product(ProductType.Flex, 4,  0.08m, 103.38m),
                new Product(ProductType.Flex, 5,  0.26m, 103.37m),
                new Product(ProductType.Flex, 6,  0.52m, 102.63m),
            };

            var parser = new ProductParser();
            var actual = parser.Parse(TestData.TestData.Kursliste);

            for(var index = 0; index < actual.Count(); index++)
                Assert.Equal(expected.ElementAt(index), actual.ElementAt(index));
        }
    }
}
