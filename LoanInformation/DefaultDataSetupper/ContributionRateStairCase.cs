using System.Collections.Generic;

namespace DefaultDataSetupper
{
    public class ContributionRateStairCase
    {
        public ContributionRateStairCase(
            int id,
            IEnumerable<string> type,
            IEnumerable<ContributionRateStairCaseStep> steps)
        {
            Id = id;
            Type = type;
            Steps = steps;
        }

        public int Id { get; }

        public IEnumerable<string> Type { get; }

        public IEnumerable<ContributionRateStairCaseStep> Steps { get; }
    }
}
