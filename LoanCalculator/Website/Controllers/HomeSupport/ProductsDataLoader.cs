using System;
using System.Collections.Generic;
using System.Linq;
using Website.Configuration;
using Website.Configuration.LoanModels;
using Website.Model.Loading;
using ProductData = Website.Model.Loading.ProductData;

namespace Website.Controllers.HomeSupport
{
    public class ProductsDataLoader
    {
        private readonly ILoanConfigurationLoader _loanConfigurationLoader;

        public ProductsDataLoader()
            : this(new ConfigurationButlerLoanConfigurationLoader())
        { }

        public ProductsDataLoader(ILoanConfigurationLoader loanConfigurationLoader)
        {
            if (loanConfigurationLoader == null)
                throw new ArgumentNullException(nameof(loanConfigurationLoader));

            _loanConfigurationLoader = loanConfigurationLoader;
        }

        public ConfigurationData LoadProductsByCompany()
        {
            return _loanConfigurationLoader.Load();
        }

        public IEnumerable<ProductData> LoadProducts()
        {
            var configurationData = _loanConfigurationLoader.Load();

            var products = new List<ProductData>();

            var allProductsData = configurationData.SelectMany(company => company.Products);
            foreach (var productGroup in allProductsData.GroupBy(p => p.Name))
            {
                var periods = new List<PeriodData>();
                foreach (var periodGroup in productGroup.GroupBy(p => p.Period))
                {
                    var interestRates = new List<InterestRateData>();
                    if (productGroup.Key.StartsWith("Fast rente")) // TODO: Should be defined via the configuration data
                    {
                        foreach (var interestRateData in periodGroup.GroupBy(p => p.InterestRate))
                        {
                            var interestRate = new InterestRateData
                            {
                                InterestRate = interestRateData.Key,
                                ShowInterestRate = true
                            };
                            interestRates.Add(interestRate);
                        }
                    }
                    else
                    {
                        interestRates.Add(new InterestRateData
                        {
                            ShowInterestRate = false
                        });
                    }

                    var period = new PeriodData
                    {
                        Period = periodGroup.Key,
                        InterestRate = interestRates
                    };
                    periods.Add(period);
                }
                var product = new ProductData
                {
                    Name = productGroup.Key,
                    Periods = periods
                };
                products.Add(product);
            }

            return products;
        }
    }
}