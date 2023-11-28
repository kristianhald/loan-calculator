using Website.Model.Loading;

namespace Website.Tests.Model.Loading
{
    public class InterestRateDataBuilder
    {
        private decimal _interestRate;
        private bool _showInterestRate;

        public InterestRateDataBuilder WithInterestRate(decimal interestRate)
        {
            _interestRate = interestRate;
            return this;
        }

        public InterestRateDataBuilder WithShowInterestRate(bool showInterestRate)
        {
            _showInterestRate = showInterestRate;
            return this;
        }

        public InterestRateData Build()
        {
            return new InterestRateData
            {
                InterestRate = _interestRate,
                ShowInterestRate = _showInterestRate
            };
        }
    }
}