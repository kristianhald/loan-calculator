using System;
using System.Collections.Generic;
using Website.Model.Loading;

namespace Website.Tests.Model.Loading
{
    public class PeriodDataBuilder
    {
        private int _period;
        private List<InterestRateData> _interestRate = new List<InterestRateData>();

        public PeriodDataBuilder WithPeriod(int period)
        {
            _period = period;
            return this;
        }

        public PeriodDataBuilder WithInterestRate(Action<InterestRateDataBuilder> builderFunc)
        {
            var builder = new InterestRateDataBuilder();
            builderFunc(builder);
            _interestRate.Add(builder.Build());
            return this;
        }

        public PeriodData Build()
        {
            return new PeriodData
            {
                Period = _period,
                InterestRate = _interestRate
            };
        }
    }
}