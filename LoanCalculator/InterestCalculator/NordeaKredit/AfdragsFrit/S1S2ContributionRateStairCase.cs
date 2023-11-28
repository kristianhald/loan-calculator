using System.Collections.Generic;
using Koolawong.InterestCalculator.Model.Outside.Complex;
using Koolawong.InterestCalculator.Model.Outside.Simple;

namespace Koolawong.InterestCalculator.NordeaKredit.AfdragsFrit
{
    public class S1S2ContributionRateStairCase
    {
        private S1S2ContributionRateStairCase() { }

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
                ContributionRateStep.From(LoanToValue.From(0.80m), YearlyContributionRate.From(0.020500m)),
                ContributionRateStep.From(LoanToValue.From(1.00m), YearlyContributionRate.From(0.01150m))
            };
        }
    }
}