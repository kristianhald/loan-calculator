using Koolawong.InterestCalculator.Extensions;
using Koolawong.InterestCalculator.Model.Inside.Simple;
using Koolawong.InterestCalculator.Model.Outside.Simple;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Koolawong.InterestCalculator.Model.Outside.Complex
{
    public class ContributionRateStairCase
    {
        private readonly ContributionRateStep[] _contributionRateSteps;

        private ContributionRateStairCase(IEnumerable<ContributionRateStep> contributionRateSteps)
        {
            if (contributionRateSteps == null)
                throw new ArgumentNullException(nameof(contributionRateSteps));
            if (contributionRateSteps.IsEmpty())
                throw new ArgumentOutOfRangeException(nameof(contributionRateSteps), "Staircase must contain steps.");

            _contributionRateSteps = contributionRateSteps.ToArray();
        }

        public static ContributionRateStairCase From(IEnumerable<ContributionRateStep> contributionRateSteps)
        {
            return new ContributionRateStairCase(contributionRateSteps);
        }

        // Should this be removed and the knowledge of how to calculate multiple loans contribution rates moved to
        // a loan service or maybe a contribution rate service?
        public YearlyContributionRate Calculate(MortgagePayout payout, HouseValue value) 
        {
            return Calculate(payout, value, PriorityLoan.From(0m));
        }

        public YearlyContributionRate Calculate(MortgagePayout payout, HouseValue value, PriorityLoan priorityLoans)
        {
            var priorityOnlyLtv = priorityLoans / value;
            var priorityAndPayoutLtv = (payout + priorityLoans) / value;

            var totalContribution = Contribution.From(0m);
            var previousStep = ContributionRateStep.From(LoanToValue.From(0m), YearlyContributionRate.From(0m));
            foreach (var currentStep in _contributionRateSteps)
            {
                if (previousStep.UpperStep > priorityAndPayoutLtv)
                    break;

                if (currentStep.UpperStep <= priorityOnlyLtv)
                    continue;

                var lowerLoanToValue = previousStep.UpperStep < priorityOnlyLtv
                    ? priorityOnlyLtv
                    : previousStep.UpperStep;

                var upperLoanToValue = currentStep.UpperStep < priorityAndPayoutLtv
                    ? currentStep.UpperStep
                    : priorityAndPayoutLtv;

                var valueOfRate = (upperLoanToValue - lowerLoanToValue) * value;
                totalContribution += valueOfRate * currentStep.ContributionRate;

                previousStep = currentStep;
            }

            return totalContribution / payout;
        }
    }
}