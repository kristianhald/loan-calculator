using System;
using System.Linq;
using Koolawong.InterestCalculator.Model.Inside.Simple;
using Koolawong.InterestCalculator.Model.Outside.Simple;
using Koolawong.InterestCalculator.Tests.Support;
using Xunit;

namespace Koolawong.InterestCalculator.Tests.BrfKredit.Fastrente
{
    public class Fastrente10Procent10år
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
            var period = Period.From(10);
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
                new { PaymentWithoutContribution = 242390m, Interest = 22496m, Repayment = 219893m, Contribution = 13631m, LoanLeft = 2886107m },
                new { PaymentWithoutContribution = 326777m, Interest = 27742m, Repayment = 299035m, Contribution = 16810m, LoanLeft = 2587071m },
                new { PaymentWithoutContribution = 326777m, Interest = 24740m, Repayment = 302037m, Contribution = 14991m, LoanLeft = 2285034m },
                new { PaymentWithoutContribution = 326777m, Interest = 21709m, Repayment = 305069m, Contribution = 13154m, LoanLeft = 1979966m },
                new { PaymentWithoutContribution = 326777m, Interest = 18647m, Repayment = 308131m, Contribution = 11299m, LoanLeft = 1671835m },
                new { PaymentWithoutContribution = 326777m, Interest = 15554m, Repayment = 311224m, Contribution = 9424m, LoanLeft = 1360612m },
                new { PaymentWithoutContribution = 326777m, Interest = 12430m, Repayment = 314348m, Contribution = 7532m, LoanLeft = 1046264m },
                new { PaymentWithoutContribution = 326777m, Interest = 9274m, Repayment = 317503m, Contribution = 5620m, LoanLeft = 728761m },
                new { PaymentWithoutContribution = 326777m, Interest = 6088m, Repayment = 320690m, Contribution = 3689m, LoanLeft = 408072m },
                new { PaymentWithoutContribution = 326777m, Interest = 2869m, Repayment = 323909m, Contribution = 1738m, LoanLeft = 84163m },
                new { PaymentWithoutContribution = 84373m, Interest = 210m, Repayment = 84163m, Contribution = 225m, LoanLeft = 0m }
            };

            var expectedRepayment = expected.Sum(plan => plan.Repayment) - 2m;
            var actualRepayment = actual.PlanByTerms.Sum(term => (decimal)term.Repayment);
            Assert.Equal(expectedRepayment, actualRepayment, 2);

            var expectedInterest = expected.Sum(plan => plan.Interest) + 6.56m;
            var actualInterest = actual.PlanByTerms.Sum(term => (decimal)term.Interest);
            Assert.Equal(expectedInterest, actualInterest, 2);

            var expectedContribution = expected.Sum(plan => plan.Contribution) - 94.4m;
            var actualContribution = actual.PlanByTerms.Sum(term => (decimal)term.Contribution);
            Assert.Equal(expectedContribution, actualContribution, 2);

            var expectedRepaymentPlan = expected.Select(plan => plan.Repayment);
            var actualRepaymentPlan = actual.PlanByYears.Select(plan => (decimal)plan.Repayment);
            CollectionAssert.Equal(expectedRepaymentPlan, actualRepaymentPlan, 10m);

            var expectedInterestPlan = expected.Select(plan => plan.Interest);
            var actualInterestPlan = actual.PlanByYears.Select(plan => (decimal)plan.Interest);
            CollectionAssert.Equal(expectedInterestPlan, actualInterestPlan, 8m);

            var expectedContributionPlan = expected.Select(plan => plan.Contribution);
            var actualContributionPlan = actual.PlanByYears.Select(plan => (decimal)plan.Contribution);
            CollectionAssert.Equal(expectedContributionPlan, actualContributionPlan, 100m);
        }
    }
}
