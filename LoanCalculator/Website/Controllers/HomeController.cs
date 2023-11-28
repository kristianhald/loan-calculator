using Koolawong.InterestCalculator.Model.Inside.Simple;
using Koolawong.InterestCalculator.Model.Outside.Complex;
using Koolawong.InterestCalculator.Model.Outside.Simple;
using Koolawong.InterestCalculator.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Helpers;
using System.Web.Mvc;
using Website.ApplicationInsights;
using Website.Configuration.LoanModels;
using Website.Controllers.HomeSupport;
using Website.Model.Calculation;
using Website.Model.Loading;
using ProductData = Website.Model.Loading.ProductData;

namespace Website.Controllers
{
    public class HomeController : Controller
    {
        private readonly ProductsDataLoader _productsDataLoader = new ProductsDataLoader();
        private readonly LoanService _loanService = new LoanService();

        public HomeController()
        { }

        // GET: Home
        public ActionResult Index()
        {
            Telemetry.Create().TrackUserInitialVisit();

            return View();
        }

        public ActionResult LoadData()
        {
            Telemetry.Create().TrackUserLoadingData();

            var productsDataKey = "ProductsData";
            IEnumerable<ProductData> products = WebCache.Get(productsDataKey);
            if (products == null)
            {
                var now = DateTime.Now;
                products = _productsDataLoader.LoadProducts().ToList().OrderBy(p => p.Name);
                WebCache.Set(productsDataKey, products, (int)now.AddDays(1).Date.Subtract(now).TotalMinutes + 30);
            }

            var loadingData = new LoadingData
            {
                Products = products,
                DefaultSettings = new[]
                {
                    new DefaultSetting
                    {
                        ProductName = products.First().Name,
                        Period = products.First().Periods.OrderBy(p => p.Period).Last().Period,
                        InterestRate = products.First().Periods.OrderBy(p => p.Period).Last().InterestRate.OrderBy(i => i.InterestRate).Last().InterestRate
                    },
                    new DefaultSetting
                    {
                        ProductName = products.Last().Name,
                        Period = products.Last().Periods.OrderBy(p => p.Period).Last().Period,
                        InterestRate = products.Last().Periods.OrderBy(p => p.Period).Last().InterestRate.OrderBy(i => i.InterestRate).Last().InterestRate
                    },
                    new DefaultSetting
                    {
                        ProductName = products.First(p => p.Name.Contains("3")).Name,
                        Period = products.First(p => p.Name.Contains("3")).Periods.OrderBy(p => p.Period).Last().Period,
                        InterestRate = products.First(p => p.Name.Contains("3")).Periods.OrderBy(p => p.Period).Last().InterestRate.OrderBy(i => i.InterestRate).Last().InterestRate
                    }
                }
            };

            var jsonSerializerSettings = new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() };
            var json = JsonConvert.SerializeObject(loadingData, Formatting.Indented, jsonSerializerSettings);

            return Content(json, "application/json");
        }

