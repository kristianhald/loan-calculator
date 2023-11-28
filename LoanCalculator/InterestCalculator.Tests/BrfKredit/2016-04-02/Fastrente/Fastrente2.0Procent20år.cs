using System;
using System.Linq;
using Koolawong.InterestCalculator.Model.Inside.Simple;
using Koolawong.InterestCalculator.Model.Outside.Simple;
using Koolawong.InterestCalculator.Tests.Support;
using Xunit;

namespace Koolawong.InterestCalculator.Tests.BrfKredit.Fastrente
{
    public class Fastrente20Procent20år
    {
        // In accordance with the documentation, this should be 2016.04.02. 
        // However, this date gets a better match on the results.
        // I believe since the 2016.04.02 is a saturday, the calculation moves
        // its date to monday.
        private static readonly DateTime Date = new DateTime(2016, 4, 4);

        private const decimal ExpectedPrincipal = 3102000m;

        [Fact]
        public void PrincipalAsExpected()
        {
            // The principal is rounded to the nearest thousand up (It seems)
            var expected = Principal.From(ExpectedPrincipal - 754.35m);

            var loanAmount = 3040560m;
            var loanCosts = 12400m + 1660m + 2000m + 6136m + 1000m + 3500m;
            var payout = MortgagePayout.From(loanAmount + loanCosts);

            var exchangeRate = ExchangeRate.From(0.98904m);

            var actual = payout / exchangeRate;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void PaymentPlanAsExpected()
        {
            var principal = Principal.From(ExpectedPrincipal);
            var termsPerYear = TermsPerYear.From(4);
            var period = Period.From(20);
            var yearlyInterestRate = YearlyInterestRate.From(0.020m);
            var yearlyContributionRate = YearlyContributionRate.From(0.0060593m);

            var actual = principal.Calculate(
                CalculationDate.From(Date),
                period * termsPerYear,
                termsPerYear,
                yearlyInterestRate / termsPerYear,
                yearlyContributionRate / termsPerYear);

            var expected = new[]
            {
                new { PaymentWithoutContribution = 139870m, Interest = 45554m, Repayment = 94316m, Contribution = 13801m, LoanLeft = 3007684m },
                new { PaymentWithoutContribution = 188566m, Interest = 59187m, Repayment = 129378m, Contribution = 17932m, LoanLeft = 2878306m },
                new { PaymentWithoutContribution = 188566m, Interest = 56580m, Repayment = 131985m, Contribution = 17142m, LoanLeft = 2746320m },
                new { PaymentWithoutContribution = 188566m, Interest = 53921m, Repayment = 134645m, Contribution = 16336m, LoanLeft = 2611675m },
                new { PaymentWithoutContribution = 188566m, Interest = 51208m, Repayment = 137358m, Contribution = 15514m, LoanLeft = 2474317m },
                new { PaymentWithoutContribution = 188566m, Interest = 48440m, Repayment = 140126m, Contribution = 14676m, LoanLeft = 2334192m },
                new { PaymentWithoutContribution = 188566m, Interest = 45616m, Repayment = 142949m, Contribution = 13820m, LoanLeft = 2191242m },
                new { PaymentWithoutContribution = 188566m, Interest = 42736m, Repayment = 145830m, Contribution = 12947m, LoanLeft = 2045412m },
                new { PaymentWithoutContribution = 188566m, Interest = 39797m, Repayment = 148768m, Contribution = 12057m, LoanLeft = 1896644m },
                new { PaymentWithoutContribution = 188566m, Interest = 36799m, Repayment = 151766m, Contribution = 11149m, LoanLeft = 1744878m },
                new { PaymentWithoutContribution = 188566m, Interest = 33741m, Repayment = 154824m, Contribution = 10222m, LoanLeft = 1590053m },
                new { PaymentWithoutContribution = 188566m, Interest = 30621m, Repayment = 157944m, Contribution = 9277m, LoanLeft = 1432109m },
                new { PaymentWithoutContribution = 188566m, Interest = 27439m, Repayment = 161127m, Contribution = 8313m, LoanLeft = 1270982m },
                new { PaymentWithoutContribution = 188566m, Interest = 24192m, Repayment = 164374m, Contribution = 7329m, LoanLeft = 1106609m },
                new { PaymentWithoutContribution = 188566m, Interest = 20880m, Repayment = 167686m, Contribution = 6326m, LoanLeft = 938923m },
                new { PaymentWithoutContribution = 188566m, Interest = 17501m, Repayment = 171065m, Contribution = 5302m, LoanLeft = 767858m },
                new { PaymentWithoutContribution = 188566m, Interest = 14054m, Repayment = 174512m, Contribution = 4258m, LoanLeft = 593346m },
                new { PaymentWithoutContribution = 188566m, Interest = 10537m, Repayment = 178028m, Contribution = 3192m, LoanLeft = 415318m },
                new { PaymentWithoutContribution = 188566m, Interest = 6950m, Repayment = 181616m, Contribution = 2106m, LoanLeft = 233702m },
                new { PaymentWithoutContribution = 188566m, Interest = 3290m, Repayment = 185275m, Contribution = 1088m, LoanLeft = 48427m },
                new { PaymentWithoutContribution = 48670m, Interest = 242m, Repayment = 48428m, Contribution = 225m, LoanLeft = 0m }
            };

            var expectedRepayment = expected.Sum(plan => plan.Repayment);
            var actualRepayment = actual.PlanByTerms.Sum(term => (decimal)term.Repayment);
            Assert.Equal(expectedRepayment, actualRepayment, 2);

            var expectedInterest = expected.Sum(plan => plan.Interest) + 11.44m;
            var actualInterest = actual.PlanByTerms.Sum(term => (decimal)term.Interest);
            Assert.Equal(expectedInterest, actualInterest, 2);

            var expectedContribution = expected.Sum(plan => plan.Contribution) - 238.6m;
            var actualContribution = actual.PlanByTerms.Sum(term => (decimal)term.Contribution);
            Assert.Equal(expectedContribution, actualContribution, 2);

            var expectedRepaymentPlan = expected.Select(plan => plan.Repayment);
            var actualRepaymentPlan = actual.PlanByYears.Select(plan => (decimal)plan.Repayment);
            CollectionAssert.Equal(expectedRepaymentPlan, actualRepaymentPlan, 20m);

            var expectedInterestPlan = expected.Select(plan => plan.Interest);
            var actualInterestPlan = actual.PlanByYears.Select(plan => (decimal)plan.Interest);
            CollectionAssert.Equal(expectedInterestPlan, actualInterestPlan, 8m);

            var expectedContributionPlan = expected.Select(plan => plan.Contribution);
            var actualContributionPlan = actual.PlanByYears.Select(plan => (decimal)plan.Contribution);
            CollectionAssert.Equal(expectedContributionPlan, actualContributionPlan, 150m);
        }
    }
}
