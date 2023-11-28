namespace DefaultDataSetupper
{
    public class ContributionRateStairCaseStep
    {
        public ContributionRateStairCaseStep(
            decimal upperStep,
            decimal contributionRate)
        {
            UpperStep = upperStep;
            ContributionRate = contributionRate;
        }

        public decimal UpperStep { get; }

        public decimal ContributionRate { get; }
    }
}
