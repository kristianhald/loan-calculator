using Koolawong.InterestCalculator.Model.Inside.Simple;
using Koolawong.InterestCalculator.Model.Outside.Simple;
using Xunit;

namespace Koolawong.InterestCalculator.Tests.Model
{
    public class PrincipalTests
    {
        [Fact]
        public void GivenExchangeRateAt100_ThenPrincipalEqualsPayout()
        {
            var exchangeRate = ExchangeRate.From(1m);
            var payout = MortgagePayout.From(1000m);

            var principal = payout / exchangeRate;

            Assert.Equal((decimal)payout, (decimal)principal);
        }

        [Fact]
        public void GivenExchangeRateIsLowerThan100_ThenPrincipalIsHigherThanPayout()
        {
            var expected = Principal.From(1250m);

            var exchangeRate = ExchangeRate.From(0.8m);
            var payout = MortgagePayout.From(1000m);

            var principal = payout / exchangeRate;

            Assert.Equal(expected, principal);
        }

        [Fact]
        public void GivenExchangeRateIsHigherThan100_ThenPrincipalIsLowerThanPayout()
        {
            var expected = Principal.From(800m);

            var exchangeRate = ExchangeRate.From(1.25m);
            var payout = MortgagePayout.From(1000m);

            var principal = payout / exchangeRate;

            Assert.Equal(expected, principal);
        }
    }
}