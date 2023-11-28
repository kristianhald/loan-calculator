using System.Collections.Generic;

namespace Website.Configuration.LoanModels
{
    public class ContributionRateStairCaseData
    {
        public int Id;

        public IEnumerable<ContributionRateStairCaseStepData> Steps;
    }
}