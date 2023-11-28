using System.Collections.Generic;
using Koolawong.InterestCalculator.Model.Outside.Complex;
using Koolawong.InterestCalculator.Model.Outside.Simple;

namespace Koolawong.InterestCalculator.RealkreditDanmark.Afdrag
{
    public class FlexTContributionRateStairCase
    {
        private FlexTContributionRateStairCase() { }

        public static ContributionRateStairCase Create()
        {
            return ContributionRateStairCase.From(Rates());
        }

        private static IEnumerable<ContributionRateStep> Rates()
        {
            return new[]
            {
                ContributionRateStep.From(LoanToValue.From(0.40m), YearlyContributionRate.From(0.003248m)),
                ContributionRateStep.From(LoanToValue.From(0.60m), YearlyContributionRate.From(0.008748m)),
                ContributionRateStep.From(LoanToValue.From(0.80m), YearlyContributionRate.From(0.014000m)),
                ContributionRateStep.From(LoanToValue.From(1.00m), YearlyContributionRate.From(0.007312m - 0.000001m))
            };
        }
    }
}