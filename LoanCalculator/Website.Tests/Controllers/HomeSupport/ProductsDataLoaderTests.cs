using FakeItEasy;
using FluentAssertions;
using System.Linq;
using Website.Configuration;
using Website.Controllers.HomeSupport;
using Website.Tests.Configuration.LoanModels;
using Website.Tests.Model.Loading;
using Xunit;

namespace Website.Tests.Controllers.HomeSupport
{
    public class ProductsDataLoaderTests
    {
        [Fact]
        public void GivenSingleCompany_WithASingleProduct_ThenTheSingleProductIsReturnedWithoutMerge()
        {
            var expected = new LoadingDataBuilder()
                .WithProduct(product => product
                    .WithName("Fast rente")
                    .WithPeriod(period => period
                        .WithPeriod(30)
                        .WithInterestRate(interestRate => interestRate
                            .WithInterestRate(10m)
                            .WithShowInterestRate(true))))
                .Build();

            var loanConfigurationLoader = A.Fake<ILoanConfigurationLoader>();
            A.CallTo(() => loanConfigurationLoader.Load())
                .Returns(new ConfigurationDataBuilder()
                    .WithCompany(company => company
                        .WithProduct(product => product
                            .WithName(expected.Products.Single().Name)
                            .WithPeriod(expected.Products.Single().Periods.Single().Period)
                            .WithInterestRate(expected.Products.Single().Periods.Single().InterestRate.Single().InterestRate)))
                    .Build());
            var controller = new ProductsDataLoader(loanConfigurationLoader);

            var actual = controller.LoadProducts();

            actual.ShouldBeEquivalentTo(expected.Products);
        }

        [Fact]
        public void GivenSingleCompany_WithDifferentProducts_ThenTheProductsAreReturnedWithoutMerge()
        {
            var expected = new LoadingDataBuilder()
                .WithProduct(product => product
                    .WithName("Fast rente")
                    .WithPeriod(period => period
                        .WithPeriod(30)
                        .WithInterestRate(interestRate => interestRate
                            .WithInterestRate(10m)
                            .WithShowInterestRate(true))))
                .WithProduct(product => product
                    .WithName("Flex 1 år")
                    .WithPeriod(period => period
                        .WithPeriod(30)
                        .WithInterestRate(interestRate => interestRate
                            .WithInterestRate(0m)
                            .WithShowInterestRate(false))))
                .Build();

            var loanConfigurationLoader = A.Fake<ILoanConfigurationLoader>();
            A.CallTo(() => loanConfigurationLoader.Load())
                .Returns(new ConfigurationDataBuilder()
                    .WithCompany(company => company
                        .WithProduct(product => product
                            .WithName(expected.Products.First().Name)
                            .WithPeriod(expected.Products.First().Periods.Single().Period)
                            .WithInterestRate(expected.Products.First().Periods.Single().InterestRate.Single().InterestRate)))
                    .WithCompany(company => company
                        .WithProduct(product => product
                            .WithName(expected.Products.Last().Name)
                            .WithPeriod(expected.Products.Last().Periods.Single().Period)
                            .WithInterestRate(expected.Products.Last().Periods.Single().InterestRate.Single().InterestRate)))
                    .Build());
            var controller = new ProductsDataLoader(loanConfigurationLoader);

            var actual = controller.LoadProducts();

            actual.ShouldBeEquivalentTo(expected.Products);
        }

        [Fact]
        public void GivenSingleCompany_WithSingleProductHavingDifferentPeriods_ThenProductsAreMergedIntoSingleProductWithMultiplePeriods()
        {
            var expected = new LoadingDataBuilder()
                .WithProduct(product => product
                    .WithName("Fast rente")
                    .WithPeriod(period => period
                        .WithPeriod(30)
                        .WithInterestRate(interestRate => interestRate
                            .WithInterestRate(10m)
                            .WithShowInterestRate(true)))
                    .WithPeriod(period => period
                        .WithPeriod(20)
                        .WithInterestRate(interestRate => interestRate
                            .WithInterestRate(10m)
                            .WithShowInterestRate(true))))
                .Build();

            var loanConfigurationLoader = A.Fake<ILoanConfigurationLoader>();
            A.CallTo(() => loanConfigurationLoader.Load())
                .Returns(new ConfigurationDataBuilder()
                    .WithCompany(company => company
                        .WithProduct(product => product
                            .WithName(expected.Products.Single().Name)
                            .WithPeriod(expected.Products.Single().Periods.First().Period)
                            .WithInterestRate(expected.Products.Single().Periods.First().InterestRate.Single().InterestRate))
                        .WithProduct(product => product
                            .WithName(expected.Products.Single().Name)
                            .WithPeriod(expected.Products.Single().Periods.Last().Period)
                            .WithInterestRate(expected.Products.Single().Periods.Last().InterestRate.Single().InterestRate)))
                    .Build());
            var controller = new ProductsDataLoader(loanConfigurationLoader);

            var actual = controller.LoadProducts();

            actual.ShouldBeEquivalentTo(expected.Products);
        }

