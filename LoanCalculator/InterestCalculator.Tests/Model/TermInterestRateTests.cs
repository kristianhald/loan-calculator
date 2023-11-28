using System.Collections.Generic;
using Koolawong.InterestCalculator.Model.Inside.Simple;
using Koolawong.InterestCalculator.Model.Outside.Simple;
using Xunit;

namespace Koolawong.InterestCalculator.Tests.Model
{
    public class TermInterestRateTests
    {
        [Fact]
        public void GivenSingleTermPerYear_ThenTermInterestRateIsSameAsYearlyInterestRate()
        {
            var yearlyInterestRate = YearlyInterestRate.From(0.1m);
            var termsPerYear = TermsPerYear.From(1);

            var termInterestRate = yearlyInterestRate / termsPerYear;

            Assert.Equal((decimal)yearlyInterestRate, (decimal)termInterestRate);
        }

        public static IEnumerable<object[]> YearlyInterestRateToTermInterestRateData => new[]
        {
            new object[] {YearlyInterestRate.From(0.12m), TermsPerYear.From(2), TermInterestRate.From(0.06m)},
            new object[] {YearlyInterestRate.From(0.12m), TermsPerYear.From(3), TermInterestRate.From(0.04m)},
            new object[] {YearlyInterestRate.From(0.12m), TermsPerYear.From(4), TermInterestRate.From(0.03m)},
            new object[] {YearlyInterestRate.From(0.12m), TermsPerYear.From(6), TermInterestRate.From(0.02m) },
            new object[] {YearlyInterestRate.From(0.12m), TermsPerYear.From(12), TermInterestRate.From(0.01m)},
        };

        [Theory]
        [MemberData("YearlyInterestRateToTermInterestRateData")]
        public void GivenProvidedInput_ThenOutputMatchesExpected(
            YearlyInterestRate yearlyInterestRate,
            TermsPerYear termsPerYear,
            TermInterestRate expected)
        {
            var termInterestRate = yearlyInterestRate / termsPerYear;

            Assert.Equal(expected, termInterestRate);
        }
    }
}
