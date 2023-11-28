using Shared;
using Xunit;

namespace NordeaScraper.Tests
{
    public class ShortProductParserTest
    {
        [Fact]
        public void GivenTheExchangeList20170525_ThenAllProductsAreParsed()
        {
            var expected = new[]
            {
                new Product(ProductType.FShort, 3, 0.16m, 100m), // The '3' is not entire correct. It depends on when the loan starts. Should look at the year
            };

            var parser = new ShortProductParser();
            var actual = parser.Parse(TestData.TestData.KortRenteKursliste20170525OBLC);

            Assert.Equal(expected, actual);
        }
    }
}