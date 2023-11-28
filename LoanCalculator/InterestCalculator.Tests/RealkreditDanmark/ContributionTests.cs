using System.Collections.Generic;
using Koolawong.InterestCalculator.Model.Inside.Simple;
using Koolawong.InterestCalculator.Model.Outside.Complex;
using Koolawong.InterestCalculator.Model.Outside.Simple;
using Koolawong.InterestCalculator.RealkreditDanmark.Afdrag;
using Xunit;

namespace Koolawong.InterestCalculator.Tests.RealkreditDanmark
{
    public class ContributionTests
    {
        public static IEnumerable<object[]> SpecificLtvContributionRateStairCaseData => new[]
        {
            // Example 1
            new object[] { YearlyContributionRate.From(0.0068052021333860285413842372m), FastRenteContributionRateStairCase.Create(), HouseValue.From(3900000m), MortgagePayout.From(3104512m + 12786m) },
            // Example 2
            new object[] { YearlyContributionRate.From(0.008564m - 0.000001m), FlexKContributionRateStairCase.Create(), HouseValue.From(3900000m), MortgagePayout.From(3063860m + 56140m) },
        };

        [Theory]
        [MemberData("SpecificLtvContributionRateStairCaseData")]
        public void SpecificContributionRates(
            YearlyContributionRate expectedContributionRate,
            ContributionRateStairCase stairCase,
            HouseValue value,
            MortgagePayout loan)
        {
            var averageContributionRate = stairCase.Calculate(loan, value);

            Assert.Equal(expectedContributionRate, averageContributionRate);
        }

        public static IEnumerable<object[]> AtEightyPercentLtvContributionRateStairCaseData => new[]
        {
            new object[] { YearlyContributionRate.From(0.006812m - 0.000001m), FastRenteContributionRateStairCase.Create()},
            new object[] { YearlyContributionRate.From(0.008564m - 0.000001m), FlexKContributionRateStairCase.Create(), },
            new object[] { YearlyContributionRate.From(0.007312m - 0.000001m), FlexTContributionRateStairCase.Create(), },
            new object[] { YearlyContributionRate.From(0.008374m), InterestCalculator.RealkreditDanmark.AfdragsFrit.FastRenteContributionRateStairCase.Create(), },
            new object[] { YearlyContributionRate.From(0.010126m), InterestCalculator.RealkreditDanmark.AfdragsFrit.FlexKContributionRateStairCase.Create(), },
            new object[] { YearlyContributionRate.From(0.008874m), InterestCalculator.RealkreditDanmark.AfdragsFrit.FlexTContributionRateStairCase.Create(), },
        };

        [Theory]
        [MemberData("AtEightyPercentLtvContributionRateStairCaseData")]
        public void AtEightyPercentLtvContributionRates(
            YearlyContributionRate expectedContributionRate,
            ContributionRateStairCase stairCase)
        {
            var value = HouseValue.From(4000000m);
            var loan = MortgagePayout.From(3200000m);

            var averageContributionRate = stairCase.Calculate(loan, value);

            Assert.Equal(expectedContributionRate, averageContributionRate);
        }

        [Theory]
        [MemberData("AtEightyPercentLtvContributionRateStairCaseData")]
        public void AboveEightyPercentLtvContributionRates(
            YearlyContributionRate expectedContributionRate,
            ContributionRateStairCase stairCase)
        {
            var value = HouseValue.From(4000000m);
            var loan = MortgagePayout.From(3500000m);

            var averageContributionRate = stairCase.Calculate(loan, value);

            Assert.Equal(expectedContributionRate, averageContributionRate);
        }

        // TODO: Add tests for splitting contribution rate. Determine how BrfKredit does it
    }
}