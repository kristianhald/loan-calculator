using Koolawong.InterestCalculator.Model.Outside.Complex;
using Koolawong.InterestCalculator.Model.Outside.Simple;
using System.Collections.Generic;

namespace Koolawong.InterestCalculator.BrfKredit.AfdragsFrit
{
    public class FastRenteContributionRateStairCase
    {
        private FastRenteContributionRateStairCase() { }

        public static ContributionRateStairCase Create()
        {
            return ContributionRateStairCase.From(Rates());
        }

        private static IEnumerable<ContributionRateStep> Rates()
        {
            return new[]
            {
                ContributionRateStep.From(LoanToValue.From(0.40m), YearlyContributionRate.From(0.004000m)),
                ContributionRateStep.From(LoanToValue.From(0.60m), YearlyContributionRate.From(0.010000m)),
                ContributionRateStep.From(LoanToValue.From(0.80m), YearlyContributionRate.From(0.017500m)),
                ContributionRateStep.From(LoanToValue.From(1.00m), YearlyContributionRate.From(0.008875m))
            };
        }
    }
}