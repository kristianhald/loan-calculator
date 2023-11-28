using System.Collections.Generic;
using Koolawong.InterestCalculator.Model.Outside.Complex;
using Koolawong.InterestCalculator.Model.Outside.Simple;

namespace Koolawong.InterestCalculator.TotalKredit.AfdragsFrit._01032016
{
    public class F1F2ContributionRateStairCase
    {
        private F1F2ContributionRateStairCase() { }

        public static ContributionRateStairCase Create()
        {
            return ContributionRateStairCase.From(Rates());
        }

        private static IEnumerable<ContributionRateStep> Rates()
        {
            return new[]
            {
                ContributionRateStep.From(LoanToValue.From(0.40m), YearlyContributionRate.From(0.005500m)),
                ContributionRateStep.From(LoanToValue.From(0.60m), YearlyContributionRate.From(0.013000m)),
                ContributionRateStep.From(LoanToValue.From(0.80m), YearlyContributionRate.From(0.021000m)),
                ContributionRateStep.From(LoanToValue.From(1.00m), YearlyContributionRate.From(0.01125m))
            };
        }
    }
}