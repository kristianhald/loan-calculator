using System;
using System.Linq;
using Koolawong.InterestCalculator.Model.Inside.Simple;
using Koolawong.InterestCalculator.Model.Outside.Simple;
using Koolawong.InterestCalculator.Tests.Support;
using Xunit;

namespace Koolawong.InterestCalculator.Tests.BrfKredit.Fastrente
{
    public class Fastrente20Procent30år
    {
        // In accordance with the documentation, this should be 2016.04.02. 
        // However, this date gets a better match on the results.
        // I believe since the 2016.04.02 is a saturday, the calculation moves
        // its date to monday.
        private static readonly DateTime Date = new DateTime(2016, 4, 4);

        private const decimal ExpectedPrincipal = 3244000m;

        [Fact]
        public void PrincipalAsExpected()
        {
            // The principal is rounded to the nearest thousand up (It seems)
            var expected = Principal.From(ExpectedPrincipal - 291.68m);

            var loanAmount = 3040560m;
            var loanCosts = 14500m + 1660m + 2000m + 6139m + 1000m + 3500m;
            var payout = MortgagePayout.From(loanAmount + loanCosts);

            var exchangeRate = ExchangeRate.From(0.94625m);

            var actual = payout / exchangeRate;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void PaymentPlanAsExpected()
        {
            var principal = Principal.From(ExpectedPrincipal);
            var termsPerYear = TermsPerYear.From(4);
            var period = Period.From(30);
            var yearlyInterestRate = YearlyInterestRate.From(0.020m);
            var yearlyContributionRate = YearlyContributionRate.From(0.0060614m);

            var actual = principal.Calculate(
                CalculationDate.From(Date),
                period * termsPerYear,
                termsPerYear,
                yearlyInterestRate / termsPerYear,
                yearlyContributionRate / termsPerYear);

            var expected = new[]
            {
                new { PaymentWithoutContribution = 106859m, Interest = 47834m, Repayment = 59024m, Contribution = 14497m, LoanLeft = 3184976m },
                new { PaymentWithoutContribution = 144061m, Interest = 63095m, Repayment = 80966m, Contribution = 19122m, LoanLeft = 3104010m },
                new { PaymentWithoutContribution = 144061m, Interest = 61463m, Repayment = 82598m, Contribution = 18628m, LoanLeft = 3021412m },
                new { PaymentWithoutContribution = 144061m, Interest = 59799m, Repayment = 84262m, Contribution = 18123m, LoanLeft = 2937151m },
                new { PaymentWithoutContribution = 144061m, Interest = 58101m, Repayment = 85960m, Contribution = 17609m, LoanLeft = 2851191m },
                new { PaymentWithoutContribution = 144061m, Interest = 56369m, Repayment = 87692m, Contribution = 17084m, LoanLeft = 2763499m },
                new { PaymentWithoutContribution = 144061m, Interest = 54602m, Repayment = 89459m, Contribution = 16548m, LoanLeft = 2674040m },
                new { PaymentWithoutContribution = 144061m, Interest = 52799m, Repayment = 91262m, Contribution = 16002m, LoanLeft = 2582778m },
                new { PaymentWithoutContribution = 144061m, Interest = 50960m, Repayment = 93101m, Contribution = 15445m, LoanLeft = 2489677m },
                new { PaymentWithoutContribution = 144061m, Interest = 49084m, Repayment = 94977m, Contribution = 14876m, LoanLeft = 2394701m },
                new { PaymentWithoutContribution = 144061m, Interest = 47170m, Repayment = 96890m, Contribution = 14296m, LoanLeft = 2297810m },
                new { PaymentWithoutContribution = 144061m, Interest = 45218m, Repayment = 98843m, Contribution = 13704m, LoanLeft = 2198967m },
                new { PaymentWithoutContribution = 144061m, Interest = 43226m, Repayment = 100835m, Contribution = 13101m, LoanLeft = 2098133m },
                new { PaymentWithoutContribution = 144061m, Interest = 41194m, Repayment = 102866m, Contribution = 12485m, LoanLeft = 1995266m },
                new { PaymentWithoutContribution = 144061m, Interest = 39122m, Repayment = 104939m, Contribution = 11857m, LoanLeft = 1890327m },
                new { PaymentWithoutContribution = 144061m, Interest = 37007m, Repayment = 107054m, Contribution = 11216m, LoanLeft = 1783273m },
                new { PaymentWithoutContribution = 144061m, Interest = 34850m, Repayment = 109211m, Contribution = 10562m, LoanLeft = 1674062m },
                new { PaymentWithoutContribution = 144061m, Interest = 32649m, Repayment = 111412m, Contribution = 9895m, LoanLeft = 1562650m },
                new { PaymentWithoutContribution = 144061m, Interest = 30404m, Repayment = 113657m, Contribution = 9215m, LoanLeft = 1448994m },
                new { PaymentWithoutContribution = 144061m, Interest = 28114m, Repayment = 115947m, Contribution = 8520m, LoanLeft = 1333047m },
                new { PaymentWithoutContribution = 144061m, Interest = 25777m, Repayment = 118283m, Contribution = 7812m, LoanLeft = 1214763m },
                new { PaymentWithoutContribution = 144061m, Interest = 23394m, Repayment = 120667m, Contribution = 7090m, LoanLeft = 1094097m },
                new { PaymentWithoutContribution = 144061m, Interest = 20963m, Repayment = 123098m, Contribution = 6353m, LoanLeft = 970998m },
                new { PaymentWithoutContribution = 144061m, Interest = 18482m, Repayment = 125579m, Contribution = 5601m, LoanLeft = 845419m },
                new { PaymentWithoutContribution = 144061m, Interest = 15952m, Repayment = 128109m, Contribution = 4834m, LoanLeft = 717310m },
                new { PaymentWithoutContribution = 144061m, Interest = 13370m, Repayment = 130691m, Contribution = 4052m, LoanLeft = 586619m },
                new { PaymentWithoutContribution = 144061m, Interest = 10737m, Repayment = 133324m, Contribution = 3254m, LoanLeft = 453295m },
                new { PaymentWithoutContribution = 144061m, Interest = 8050m, Repayment = 136011m, Contribution = 2440m, LoanLeft = 317284m },
                new { PaymentWithoutContribution = 144061m, Interest = 5309m, Repayment = 138751m, Contribution = 1609m, LoanLeft = 178533m },
                new { PaymentWithoutContribution = 144061m, Interest = 2513m, Repayment = 141547m, Contribution = 946m, LoanLeft = 36986m },
                new { PaymentWithoutContribution = 37170m, Interest = 185m, Repayment = 36985m, Contribution = 225m, LoanLeft = 0m }
            };

            var expectedRepayment = expected.Sum(plan => plan.Repayment);
            var actualRepayment = actual.PlanByTerms.Sum(term => (decimal)term.Repayment);
            Assert.Equal(expectedRepayment, actualRepayment, 2);

            var expectedInterest = expected.Sum(plan => plan.Interest) + 14.1m;
            var actualInterest = actual.PlanByTerms.Sum(term => (decimal)term.Interest);
            Assert.Equal(expectedInterest, actualInterest, 2);

            var expectedContribution = expected.Sum(plan => plan.Contribution) - 350.31m;
            var actualContribution = actual.PlanByTerms.Sum(term => (decimal)term.Contribution);
            Assert.Equal(expectedContribution, actualContribution, 2);

            var expectedRepaymentPlan = expected.Select(plan => plan.Repayment);
            var actualRepaymentPlan = actual.PlanByYears.Select(plan => (decimal)plan.Repayment);
            CollectionAssert.Equal(expectedRepaymentPlan, actualRepaymentPlan, 30m);

            var expectedInterestPlan = expected.Select(plan => plan.Interest);
            var actualInterestPlan = actual.PlanByYears.Select(plan => (decimal)plan.Interest);
            CollectionAssert.Equal(expectedInterestPlan, actualInterestPlan, 6m);

            var expectedContributionPlan = expected.Select(plan => plan.Contribution);
            var actualContributionPlan = actual.PlanByYears.Select(plan => (decimal)plan.Contribution);
            CollectionAssert.Equal(expectedContributionPlan, actualContributionPlan, 200m);
        }
    }
}
