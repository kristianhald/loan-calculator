using Website.Configuration.LoanModels;

namespace Website.Tests.Configuration.LoanModels
{
    public class ProductDataBuilder
    {
        private int _id;
        private string _name;
        private int _period;
        private decimal _interestRate;
        private decimal _exchangeRate;
        private int _contributionRateStairCaseId;

        public ProductDataBuilder WithId(int id)
        {
            _id = id;
            return this;
        }

        public ProductDataBuilder WithName(string name)
        {
            _name = name;
            return this;
        }

        public ProductDataBuilder WithPeriod(int period)
        {
            _period = period;
            return this;
        }

        public ProductDataBuilder WithInterestRate(decimal interestRate)
        {
            _interestRate = interestRate;
            return this;
        }

        public ProductDataBuilder WithExchangeRate(decimal exchangeRate)
        {
            _exchangeRate = exchangeRate;
            return this;
        }

        public ProductDataBuilder WithContributionRateStairCaseId(int contributionRateStairCaseId)
        {
            _contributionRateStairCaseId = contributionRateStairCaseId;
            return this;
        }

        public ProductData Build()
        {
            return new ProductData
            {
                Id = _id,
                Name = _name,
                Period = _period,
                InterestRate = _interestRate,
                ExchangeRate = _exchangeRate,
                ContributionRateStairCaseId = _contributionRateStairCaseId
            };
        }
    }
}