using System.Collections.Generic;
using Koolawong.InterestCalculator.Model.Inside.Simple;
using Koolawong.InterestCalculator.Model.Outside.Simple;
using Xunit;

namespace Koolawong.InterestCalculator.Tests.Model
{
    public class TermContributionRateTests
    {
        [Fact]
        public void GivenSingleTermPerYear_ThenTermContributionRateIsSameAsYearlyContributionRate()
        {
            var yearlyContributionRate = YearlyContributionRate.From(0.1m);
            var termsPerYear = TermsPerYear.From(1);

            var termContributionRate = yearlyContributionRate / termsPerYear;

            Assert.Equal((decimal)yearlyContributionRate, (decimal)termContributionRate);
        }

        public static IEnumerable<object[]> YearlyContributionRateToTermContributionRateData => new[]
        {
            new object[] {YearlyContributionRate.From(0.12m), TermsPerYear.From(2), TermContributionRate.From(0.06m)},
            new object[] {YearlyContributionRate.From(0.12m), TermsPerYear.From(3), TermContributionRate.From(0.04m)},
            new object[] {YearlyContributionRate.From(0.12m), TermsPerYear.From(4), TermContributionRate.From(0.03m)},
            new object[] {YearlyContributionRate.From(0.12m), TermsPerYear.From(6), TermContributionRate.From(0.02m) },
            new object[] {YearlyContributionRate.From(0.12m), TermsPerYear.From(12), TermContributionRate.From(0.01m)},
        };

        [Theory]
        [MemberData("YearlyContributionRateToTermContributionRateData")]
        public void GivenProvidedInput_ThenOutputMatchesExpected(
            YearlyContributionRate yearlyContributionRate,
            TermsPerYear termsPerYear,
            TermContributionRate expected)
        {
            var termContributionRate = yearlyContributionRate / termsPerYear;

            Assert.Equal(expected, termContributionRate);
        }
    }
}
