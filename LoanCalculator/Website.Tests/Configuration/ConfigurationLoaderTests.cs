using FluentAssertions;
using System;
using System.Linq;
using Website.Configuration;
using Xunit;

namespace Website.Tests.Configuration
{
    public class ConfigurationLoaderTests
    {
        [Fact]
        public void CanDataBeReadFromExternalDataSource()
        {
            var loader = new ConfigurationButlerLoanConfigurationLoader();
            var actual = loader.Load();

            actual.Should().HaveCount(5)
                .And.OnlyContain(company => company.Id != 0)
                .And.OnlyContain(company => !String.IsNullOrWhiteSpace(company.Name))
                .And.OnlyContain(company => company.Products != null && company.Products.Any())
                .And.OnlyContain(company => company.Products.All(product => product.Id > 0))
                .And.OnlyContain(company => company.Products.All(product => !String.IsNullOrWhiteSpace(product.Name)))
                .And.OnlyContain(company => company.Products.All(product => product.ContributionRateStairCaseId > 0))
                .And.OnlyContain(company => company.Products.All(product => product.InterestRate > -5.0m))
                .And.OnlyContain(company => company.Products.All(product => product.InterestRate < 5.0m))
                .And.OnlyContain(company => company.Products.All(product => product.Period >= 5))
                .And.OnlyContain(company => company.Products.All(product => product.Period <= 30))
                .And.OnlyContain(company => company.Products.All(product => product.ExchangeRate > 85m))
                .And.OnlyContain(company => company.Products.All(product => product.ExchangeRate < 125m))
                .And.OnlyContain(company => company.ContributionRateStairCases != null && company.ContributionRateStairCases.Any())
                .And.OnlyContain(company => company.ContributionRateStairCases.All(rate => rate.Id > 0))
                .And.OnlyContain(company => company.ContributionRateStairCases.All(rate => rate.Steps != null && rate.Steps.Any()))
                .And.OnlyContain(company => company.ContributionRateStairCases.All(rate => rate.Steps.All(step => step.ContributionRate > 0m)))
                .And.OnlyContain(company => company.ContributionRateStairCases.All(rate => rate.Steps.All(step => step.ContributionRate <= 5m)))
                .And.OnlyContain(company => company.ContributionRateStairCases.All(rate => rate.Steps.All(step => step.UpperStep >= 40m)))
                .And.OnlyContain(company => company.ContributionRateStairCases.All(rate => rate.Steps.All(step => step.UpperStep <= 100m)));
        }
    }
}
