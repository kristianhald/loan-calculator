﻿using System.Collections.Generic;
using Koolawong.InterestCalculator.Model.Outside.Complex;
using Koolawong.InterestCalculator.Model.Outside.Simple;

namespace Koolawong.InterestCalculator.BrfKredit.AfdragsFrit
{
    public class KortRenteContributionRateStairCase
    {
        private KortRenteContributionRateStairCase() { }

        public static ContributionRateStairCase Create()
        {
            return ContributionRateStairCase.From(Rates());
        }

        private static IEnumerable<ContributionRateStep> Rates()
        {
            return new[]
            {
                ContributionRateStep.From(LoanToValue.From(0.40m), YearlyContributionRate.From(0.004750m)),
                ContributionRateStep.From(LoanToValue.From(0.60m), YearlyContributionRate.From(0.011500m)),
                ContributionRateStep.From(LoanToValue.From(0.80m), YearlyContributionRate.From(0.02000m)),
                ContributionRateStep.From(LoanToValue.From(1.00m), YearlyContributionRate.From(0.01025m))
            };
        }
    }
}