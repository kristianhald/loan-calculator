using System;
using System.Linq;
using Koolawong.InterestCalculator.Model.Inside.Simple;
using Koolawong.InterestCalculator.Model.Outside.Simple;
using Koolawong.InterestCalculator.Tests.Support;
using Xunit;

namespace Koolawong.InterestCalculator.Tests.RealkreditDanmark.Fastrente
{
    public class Fastrente20Procent15år
    {
        private static readonly DateTime Date = new DateTime(2016, 4, 2);

        private const decimal ExpectedPrincipal = 3077000m;

        [Fact]
        public void PrincipalAsExpected()
        {
            var expected = Principal.From(ExpectedPrincipal);

            var loanAmount = 3041495m;
            var loanCosts = 55505.50m;
            var payout = MortgagePayout.From(loanAmount + loanCosts);

            var exchangeRate = ExchangeRate.From(1.00650m);

            var actual = payout / exchangeRate;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void PaymentPlanAsExpected()
        {
            var principal = Principal.From(ExpectedPrincipal);
            var termsPerYear = TermsPerYear.From(4);
            var period = Period.From(15);
            var yearlyInterestRate = YearlyInterestRate.From(0.020m);
            var yearlyContributionRate = YearlyContributionRate.From(0.006812m);

            var actual = principal.Calculate(
                CalculationDate.From(Date),
                period * termsPerYear,
                termsPerYear,
                yearlyInterestRate / termsPerYear,
                yearlyContributionRate / termsPerYear);

            var expected = new[]
            {
                new { PaymentWithContribution = 193248m, InterestAndContribution = 60767m, Repayment = 132481m, LoanLeft = 2944519m },
                new { PaymentWithContribution = 257550m, InterestAndContribution = 77142m, Repayment = 180408m, LoanLeft = 2764111m },
                new { PaymentWithContribution = 256312m, InterestAndContribution = 72269m, Repayment = 184043m, LoanLeft = 2580068m },
                new { PaymentWithContribution = 255048m, InterestAndContribution = 67297m, Repayment = 187751m, LoanLeft = 2392317m },
                new { PaymentWithContribution = 253760m, InterestAndContribution = 62225m, Repayment = 191535m, LoanLeft = 2200782m },
                new { PaymentWithContribution = 252445m, InterestAndContribution = 57051m, Repayment = 195394m, LoanLeft = 2005388m },
                new { PaymentWithContribution = 251104m, InterestAndContribution = 51773m, Repayment = 199332m, LoanLeft = 1806056m },
                new { PaymentWithContribution = 249736m, InterestAndContribution = 46388m, Repayment = 203348m, LoanLeft = 1602708m },
                new { PaymentWithContribution = 248340m, InterestAndContribution = 40895m, Repayment = 207446m, LoanLeft = 1395262m },
                new { PaymentWithContribution = 246917m, InterestAndContribution = 35291m, Repayment = 211626m, LoanLeft = 1183636m },
                new { PaymentWithContribution = 245464m, InterestAndContribution = 29574m, Repayment = 215890m, LoanLeft = 967746m },
                new { PaymentWithContribution = 243983m, InterestAndContribution = 23742m, Repayment = 220241m, LoanLeft = 747506m },
                new { PaymentWithContribution = 242471m, InterestAndContribution = 17792m, Repayment = 224679m, LoanLeft = 522827m },
                new { PaymentWithContribution = 240929m, InterestAndContribution = 11723m, Repayment = 229206m, LoanLeft = 293621m },
                new { PaymentWithContribution = 239356m, InterestAndContribution = 5531m, Repayment = 233825m, LoanLeft = 59797m },
                new { PaymentWithContribution = 60362m, InterestAndContribution = 566m, Repayment = 59797m, LoanLeft = 0.0m },
            };

            var expectedRepayment = expected.Sum(plan => plan.Repayment) - 2m;
            var actualRepayment = actual.PlanByTerms.Sum(term => (decimal)term.Repayment);
            Assert.Equal(expectedRepayment, actualRepayment, 2);

            var expectedInterestAndContribution = expected.Sum(plan => plan.InterestAndContribution) - 153.43m;
            var actualInterestAndContribution = actual.PlanByTerms.Sum(term => (decimal)term.Interest + (decimal)term.Contribution);
            Assert.Equal(expectedInterestAndContribution, actualInterestAndContribution, 2);

            var expectedRepaymentPlan = expected.Select(plan => plan.Repayment);
            var actualRepaymentPlan = actual.PlanByYears.Select(plan => (decimal)plan.Repayment);
            CollectionAssert.Equal(expectedRepaymentPlan, actualRepaymentPlan, 60m);

            var expectedInterestContributionPlan = expected.Select(plan => plan.InterestAndContribution);
            var actualInterestContributionPlan = actual.PlanByYears.Select(plan => (decimal)plan.Interest + (decimal)plan.Contribution);
            CollectionAssert.Equal(expectedInterestContributionPlan, actualInterestContributionPlan, 161m);
        }
    }
}
