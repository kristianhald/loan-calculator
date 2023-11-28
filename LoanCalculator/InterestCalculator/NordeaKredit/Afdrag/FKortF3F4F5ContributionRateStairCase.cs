using System.Collections.Generic;
using Koolawong.InterestCalculator.Model.Outside.Complex;
using Koolawong.InterestCalculator.Model.Outside.Simple;

namespace Koolawong.InterestCalculator.NordeaKredit.Afdrag
{
    public class FKortF3F4F5ContributionRateStairCase
    {
        private FKortF3F4F5ContributionRateStairCase() { }

        public static ContributionRateStairCase Create()
        {
            return ContributionRateStairCase.From(Rates());
        }

        private static IEnumerable<ContributionRateStep> Rates()
        {
            return new[]
            {
                ContributionRateStep.From(LoanToValue.From(0.40m), YearlyContributionRate.From(0.004500m)),
                ContributionRateStep.From(LoanToValue.From(0.60m), YearlyContributionRate.From(0.009750m)),
                ContributionRateStep.From(LoanToValue.From(0.80m), YearlyContributionRate.From(0.013250m)),
                ContributionRateStep.From(LoanToValue.From(1.00m), YearlyContributionRate.From(0.00800m))
            };
        }
    }
}