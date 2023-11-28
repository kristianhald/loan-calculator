using System.Collections.Generic;
using Koolawong.InterestCalculator.Model.Inside.Simple;
using Koolawong.InterestCalculator.Model.Outside.Complex;
using Koolawong.InterestCalculator.Model.Outside.Simple;
using Koolawong.InterestCalculator.NordeaKredit.Afdrag;
using Koolawong.InterestCalculator.NordeaKredit.AfdragsFrit;
using Xunit;
using FastRenteContributionRateStairCase = Koolawong.InterestCalculator.NordeaKredit.Afdrag.FastRenteContributionRateStairCase;

namespace Koolawong.InterestCalculator.Tests.NordeaKredit
{
    public class ContributionTests
    {
        public static IEnumerable<object[]> SpecificLtvContributionRateStairCaseData => new[]
        {
            // Example 1
            new object[] { YearlyContributionRate.From(0.00650m), FastRenteContributionRateStairCase.Create(), HouseValue.From(3900000m), MortgagePayout.From(3105720m + 59425m) },
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
            new object[] { YearlyContributionRate.From(0.00650m), FastRenteContributionRateStairCase.Create()},
            new object[] { YearlyContributionRate.From(0.009m + 0.00002500m), F1F2ContributionRateStairCase.Create(), },
            new object[] { YearlyContributionRate.From(0.008m), FKortF3F4F5ContributionRateStairCase.Create(), },
            new object[] { YearlyContributionRate.From(0.009m), InterestCalculator.NordeaKredit.AfdragsFrit.FastRenteContributionRateStairCase.Create(), },
            new object[] { YearlyContributionRate.From(0.0115m), S1S2ContributionRateStairCase.Create(), },
            new object[] { YearlyContributionRate.From(0.0105m), FKortS3S4S5ContributionRateStairCase.Create(), },
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