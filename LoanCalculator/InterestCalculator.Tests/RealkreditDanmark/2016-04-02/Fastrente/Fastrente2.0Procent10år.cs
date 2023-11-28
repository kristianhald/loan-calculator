using System;
using System.Linq;
using Koolawong.InterestCalculator.Model.Inside.Simple;
using Koolawong.InterestCalculator.Model.Outside.Simple;
using Koolawong.InterestCalculator.Tests.Support;
using Xunit;

namespace Koolawong.InterestCalculator.Tests.RealkreditDanmark.Fastrente
{
    public class Fastrente20Procent10år
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
            var period = Period.From(10);
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
                new { PaymentWithContribution = 269574m, InterestAndContribution = 60255m, Repayment = 209319m, LoanLeft = 2867681m },
                new { PaymentWithContribution = 359076m, InterestAndContribution = 74034m, Repayment = 285042m, LoanLeft = 2582639m },
                new { PaymentWithContribution = 357120m, InterestAndContribution = 66334m, Repayment = 290786m, LoanLeft = 2291853m },
                new { PaymentWithContribution = 355124m, InterestAndContribution = 58479m, Repayment = 296646m, LoanLeft = 1995207m },
                new { PaymentWithContribution = 353088m, InterestAndContribution = 50465m, Repayment = 302623m, LoanLeft = 1692584m },
                new { PaymentWithContribution = 351011m, InterestAndContribution = 42290m, Repayment = 308721m, LoanLeft = 1383863m },
                new { PaymentWithContribution = 348893m, InterestAndContribution = 33951m, Repayment = 314942m, LoanLeft = 1068921m },
                new { PaymentWithContribution = 346731m, InterestAndContribution = 25443m, Repayment = 321288m, LoanLeft = 747633m },
                new { PaymentWithContribution = 344526m, InterestAndContribution = 16764m, Repayment = 327762m, LoanLeft = 419871m },
                new { PaymentWithContribution = 342277m, InterestAndContribution = 7910m, Repayment = 334367m, LoanLeft = 85504m },
                new { PaymentWithContribution = 86336m, InterestAndContribution = 832m, Repayment = 85504m, LoanLeft = 0.0m },
            };

            var expectedRepayment = expected.Sum(plan => plan.Repayment);
            var actualRepayment = actual.PlanByTerms.Sum(term => (decimal)term.Repayment);
            Assert.Equal(expectedRepayment, actualRepayment, 2);

            var expectedInterestAndContribution = expected.Sum(plan => plan.InterestAndContribution) - 243.65m;
            var actualInterestAndContribution = actual.PlanByTerms.Sum(term => (decimal)term.Interest + (decimal)term.Contribution);
            Assert.Equal(expectedInterestAndContribution, actualInterestAndContribution, 2);

            var expectedRepaymentPlan = expected.Select(plan => plan.Repayment);
            var actualRepaymentPlan = actual.PlanByYears.Select(plan => (decimal)plan.Repayment);
            CollectionAssert.Equal(expectedRepaymentPlan, actualRepaymentPlan, 65m);

            var expectedInterestContributionPlan = expected.Select(plan => plan.InterestAndContribution);
            var actualInterestContributionPlan = actual.PlanByYears.Select(plan => (decimal)plan.Interest + (decimal)plan.Contribution);
            CollectionAssert.Equal(expectedInterestContributionPlan, actualInterestContributionPlan, 270m);
        }
    }
}
