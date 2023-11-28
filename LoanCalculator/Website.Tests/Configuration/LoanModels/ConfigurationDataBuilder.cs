using System;
using Website.Configuration.LoanModels;

namespace Website.Tests.Configuration.LoanModels
{
    public class ConfigurationDataBuilder
    {
        private ConfigurationData _configurationData = new ConfigurationData();

        public ConfigurationDataBuilder WithCompany(Action<CompanyDataBuilder> builderFunc)
        {
            var builder = new CompanyDataBuilder();
            builderFunc(builder);
            _configurationData.Add(builder.Build());
            return this;
        }

        public ConfigurationData Build()
        {
            return _configurationData;
        }
    }
}