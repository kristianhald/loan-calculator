using Website.Configuration.LoanModels;

namespace Website.Tests.Configuration.LoanModels
{
    public class ContributionRateStairCaseStepDataBuilder
    {
        private decimal _upperStep;
        private decimal _contributionRate;

        public ContributionRateStairCaseStepDataBuilder WithUpperStep(decimal upperStep)
        {
            _upperStep = upperStep;
            return this;
        }

        public ContributionRateStairCaseStepDataBuilder WithContributionRate(decimal contributionRate)
        {
            _contributionRate = contributionRate;
            return this;
        }

        public ContributionRateStairCaseStepData Build()
        {
            return new ContributionRateStairCaseStepData
            {
                UpperStep = _upperStep,
                ContributionRate = _contributionRate
            };
        }
    }
}