using System;
using System.Collections.Generic;
using Website.Configuration.LoanModels;

namespace Website.Tests.Configuration.LoanModels
{
    public class CompanyDataBuilder
    {
        private int _id;
        private string _name;
        private List<ProductData> _products = new List<ProductData>();
        private List<ContributionRateStairCaseData> _contributionRateStairCases = new List<ContributionRateStairCaseData>();
        private bool _calculateContributionRateFromTotalPayout;

        public CompanyDataBuilder WithId(int id)
        {
            _id = id;
            return this;
        }

        public CompanyDataBuilder WithName(string name)
        {
            _name = name;
            return this;
        }

        public CompanyDataBuilder WithProduct(Action<ProductDataBuilder> builderFunc)
        {
            var builder = new ProductDataBuilder();
            builderFunc(builder);
            _products.Add(builder.Build());
            return this;
        }

        public CompanyDataBuilder WithContributionRateStairCase(Action<ContributionRateStairCaseDataBuilder> builderFunc)
        {
            var builder = new ContributionRateStairCaseDataBuilder();
            builderFunc(builder);
            _contributionRateStairCases.Add(builder.Build());
            return this;
        }

        public CompanyDataBuilder WithCalculateContributionRateFromTotalPayout(bool calculateContributionRateFromTotalPayout)
        {
            _calculateContributionRateFromTotalPayout = calculateContributionRateFromTotalPayout;
            return this;
        }

        public CompanyData Build()
        {
            return new CompanyData
            {
                Id = _id,
                Name = _name,
                Products = _products,
                ContributionRateStairCases = _contributionRateStairCases,
                CalculateContributionRateFromTotalPayout = _calculateContributionRateFromTotalPayout
            };
        }
    }
}