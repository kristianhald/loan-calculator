using System;
using System.Collections.Generic;
using Website.Model.Loading;

namespace Website.Tests.Model.Loading
{
    public class ProductDataBuilder
    {
        private string _name;
        private List<PeriodData> _periods = new List<PeriodData>();

        public ProductDataBuilder WithName(string name)
        {
            _name = name;
            return this;
        }

        public ProductDataBuilder WithPeriod(Action<PeriodDataBuilder> builderFunc)
        {
            var builder = new PeriodDataBuilder();
            builderFunc(builder);
            _periods.Add(builder.Build());
            return this;
        }

        public ProductData Build()
        {
            return new ProductData
            {
                Name = _name,
                Periods = _periods
            };
        }
    }
}