        [HttpPost]
        public ActionResult CalculateSpecification(CalculationData data)
        {
            Telemetry.Create().TrackUserCalculation(data);

            var configurationDataKey = "ConfigurationData";
            ConfigurationData configurationData = WebCache.Get(configurationDataKey);
            if (configurationData == null)
            {
                var now = DateTime.Now;
                configurationData = _productsDataLoader.LoadProductsByCompany();
                WebCache.Set(configurationDataKey, configurationData, (int)now.AddDays(1).Date.Subtract(now).TotalMinutes + 30);
            }

            var calculationDate = CalculationDate.From(DateTime.Today);
            var houseValue = HouseValue.From(data.Information.Value);
            var termsPerYear = TermsPerYear.From(4);

            var payoutDistribution = _loanService.CalculatePayoutDistribution(
                houseValue,
                Savings.From(data.Information.Value - data.Information.Amount),
                UpperMortgageLimitPercentage.From(0.8m),
                UpperBankLoanLimitPercentage.From(0.95m));

            var results = new List<MortgageCompanyResultData>();
            foreach (var companyData in configurationData)
            {
                var wasProductsFound = true;
                var loans = new List<Koolawong.InterestCalculator.Services.Data.Loan>();
                if (data.Specification != null)
                {
                    foreach (var specification in data.Specification)
                    {
                        Configuration.LoanModels.ProductData product;
                        // TODO: Needs to be defined via the configuration
                        if (specification.Type == "Fast rente" || specification.Type == "Fast rente med afdragsfrihed")
                        {
                            product = companyData.Products.SingleOrDefault(p =>
                                p.Name == specification.Type &&
                                p.Period == specification.Period &&
                                p.InterestRate == specification.InterestRate);
                        }
                        else
                        {
                            product = companyData.Products.SingleOrDefault(p =>
                                p.Name == specification.Type &&
                                p.Period == specification.Period);
                        }

                        if (product == null)
                        {
                            wasProductsFound = false;
                            break;
                        }

                        var contributionRateStairCase = companyData
                            .ContributionRateStairCases
                            .Single(c => c.Id == product.ContributionRateStairCaseId);

                        loans.Add(new Koolawong.InterestCalculator.Services.Data.Loan(
                            MortgagePayout.From((decimal)payoutDistribution.MortgagePayout * specification.Percentage / 100m),
                            termsPerYear,
                            Period.From(product.Period),
                            YearlyInterestRate.From(product.InterestRate / 100m),
                            ExchangeRate.From(product.ExchangeRate / 100m),
                            ContributionRateStairCase.From(
                                contributionRateStairCase.Steps.Select(
                                    step => ContributionRateStep.From(
                                        LoanToValue.From(step.UpperStep / 100m),
                                        YearlyContributionRate.From(step.ContributionRate / 100m))))));
                    }
                }
                else
                {
                    wasProductsFound = false;
                }

                if (!wasProductsFound)
                {
                    results.Add(new MortgageCompanyResultData
                    {
                        CompanyName = companyData.Name
                    });
                }
                else
                {
                    var loanInformation = _loanService.CalculatePaymentPlan(
                        calculationDate,
                        houseValue,
                        companyData.CalculateContributionRateFromTotalPayout,
                        loans);

                    var startYear = (int)calculationDate.Year;
                    var firstYearPaymentPlan = loanInformation
                        .PaymentPlan
                        .PlanByTerms
                        .Skip(1) // Skipping the first term as it will not be an entire term
                        .Take(4)
                        .ToList();
                    var result = new MortgageCompanyResultData
                    {
                        CompanyName = companyData.Name,
                        Overview = new ResultOverview
                        {
                            FirstYearPayment =
                                Math.Round(
                                    firstYearPaymentPlan.Sum(
                                        term =>
                                            (decimal)term.Repayment + (decimal)term.Interest +
                                            (decimal)term.Contribution)),
                            FirstYearRepayment = Math.Round(firstYearPaymentPlan.Sum(term => (decimal)term.Repayment)),
                            FirstYearInterest = Math.Round(firstYearPaymentPlan.Sum(term => (decimal)term.Interest)),
                            FirstYearContribution =
                                Math.Round(firstYearPaymentPlan.Sum(term => (decimal)term.Contribution))
                        },
                        PaymentPlan = loanInformation.PaymentPlan.PlanByYears.Select(plan => new ResultDetailed
                        {
                            Year = startYear++,
                            Payment =
                                Math.Round((decimal)plan.Repayment + (decimal)plan.Interest +
                                           (decimal)plan.Contribution),
                            Repayment = Math.Round((decimal)plan.Repayment),
                            Contribution = Math.Round((decimal)plan.Contribution),
                            InterestRate = Math.Round((decimal)plan.Interest),
                            LoanLeft = Math.Round((decimal)plan.PaymentLeft)
                        })
                    };

                    results.Add(result);
                }
            }

            var resultsData = new ResultData
            {
                Results = results,
                BankResult = new BankResultData
                {
                    BankPayout = (decimal)payoutDistribution.BankLoanPayout
                },
                OwnPaymentResult = new OwnPaymentResultData
                {
                    OwnPayment = (decimal)payoutDistribution.OwnPayment
                }
            };
            var jsonSerializerSettings = new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() };
            var json = JsonConvert.SerializeObject(resultsData, Formatting.Indented, jsonSerializerSettings);

            return Content(json, "application/json");
        }
    }
}