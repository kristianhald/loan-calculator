using System.Collections.Generic;
using Koolawong.InterestCalculator.Model.Inside.Simple;
using Koolawong.InterestCalculator.Model.Outside.Simple;
using Xunit;

namespace Koolawong.InterestCalculator.Tests.Model
{
    public class TermsTests
    {
        public static IEnumerable<object[]> PeriodToTerms => new[]
        {
            new object[] {Period.From(1), TermsPerYear.From(1), Terms.From(1)},
            new object[] {Period.From(1), TermsPerYear.From(2), Terms.From(2)},
            new object[] {Period.From(1), TermsPerYear.From(3), Terms.From(3)},
            new object[] {Period.From(1), TermsPerYear.From(4), Terms.From(4)},
            new object[] {Period.From(1), TermsPerYear.From(6), Terms.From(6)},
            new object[] {Period.From(1), TermsPerYear.From(12), Terms.From(12)},
            new object[] {Period.From(2), TermsPerYear.From(1), Terms.From(2)},
            new object[] {Period.From(3), TermsPerYear.From(1), Terms.From(3)},
            new object[] {Period.From(4), TermsPerYear.From(1), Terms.From(4)},
            new object[] {Period.From(5), TermsPerYear.From(1), Terms.From(5)},
            new object[] {Period.From(10), TermsPerYear.From(1), Terms.From(10)},
            new object[] {Period.From(15), TermsPerYear.From(1), Terms.From(15)},
            new object[] {Period.From(20), TermsPerYear.From(1), Terms.From(20)},
            new object[] {Period.From(30), TermsPerYear.From(1), Terms.From(30)},
            new object[] {Period.From(3), TermsPerYear.From(2), Terms.From(6)},
            new object[] {Period.From(4), TermsPerYear.From(3), Terms.From(12)},
            new object[] {Period.From(5), TermsPerYear.From(4), Terms.From(20)},
            new object[] {Period.From(10), TermsPerYear.From(6), Terms.From(60)},
            new object[] {Period.From(30), TermsPerYear.From(12), Terms.From(360) },
        };

        [Theory]
        [MemberData("PeriodToTerms")]
        public void GivenTheProvidedTermsPerYearAndPeriodLength_ThenTermsMatchesExpected(
            Period period,
            TermsPerYear termsPerYear,
            Terms expected)
        {
            var terms = period * termsPerYear;

            Assert.Equal(expected, terms);
        }
    }
}