        [Fact]
        public void GivenSingleCompany_WithSingleProductHavingDifferentInterestRate_ThenProductsAreMergedIntoSingleProductWithMultipleInterestRates()
        {
            var expected = new LoadingDataBuilder()
                .WithProduct(product => product
                    .WithName("Fast rente")
                    .WithPeriod(period => period
                        .WithPeriod(30)
                        .WithInterestRate(interestRate => interestRate
                            .WithInterestRate(10m)
                            .WithShowInterestRate(true))
                        .WithInterestRate(interestRate => interestRate
                            .WithInterestRate(15m)
                            .WithShowInterestRate(true))))
                .Build();

            var loanConfigurationLoader = A.Fake<ILoanConfigurationLoader>();
            A.CallTo(() => loanConfigurationLoader.Load())
                .Returns(new ConfigurationDataBuilder()
                    .WithCompany(company => company
                        .WithProduct(product => product
                            .WithName(expected.Products.Single().Name)
                            .WithPeriod(expected.Products.Single().Periods.Single().Period)
                            .WithInterestRate(expected.Products.Single().Periods.Single().InterestRate.First().InterestRate))
                        .WithProduct(product => product
                            .WithName(expected.Products.Single().Name)
                            .WithPeriod(expected.Products.Single().Periods.Single().Period)
                            .WithInterestRate(expected.Products.Single().Periods.Single().InterestRate.Last().InterestRate)))
                    .Build());
            var controller = new ProductsDataLoader(loanConfigurationLoader);

            var actual = controller.LoadProducts();

            actual.ShouldBeEquivalentTo(expected.Products);
        }

        [Fact]
        public void GivenSingleCompany_WithSingleProductHavingSameInterestRate_ThenProductsAreMergedIntoSingleProductWithSingleInterestRates()
        {
            var expected = new LoadingDataBuilder()
                .WithProduct(product => product
                    .WithName("Fast rente")
                    .WithPeriod(period => period
                        .WithPeriod(30)
                        .WithInterestRate(interestRate => interestRate
                            .WithInterestRate(10m)
                            .WithShowInterestRate(true))))
                .Build();

            var loanConfigurationLoader = A.Fake<ILoanConfigurationLoader>();
            A.CallTo(() => loanConfigurationLoader.Load())
                .Returns(new ConfigurationDataBuilder()
                    .WithCompany(company => company
                        .WithProduct(product => product
                            .WithName(expected.Products.Single().Name)
                            .WithPeriod(expected.Products.Single().Periods.Single().Period)
                            .WithInterestRate(expected.Products.Single().Periods.Single().InterestRate.Single().InterestRate))
                        .WithProduct(product => product
                            .WithName(expected.Products.Single().Name)
                            .WithPeriod(expected.Products.Single().Periods.Single().Period)
                            .WithInterestRate(expected.Products.Single().Periods.Single().InterestRate.Single().InterestRate)))
                    .Build());
            var controller = new ProductsDataLoader(loanConfigurationLoader);

            var actual = controller.LoadProducts();

            actual.ShouldBeEquivalentTo(expected.Products);
        }

