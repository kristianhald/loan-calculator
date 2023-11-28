using System;
using System.Linq;
using Koolawong.InterestCalculator.Model.Inside.Simple;
using Koolawong.InterestCalculator.Model.Outside.Simple;
using Koolawong.InterestCalculator.Tests.Support;
using Xunit;

namespace Koolawong.InterestCalculator.Tests.RealkreditDanmark.Fastrente
{
    public class Fastrente10Procent10år
    {
        private static readonly DateTime Date = new DateTime(2016, 4, 2);

        private const decimal ExpectedPrincipal = 3123000m;

        [Fact]
        public void PrincipalAsExpected()
        {
            var expected = Principal.From(ExpectedPrincipal);

            var loanAmount = 3040717.57m;
            var loanCosts = 56205.38m;
            var payout = MortgagePayout.From(loanAmount + loanCosts);

            var exchangeRate = ExchangeRate.From(0.99165m);

            var actual = payout / exchangeRate;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void PaymentPlanAsExpected()
        {
            var houseValue = HouseValue.From(3900000m);
            var principal = Principal.From(ExpectedPrincipal);
            var termsPerYear = TermsPerYear.From(4);
            var period = Period.From(10);
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
                new { PaymentWithContribution = 261050m, InterestAndContribution = 38302m, Repayment = 222747m, LoanLeft = 2900253m },
                new { PaymentWithContribution = 347567m, InterestAndContribution = 46867m, Repayment = 300700m, LoanLeft = 2599553m },
                new { PaymentWithContribution = 345511m, InterestAndContribution = 41793m, Repayment = 303718m, LoanLeft = 2295835m },
                new { PaymentWithContribution = 343434m, InterestAndContribution = 36668m, Repayment = 306767m, LoanLeft = 1989068m },
                new { PaymentWithContribution = 341337m, InterestAndContribution = 31491m, Repayment = 309846m, LoanLeft = 1679222m },
                new { PaymentWithContribution = 339218m, InterestAndContribution = 26262m, Repayment = 312956m, LoanLeft = 1366266m },
                new { PaymentWithContribution = 337078m, InterestAndContribution = 20981m, Repayment = 316097m, LoanLeft = 1050169m },
                new { PaymentWithContribution = 334917m, InterestAndContribution = 15647m, Repayment = 319270m, LoanLeft = 730899m },
                new { PaymentWithContribution = 332734m, InterestAndContribution = 10259m, Repayment = 322475m, LoanLeft = 408424m },
                new { PaymentWithContribution = 330529m, InterestAndContribution = 4817m, Repayment = 325712m, LoanLeft = 82712m },
                new { PaymentWithContribution = 83185m, InterestAndContribution = 473m, Repayment = 82712m, LoanLeft = 0.0m },
            };

            var expectedRepayment = expected.Sum(plan => plan.Repayment);
            var actualRepayment = actual.PlanByTerms.Sum(term => (decimal)term.Repayment);
            Assert.Equal(expectedRepayment, actualRepayment, 2);

            var expectedInterestAndContribution = expected.Sum(plan => plan.InterestAndContribution) - 111.23m;
            var actualInterestAndContribution = actual.PlanByTerms.Sum(term => (decimal)term.Interest + (decimal)term.Contribution);
            Assert.Equal(expectedInterestAndContribution, actualInterestAndContribution, 2);

            var expectedRepaymentPlan = expected.Select(plan => plan.Repayment);
            var actualRepaymentPlan = actual.PlanByYears.Select(plan => (decimal)plan.Repayment);
            CollectionAssert.Equal(expectedRepaymentPlan, actualRepaymentPlan, 125m);

            var expectedInterestContributionPlan = expected.Select(plan => plan.InterestAndContribution);
            var actualInterestContributionPlan = actual.PlanByYears.Select(plan => (decimal)plan.Interest + (decimal)plan.Contribution);
            CollectionAssert.Equal(expectedInterestContributionPlan, actualInterestContributionPlan, 125m);
        }
    }
}
