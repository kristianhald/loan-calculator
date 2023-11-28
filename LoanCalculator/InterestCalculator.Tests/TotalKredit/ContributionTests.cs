using Koolawong.InterestCalculator.TotalKredit.Afdrag._01032016;
using Koolawong.InterestCalculator.TotalKredit.Afdrag._01072016;
using System.Collections.Generic;
using Koolawong.InterestCalculator.Model.Inside.Simple;
using Koolawong.InterestCalculator.Model.Outside.Complex;
using Koolawong.InterestCalculator.Model.Outside.Simple;
using Xunit;
using F1F2ContributionRateStairCase = Koolawong.InterestCalculator.TotalKredit.Afdrag._01032016.F1F2ContributionRateStairCase;
using FastRenteContributionRateStairCase = Koolawong.InterestCalculator.TotalKredit.Afdrag._01032016.FastRenteContributionRateStairCase;

namespace Koolawong.InterestCalculator.Tests.TotalKredit
{
    public class ContributionTests
    {
        public static IEnumerable<object[]> AtEightyPercentLtvContributionRateStairCaseData => new[]
        {
            new object[] { YearlyContributionRate.From(0.006125m), FastRenteContributionRateStairCase.Create() },
            new object[] { YearlyContributionRate.From(0.007500m), RenteMaxF3ToF10FKortContributionRateStairCase.Create() },
            new object[] { YearlyContributionRate.From(0.008500m), F1F2ContributionRateStairCase.Create() },
            new object[] { YearlyContributionRate.From(0.007375m), InterestCalculator.TotalKredit.Afdrag._01072016.FastRenteContributionRateStairCase.Create() },
            new object[] { YearlyContributionRate.From(0.008750m), RenteMaxF5F10ContributionRateStairCase.Create() },
            new object[] { YearlyContributionRate.From(0.009000m), FKortContributionRateStairCase.Create() },
            new object[] { YearlyContributionRate.From(0.010750m), F3F4ContributionRateStairCase.Create() },
            new object[] { YearlyContributionRate.From(0.011750m), InterestCalculator.TotalKredit.Afdrag._01072016.F1F2ContributionRateStairCase.Create() },
            new object[] { YearlyContributionRate.From(0.008875m), InterestCalculator.TotalKredit.AfdragsFrit._01032016.FastRenteContributionRateStairCase.Create() },
            new object[] { YearlyContributionRate.From(0.010250m), InterestCalculator.TotalKredit.AfdragsFrit._01032016.RenteMaxF3ToF10FKortContributionRateStairCase.Create() },
            new object[] { YearlyContributionRate.From(0.011250m), InterestCalculator.TotalKredit.AfdragsFrit._01032016.F1F2ContributionRateStairCase.Create() },
            new object[] { YearlyContributionRate.From(0.010625m), InterestCalculator.TotalKredit.AfdragsFrit._01072016.FastRenteContributionRateStairCase.Create() },
            new object[] { YearlyContributionRate.From(0.012000m), InterestCalculator.TotalKredit.AfdragsFrit._01072016.RenteMaxF5F10ContributionRateStairCase.Create() },
            new object[] { YearlyContributionRate.From(0.012250m), InterestCalculator.TotalKredit.AfdragsFrit._01072016.FKortContributionRateStairCase.Create() },
            new object[] { YearlyContributionRate.From(0.014000m), InterestCalculator.TotalKredit.AfdragsFrit._01072016.F3F4ContributionRateStairCase.Create() },
            new object[] { YearlyContributionRate.From(0.015000m), InterestCalculator.TotalKredit.AfdragsFrit._01072016.F1F2ContributionRateStairCase.Create() },
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