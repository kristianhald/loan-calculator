using Koolawong.InterestCalculator.Model.Outside.Simple;

namespace Koolawong.InterestCalculator.Model.Outside.Complex
{
    public class ContributionRateStep
    {
        private ContributionRateStep(LoanToValue upperStep, YearlyContributionRate contributionRate)
        {
            UpperStep = upperStep;
            ContributionRate = contributionRate;
        }

        public LoanToValue UpperStep { get; }

        public YearlyContributionRate ContributionRate { get; }

        public static ContributionRateStep From(LoanToValue upperStep, YearlyContributionRate contributionRate)
        {
            return new ContributionRateStep(upperStep, contributionRate);
        }
    }
}