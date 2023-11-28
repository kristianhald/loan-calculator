using System.Collections.Generic;
using Koolawong.InterestCalculator.BrfKredit.Afdrag;
using Koolawong.InterestCalculator.Model.Inside.Simple;
using Koolawong.InterestCalculator.Model.Outside.Complex;
using Koolawong.InterestCalculator.Model.Outside.Simple;
using Xunit;

namespace Koolawong.InterestCalculator.Tests.BrfKredit
{
    public class ContributionTests
    {
        public static IEnumerable<object[]> SpecificLtvContributionRateStairCaseData => new[]
        {
            // Example 1
            new object[] { YearlyContributionRate.From(0.0060595m - 0.0000003629348965531628603168m), FastRenteContributionRateStairCase.Create(), HouseValue.From(3900000m), MortgagePayout.From(3000000m + 40560m + 27296m) },
            // Example 2
            new object[] { YearlyContributionRate.From(0.0083870m - 0.0000000729495642317536415108m), VariabelRenteContributionRateStairCase.Create(), HouseValue.From(3900000m), MortgagePayout.From(3000000m + 40560m + 26093m) },
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
            new object[] { YearlyContributionRate.From(0.008500m), VariabelRenteContributionRateStairCase.Create()},
            new object[] { YearlyContributionRate.From(0.006125m), FastRenteContributionRateStairCase.Create(), },
            new object[] { YearlyContributionRate.From(0.007500m), KortRenteContributionRateStairCase.Create(), },
            new object[] { YearlyContributionRate.From(0.010750m), InterestCalculator.BrfKredit.AfdragsFrit.VariabelRenteContributionRateStairCase.Create(), },
            new object[] { YearlyContributionRate.From(0.008875m), InterestCalculator.BrfKredit.AfdragsFrit.FastRenteContributionRateStairCase.Create(), },
            new object[] { YearlyContributionRate.From(0.010250m), InterestCalculator.BrfKredit.AfdragsFrit.KortRenteContributionRateStairCase.Create(), },
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