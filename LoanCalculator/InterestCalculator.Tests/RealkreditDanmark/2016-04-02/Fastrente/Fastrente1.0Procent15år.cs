using System;
using System.Linq;
using Koolawong.InterestCalculator.Model.Inside.Simple;
using Koolawong.InterestCalculator.Model.Outside.Simple;
using Koolawong.InterestCalculator.Tests.Support;
using Xunit;

namespace Koolawong.InterestCalculator.Tests.RealkreditDanmark.Fastrente
{
    public class Fastrente10Procent15år
    {
        private static readonly DateTime Date = new DateTime(2016, 4, 2);

        private const decimal ExpectedPrincipal = 3222000m;

        [Fact]
        public void PrincipalAsExpected()
        {
            var expected = Principal.From(ExpectedPrincipal);

            var loanAmount = 3041050.36m;
            var loanCosts = 57708.14m;
            var payout = MortgagePayout.From(loanAmount + loanCosts);

            var exchangeRate = ExchangeRate.From(0.96175m);

            var actual = payout / exchangeRate;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void PaymentPlanAsExpected()
        {
            var principal = Principal.From(ExpectedPrincipal);
            var termsPerYear = TermsPerYear.From(4);
            var period = Period.From(15);
            var yearlyInterestRate = YearlyInterestRate.From(0.010m);
            var yearlyContributionRate = YearlyContributionRate.From(0.006812m);

            var actual = principal.Calculate(
                CalculationDate.From(Date),
                period * termsPerYear,
                termsPerYear,
                yearlyInterestRate / termsPerYear,
                yearlyContributionRate / termsPerYear);

            var expected = new[]
            {
                new { PaymentWithContribution = 189200m, InterestAndContribution = 39853m, Repayment = 149347m, LoanLeft = 3072653m },
                new { PaymentWithContribution = 252001m, InterestAndContribution = 50389m, Repayment = 201612m, LoanLeft = 2871041m },
                new { PaymentWithContribution = 250623m, InterestAndContribution = 46987m, Repayment = 203636m, LoanLeft = 2667404m },
                new { PaymentWithContribution = 249231m, InterestAndContribution = 43550m, Repayment = 205680m, LoanLeft = 2461724m },
                new { PaymentWithContribution = 247824m, InterestAndContribution = 40080m, Repayment = 207745m, LoanLeft = 2253980m },
                new { PaymentWithContribution = 246404m, InterestAndContribution = 36574m, Repayment = 209830m, LoanLeft = 2044150m },
                new { PaymentWithContribution = 244969m, InterestAndContribution = 33033m, Repayment = 211936m, LoanLeft = 1832214m },
                new { PaymentWithContribution = 243520m, InterestAndContribution = 29456m, Repayment = 214063m, LoanLeft = 1618150m },
                new { PaymentWithContribution = 242056m, InterestAndContribution = 25844m, Repayment = 216212m, LoanLeft = 1401938m },
                new { PaymentWithContribution = 240578m, InterestAndContribution = 22195m, Repayment = 218382m, LoanLeft = 1183556m },
                new { PaymentWithContribution = 239085m, InterestAndContribution = 18510m, Repayment = 220574m, LoanLeft = 962981m },
                new { PaymentWithContribution = 237576m, InterestAndContribution = 14788m, Repayment = 222788m, LoanLeft = 740193m },
                new { PaymentWithContribution = 236053m, InterestAndContribution = 11028m, Repayment = 225025m, LoanLeft = 515168m },
                new { PaymentWithContribution = 234514m, InterestAndContribution = 7231m, Repayment = 227283m, LoanLeft = 287885m },
                new { PaymentWithContribution = 232963m, InterestAndContribution = 3398m, Repayment = 229565m, LoanLeft = 58320m },
                new { PaymentWithContribution = 58733m, InterestAndContribution = 412m, Repayment = 58320m, LoanLeft = 0.0m },
            };

            var expectedRepayment = expected.Sum(plan => plan.Repayment) + 2m;
            var actualRepayment = actual.PlanByTerms.Sum(term => (decimal)term.Repayment);
            Assert.Equal(expectedRepayment, actualRepayment, 2);

            var expectedInterestAndContribution = expected.Sum(plan => plan.InterestAndContribution) - 157.72m;
            var actualInterestAndContribution = actual.PlanByTerms.Sum(term => (decimal)term.Interest + (decimal)term.Contribution);
            Assert.Equal(expectedInterestAndContribution, actualInterestAndContribution, 2);

            var expectedRepaymentPlan = expected.Select(plan => plan.Repayment);
            var actualRepaymentPlan = actual.PlanByYears.Select(plan => (decimal)plan.Repayment);
            CollectionAssert.Equal(expectedRepaymentPlan, actualRepaymentPlan, 65m);

            var expectedInterestContributionPlan = expected.Select(plan => plan.InterestAndContribution);
            var actualInterestContributionPlan = actual.PlanByYears.Select(plan => (decimal)plan.Interest + (decimal)plan.Contribution);
            CollectionAssert.Equal(expectedInterestContributionPlan, actualInterestContributionPlan, 220m);
        }
    }
}