        [Fact]
        public void GivenMultipleCompanies_WithDifferentProducts_ThenProductsAreReturnedWithoutMerge()
        {
            var expected = new LoadingDataBuilder()
                .WithProduct(product => product
                    .WithName("Fast rente")
                    .WithPeriod(period => period
                        .WithInterestRate(interestRate => interestRate
                            .WithInterestRate(15m)
                            .WithShowInterestRate(true))))
                .WithProduct(product => product
                    .WithName("Fast rente med afdragsfrihed")
                    .WithPeriod(period => period
                        .WithInterestRate(interestRate => interestRate
                            .WithInterestRate(15m)
                            .WithShowInterestRate(true))))
                .Build();

            var loanConfigurationLoader = A.Fake<ILoanConfigurationLoader>();
            A.CallTo(() => loanConfigurationLoader.Load())
                .Returns(new ConfigurationDataBuilder()
                    .WithCompany(company => company
                        .WithName("First company")
                        .WithProduct(product => product
                            .WithName(expected.Products.First().Name)
                            .WithPeriod(expected.Products.First().Periods.Single().Period)
                            .WithInterestRate(expected.Products.First().Periods.Single().InterestRate.Single().InterestRate)))
                    .WithCompany(company => company
                        .WithName("Second company")
                        .WithProduct(product => product
                            .WithName(expected.Products.Last().Name)
                            .WithPeriod(expected.Products.Last().Periods.Single().Period)
                            .WithInterestRate(expected.Products.Last().Periods.Single().InterestRate.Single().InterestRate)))
                    .Build());
            var controller = new ProductsDataLoader(loanConfigurationLoader);

            var actual = controller.LoadProducts();

            actual.ShouldBeEquivalentTo(expected.Products);
        }

        [Fact]
        public void GivenMultipleCompanies_WithSameProduct_ThenProductsAreMerged()
        {
            var expected = new LoadingDataBuilder()
                .WithProduct(product => product
                    .WithName("Fast rente")
                    .WithPeriod(period => period
                        .WithPeriod(30)
                        .WithInterestRate(interestRate => interestRate
                            .WithInterestRate(15m)
                            .WithShowInterestRate(true))))
                .Build();

            var loanConfigurationLoader = A.Fake<ILoanConfigurationLoader>();
            A.CallTo(() => loanConfigurationLoader.Load())
                .Returns(new ConfigurationDataBuilder()
                    .WithCompany(company => company
                        .WithName("First company")
                        .WithProduct(product => product
                            .WithName(expected.Products.Single().Name)
                            .WithPeriod(expected.Products.Single().Periods.Single().Period)
                            .WithInterestRate(expected.Products.Single().Periods.Single().InterestRate.Single().InterestRate)))
                    .WithCompany(company => company
                        .WithName("Second company")
                        .WithProduct(product => product
                            .WithName(expected.Products.Single().Name)
                            .WithPeriod(expected.Products.Single().Periods.Single().Period)
                            .WithInterestRate(expected.Products.Single().Periods.Single().InterestRate.Single().InterestRate)))
                    .Build());
            var controller = new ProductsDataLoader(loanConfigurationLoader);

            var actual = controller.LoadProducts();

            actual.ShouldBeEquivalentTo(expected.Products);
        }

        [Fact]
        public void GivenMultipleCompanies_WithSameFlexProduct_ButDifferentInterestRates_ThenProductIsStillMerged()
        {
            var expected = new LoadingDataBuilder()
                .WithProduct(product => product
                    .WithName("Flex 1 år")
                    .WithPeriod(period => period
                        .WithPeriod(30)
                        .WithInterestRate(interestRate => interestRate
                            .WithInterestRate(0m)
                            .WithShowInterestRate(false))))
                .Build();

            var loanConfigurationLoader = A.Fake<ILoanConfigurationLoader>();
            A.CallTo(() => loanConfigurationLoader.Load())
                .Returns(new ConfigurationDataBuilder()
                    .WithCompany(company => company
                        .WithName("First company")
                        .WithProduct(product => product
                            .WithName(expected.Products.First().Name)
                            .WithPeriod(expected.Products.First().Periods.Single().Period)
                            .WithInterestRate(99m)))
                    .WithCompany(company => company
                        .WithName("Second company")
                        .WithProduct(product => product
                            .WithName(expected.Products.Last().Name)
                            .WithPeriod(expected.Products.Last().Periods.Single().Period)
                            .WithInterestRate(1m)))
                    .Build());
            var controller = new ProductsDataLoader(loanConfigurationLoader);

            var actual = controller.LoadProducts();

            actual.ShouldBeEquivalentTo(expected.Products);
        }
    }
}