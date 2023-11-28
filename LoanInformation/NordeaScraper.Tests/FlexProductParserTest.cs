using Shared;
using Xunit;

namespace NordeaScraper.Tests
{
    public class FlexProductParserTest
    {
        [Fact]
        public void GivenTheExchangeList_ThenAllProductsAreParsed()
        {
            var expected = new[]
            {
                new Product(ProductType.Flex, 1, -0.049m, 100m),
                new Product(ProductType.Flex, 2,  0.009m, 100m),
                new Product(ProductType.Flex, 3,  0.030m, 100m),
                new Product(ProductType.Flex, 4,  0.118m, 100m),
                new Product(ProductType.Flex, 5,  0.262m, 100m),
            };

            var parser = new FlexProductParser();
            var actual = parser.Parse(TestData.TestData.FlexRenteKursliste);

            Assert.Equal(expected, actual);
        }
    }
}
