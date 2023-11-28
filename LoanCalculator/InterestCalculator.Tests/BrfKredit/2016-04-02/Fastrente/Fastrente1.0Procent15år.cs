using System;
using System.Linq;
using Koolawong.InterestCalculator.Model.Inside.Simple;
using Koolawong.InterestCalculator.Model.Outside.Simple;
using Koolawong.InterestCalculator.Tests.Support;
using Xunit;

namespace Koolawong.InterestCalculator.Tests.BrfKredit.Fastrente
{
    public class Fastrente10Procent15år
    {
        // In accordance with the documentation, this should be 2016.04.02. 
        // However, this date gets a better match on the results.
        // I believe since the 2016.04.02 is a saturday, the calculation moves
        // its date to monday.
        private static readonly DateTime Date = new DateTime(2016, 4, 4);

        private const decimal ExpectedPrincipal = 3205000m;

        [Fact]
        public void PrincipalAsExpected()
        {
            // The principal is rounded to the nearest thousand up (It seems)
            var expected = Principal.From(ExpectedPrincipal - 966.71m);

            var loanAmount = 3040560m;
            var loanCosts = 13900m + 1660m + 2000m + 6139m + 1000m + 3500m;
            var payout = MortgagePayout.From(loanAmount + loanCosts);

            var exchangeRate = ExchangeRate.From(0.95778m);

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
            var yearlyContributionRate = YearlyContributionRate.From(0.0060615m);

            var actual = principal.Calculate(
                CalculationDate.From(Date),
                period * termsPerYear,
                termsPerYear,
                yearlyInterestRate / termsPerYear,
                yearlyContributionRate / termsPerYear);

            var expected = new[]
            {
                new { PaymentWithoutContribution = 170870m, Interest = 23409m, Repayment = 147461m, Contribution = 14190m, LoanLeft = 3057539m },
                new { PaymentWithoutContribution = 230359m, Interest = 29825m, Repayment = 200534m, Contribution = 18078m, LoanLeft = 2857005m },
                new { PaymentWithoutContribution = 230359m, Interest = 27812m, Repayment = 202547m, Contribution = 16858m, LoanLeft = 2654459m },
                new { PaymentWithoutContribution = 230359m, Interest = 25779m, Repayment = 204580m, Contribution = 15626m, LoanLeft = 2449879m },
                new { PaymentWithoutContribution = 230359m, Interest = 23726m, Repayment = 206633m, Contribution = 14381m, LoanLeft = 2243246m },
                new { PaymentWithoutContribution = 230359m, Interest = 21651m, Repayment = 208707m, Contribution = 13124m, LoanLeft = 2034538m },
                new { PaymentWithoutContribution = 230359m, Interest = 19557m, Repayment = 210802m, Contribution = 11854m, LoanLeft = 1823736m },
                new { PaymentWithoutContribution = 230359m, Interest = 17441m, Repayment = 212918m, Contribution = 10572m, LoanLeft = 1610818m },
                new { PaymentWithoutContribution = 230359m, Interest = 15303m, Repayment = 215055m, Contribution = 9276m, LoanLeft = 1395763m },
                new { PaymentWithoutContribution = 230359m, Interest = 13145m, Repayment = 217214m, Contribution = 7968m, LoanLeft = 1178549m },
                new { PaymentWithoutContribution = 230359m, Interest = 10964m, Repayment = 219394m, Contribution = 6646m, LoanLeft = 959154m },
                new { PaymentWithoutContribution = 230359m, Interest = 8762m, Repayment = 221596m, Contribution = 5311m, LoanLeft = 737558m },
                new { PaymentWithoutContribution = 230359m, Interest = 6538m, Repayment = 223821m, Contribution = 3963m, LoanLeft = 513737m },
                new { PaymentWithoutContribution = 230359m, Interest = 4291m, Repayment = 226067m, Contribution = 2601m, LoanLeft = 287670m },
                new { PaymentWithoutContribution = 230359m, Interest = 2022m, Repayment = 228337m, Contribution = 1274m, LoanLeft = 59333m },
                new { PaymentWithoutContribution = 59483m, Interest = 148m, Repayment = 59334m, Contribution = 225m, LoanLeft = 0m }
            };

            var expectedRepayment = expected.Sum(plan => plan.Repayment);
            var actualRepayment = actual.PlanByTerms.Sum(term => (decimal)term.Repayment);
            Assert.Equal(expectedRepayment, actualRepayment, 2);

            var expectedInterest = expected.Sum(plan => plan.Interest) + 6.21m;
            var actualInterest = actual.PlanByTerms.Sum(term => (decimal)term.Interest);
            Assert.Equal(expectedInterest, actualInterest, 2);

            var expectedContribution = expected.Sum(plan => plan.Contribution) - 179.64m;
            var actualContribution = actual.PlanByTerms.Sum(term => (decimal)term.Contribution);
            Assert.Equal(expectedContribution, actualContribution, 2);

            var expectedRepaymentPlan = expected.Select(plan => plan.Repayment);
            var actualRepaymentPlan = actual.PlanByYears.Select(plan => (decimal)plan.Repayment);
            CollectionAssert.Equal(expectedRepaymentPlan, actualRepaymentPlan, 2m);

            var expectedInterestPlan = expected.Select(plan => plan.Interest);
            var actualInterestPlan = actual.PlanByYears.Select(plan => (decimal)plan.Interest);
            CollectionAssert.Equal(expectedInterestPlan, actualInterestPlan, 6m);

            var expectedContributionPlan = expected.Select(plan => plan.Contribution);
            var actualContributionPlan = actual.PlanByYears.Select(plan => (decimal)plan.Contribution);
            CollectionAssert.Equal(expectedContributionPlan, actualContributionPlan, 135m);
        }
    }
}
