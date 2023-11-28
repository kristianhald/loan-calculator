using System.Collections.Generic;

namespace Website.Configuration.LoanModels
{
    public class CompanyData
    {
        public int Id;

        public string Name;

        public IEnumerable<ProductData> Products;

        public IEnumerable<ContributionRateStairCaseData> ContributionRateStairCases;

        public bool CalculateContributionRateFromTotalPayout;
    }
}