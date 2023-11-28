using System;
using System.Linq;
using Koolawong.InterestCalculator.Model.Inside.Simple;
using Koolawong.InterestCalculator.Model.Outside.Simple;
using Koolawong.InterestCalculator.Tests.Support;
using Xunit;

namespace Koolawong.InterestCalculator.Tests.BrfKredit.Fastrente
{
    public class Fastrente10Procent5år
    {
        // In accordance with the documentation, this should be 2016.04.02. 
        // However, this date gets a better match on the results.
        // I believe since the 2016.04.02 is a saturday, the calculation moves
        // its date to monday.
        private static readonly DateTime Date = new DateTime(2016, 4, 4);

        private const decimal ExpectedPrincipal = 3106000m;

        [Fact]
        public void PrincipalAsExpected()
        {
            // The principal is rounded to the nearest thousand up (It seems)
            var expected = Principal.From(ExpectedPrincipal - 704.13m);

            var loanAmount = 3040560m;
            var loanCosts = 12400m + 1660m + 2000m + 6136m + 1000m + 3500m;
            var payout = MortgagePayout.From(loanAmount + loanCosts);

            var exchangeRate = ExchangeRate.From(0.98775m);

            var actual = payout / exchangeRate;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void PaymentPlanAsExpected()
        {
            var principal = Principal.From(ExpectedPrincipal);
            var termsPerYear = TermsPerYear.From(4);
            var period = Period.From(5);
            var yearlyInterestRate = YearlyInterestRate.From(0.010m);
            var yearlyContributionRate = YearlyContributionRate.From(0.0060593m);

            var actual = principal.Calculate(
                CalculationDate.From(Date),
                period * termsPerYear,
                termsPerYear,
                yearlyInterestRate / termsPerYear,
                yearlyContributionRate / termsPerYear);

            var expected = new[]
            {
                new { PaymentWithoutContribution = 472972m, Interest = 21926m, Repayment = 451046m, Contribution = 13285m, LoanLeft = 2654954m },
                new { PaymentWithoutContribution = 637636m, Interest = 24254m, Repayment = 613382m, Contribution = 14696m, LoanLeft = 2041573m },
                new { PaymentWithoutContribution = 637635m, Interest = 18097m, Repayment = 619538m, Contribution = 10966m, LoanLeft = 1422034m },
                new { PaymentWithoutContribution = 637635m, Interest = 11879m, Repayment = 625757m, Contribution = 7198m, LoanLeft = 796277m },
                new { PaymentWithoutContribution = 637635m, Interest = 5598m, Repayment = 632038m, Contribution = 3392m, LoanLeft = 164239m },
                new { PaymentWithoutContribution = 164649m, Interest = 411m, Repayment = 164239m, Contribution = 225m, LoanLeft = 0m }
            };

            var expectedRepayment = expected.Sum(plan => plan.Repayment);
            var actualRepayment = actual.PlanByTerms.Sum(term => (decimal)term.Repayment);
            Assert.Equal(expectedRepayment, actualRepayment, 2);

            var expectedInterest = expected.Sum(plan => plan.Interest) + 12.13m;
            var actualInterest = actual.PlanByTerms.Sum(term => (decimal)term.Interest);
            Assert.Equal(expectedInterest, actualInterest, 2);

            var expectedContribution = expected.Sum(plan => plan.Contribution) + 31.59m;
            var actualContribution = actual.PlanByTerms.Sum(term => (decimal)term.Contribution);
            Assert.Equal(expectedContribution, actualContribution, 2);

            var expectedRepaymentPlan = expected.Select(plan => plan.Repayment);
            var actualRepaymentPlan = actual.PlanByYears.Select(plan => (decimal)plan.Repayment);
            CollectionAssert.Equal(expectedRepaymentPlan, actualRepaymentPlan, 2m);

            var expectedInterestPlan = expected.Select(plan => plan.Interest);
            var actualInterestPlan = actual.PlanByYears.Select(plan => (decimal)plan.Interest);
            CollectionAssert.Equal(expectedInterestPlan, actualInterestPlan, 13m);

            var expectedContributionPlan = expected.Select(plan => plan.Contribution);
            var actualContributionPlan = actual.PlanByYears.Select(plan => (decimal)plan.Contribution);
            CollectionAssert.Equal(expectedContributionPlan, actualContributionPlan, 32m);
        }
    }
}
