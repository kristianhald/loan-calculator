﻿using System.Collections.Generic;
using Koolawong.InterestCalculator.Model.Outside.Complex;
using Koolawong.InterestCalculator.Model.Outside.Simple;

namespace Koolawong.InterestCalculator.RealkreditDanmark.Afdrag
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
                ContributionRateStep.From(LoanToValue.From(0.40m), YearlyContributionRate.From(0.002748m)),
                ContributionRateStep.From(LoanToValue.From(0.60m), YearlyContributionRate.From(0.008248m)),
                ContributionRateStep.From(LoanToValue.From(0.80m), YearlyContributionRate.From(0.013500m)),
                ContributionRateStep.From(LoanToValue.From(1.00m), YearlyContributionRate.From(0.006812m - 0.000001m))
            };
        }
    }
}