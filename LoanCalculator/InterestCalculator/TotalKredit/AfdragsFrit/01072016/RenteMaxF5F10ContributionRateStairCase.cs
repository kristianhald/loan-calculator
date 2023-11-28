using System.Collections.Generic;
using Koolawong.InterestCalculator.Model.Outside.Complex;
using Koolawong.InterestCalculator.Model.Outside.Simple;

namespace Koolawong.InterestCalculator.TotalKredit.AfdragsFrit._01072016
{
    public class RenteMaxF5F10ContributionRateStairCase
    {
        private RenteMaxF5F10ContributionRateStairCase() { }

        public static ContributionRateStairCase Create()
        {
            return ContributionRateStairCase.From(Rates());
        }

        private static IEnumerable<ContributionRateStep> Rates()
        {
            return new[]
            {
                ContributionRateStep.From(LoanToValue.From(0.40m), YearlyContributionRate.From(0.006000m)),
                ContributionRateStep.From(LoanToValue.From(0.60m), YearlyContributionRate.From(0.013500m)),
                ContributionRateStep.From(LoanToValue.From(0.80m), YearlyContributionRate.From(0.022500m)),
                ContributionRateStep.From(LoanToValue.From(1.00m), YearlyContributionRate.From(0.01200m))
            };
        }
    }
}