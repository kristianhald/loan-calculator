using System.Collections.Generic;

namespace DefaultDataSetupper
{
    public class CompanyContributionRateStairCases
    {
        public CompanyContributionRateStairCases(
            int id,
            IEnumerable<ContributionRateStairCase> contributionRateStairCases)
        {
            Id = id;
            ContributionRateStairCases = contributionRateStairCases;
        }

        public int Id { get; }

        public IEnumerable<ContributionRateStairCase> ContributionRateStairCases { get; }
    }
}
