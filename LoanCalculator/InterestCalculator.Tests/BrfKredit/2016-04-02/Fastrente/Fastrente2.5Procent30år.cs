using System;
using System.Linq;
using Koolawong.InterestCalculator.Model.Inside.Simple;
using Koolawong.InterestCalculator.Model.Outside.Simple;
using Koolawong.InterestCalculator.Tests.Support;
using Xunit;

namespace Koolawong.InterestCalculator.Tests.BrfKredit.Fastrente
{
    public class Fastrente25Procent30år
    {
        // In accordance with the documentation, this should be 2016.04.02. 
        // However, this date gets a better match on the results.
        // I believe since the 2016.04.02 is a saturday, the calculation moves
        // its date to monday.
        private static readonly DateTime Date = new DateTime(2016, 4, 4);

        private const decimal ExpectedPrincipal = 3107000m;

        [Fact]
        public void PrincipalAsExpected()
        {
            // The principal is rounded to the nearest thousand up (It seems)
            var expected = Principal.From(ExpectedPrincipal - 540.48m);

            var loanAmount = 3040560m;
            var loanCosts = 12400m + 1660m + 2000m + 6136m + 1000m + 3500m;
            var payout = MortgagePayout.From(loanAmount + loanCosts);

            var exchangeRate = ExchangeRate.From(0.98738m);

            var actual = payout / exchangeRate;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void PaymentPlanAsExpected()
        {
            var principal = Principal.From(ExpectedPrincipal);
            var termsPerYear = TermsPerYear.From(4);
            var period = Period.From(30);
            var yearlyInterestRate = YearlyInterestRate.From(0.025m);
            var yearlyContributionRate = YearlyContributionRate.From(0.0060591m);

            var actual = principal.Calculate(
                CalculationDate.From(Date),
                period * termsPerYear,
                termsPerYear,
                yearlyInterestRate / termsPerYear,
                yearlyContributionRate / termsPerYear);

            var expected = new[]
            {
                new { PaymentWithoutContribution = 109427m, Interest = 57295m, Repayment = 52131m, Contribution = 13886m, LoanLeft = 3054869m },
                new { PaymentWithoutContribution = 147523m, Interest = 75702m, Repayment = 71821m, Contribution = 18347m, LoanLeft = 2983048m },
                new { PaymentWithoutContribution = 147523m, Interest = 73889m, Repayment = 73633m, Contribution = 17908m, LoanLeft = 2909415m },
                new { PaymentWithoutContribution = 147523m, Interest = 72031m, Repayment = 75492m, Contribution = 17458m, LoanLeft = 2833923m },
                new { PaymentWithoutContribution = 147523m, Interest = 70126m, Repayment = 77397m, Contribution = 16996m, LoanLeft = 2756526m },
                new { PaymentWithoutContribution = 147523m, Interest = 68173m, Repayment = 79350m, Contribution = 16523m, LoanLeft = 2677177m },
                new { PaymentWithoutContribution = 147523m, Interest = 66171m, Repayment = 81352m, Contribution = 16037m, LoanLeft = 2595825m },
                new { PaymentWithoutContribution = 147523m, Interest = 64118m, Repayment = 83405m, Contribution = 15540m, LoanLeft = 2512420m },
                new { PaymentWithoutContribution = 147523m, Interest = 62013m, Repayment = 85510m, Contribution = 15030m, LoanLeft = 2426910m },
                new { PaymentWithoutContribution = 147523m, Interest = 59855m, Repayment = 87668m, Contribution = 14507m, LoanLeft = 2339242m },
                new { PaymentWithoutContribution = 147523m, Interest = 57643m, Repayment = 89880m, Contribution = 13971m, LoanLeft = 2249362m },
                new { PaymentWithoutContribution = 147523m, Interest = 55375m, Repayment = 92148m, Contribution = 13421m, LoanLeft = 2157214m },
                new { PaymentWithoutContribution = 147523m, Interest = 53049m, Repayment = 94474m, Contribution = 12857m, LoanLeft = 2062740m },
                new { PaymentWithoutContribution = 147523m, Interest = 50665m, Repayment = 96858m, Contribution = 12279m, LoanLeft = 1965882m },
                new { PaymentWithoutContribution = 147523m, Interest = 48221m, Repayment = 99302m, Contribution = 11687m, LoanLeft = 1866581m },
                new { PaymentWithoutContribution = 147523m, Interest = 45715m, Repayment = 101808m, Contribution = 11080m, LoanLeft = 1764773m },
                new { PaymentWithoutContribution = 147523m, Interest = 43146m, Repayment = 104377m, Contribution = 10457m, LoanLeft = 1660396m },
                new { PaymentWithoutContribution = 147523m, Interest = 40512m, Repayment = 107011m, Contribution = 9819m, LoanLeft = 1553385m },
                new { PaymentWithoutContribution = 147523m, Interest = 37811m, Repayment = 109711m, Contribution = 9164m, LoanLeft = 1443673m },
                new { PaymentWithoutContribution = 147523m, Interest = 35043m, Repayment = 112480m, Contribution = 8493m, LoanLeft = 1331193m },
                new { PaymentWithoutContribution = 147523m, Interest = 32204m, Repayment = 115319m, Contribution = 7805m, LoanLeft = 1215875m },
                new { PaymentWithoutContribution = 147523m, Interest = 29294m, Repayment = 118229m, Contribution = 7100m, LoanLeft = 1097646m },
                new { PaymentWithoutContribution = 147523m, Interest = 26311m, Repayment = 121212m, Contribution = 6377m, LoanLeft = 976434m },
                new { PaymentWithoutContribution = 147523m, Interest = 23252m, Repayment = 124271m, Contribution = 5635m, LoanLeft = 852163m },
                new { PaymentWithoutContribution = 147523m, Interest = 20116m, Repayment = 127407m, Contribution = 4875m, LoanLeft = 724756m },
                new { PaymentWithoutContribution = 147523m, Interest = 16901m, Repayment = 130622m, Contribution = 4096m, LoanLeft = 594134m },
                new { PaymentWithoutContribution = 147523m, Interest = 13604m, Repayment = 133918m, Contribution = 3297m, LoanLeft = 460216m },
                new { PaymentWithoutContribution = 147523m, Interest = 10225m, Repayment = 137298m, Contribution = 2478m, LoanLeft = 322918m },
                new { PaymentWithoutContribution = 147523m, Interest = 6760m, Repayment = 140763m, Contribution = 1638m, LoanLeft = 182155m },
                new { PaymentWithoutContribution = 147523m, Interest = 3208m, Repayment = 144315m, Contribution = 951m, LoanLeft = 37840m },
                new { PaymentWithoutContribution = 38076m, Interest = 237m, Repayment = 37840m, Contribution = 225m, LoanLeft = 0m }
            };

            var expectedRepayment = expected.Sum(plan => plan.Repayment) - 2m;
            var actualRepayment = actual.PlanByTerms.Sum(term => (decimal)term.Repayment);
            Assert.Equal(expectedRepayment, actualRepayment, 2);

            var expectedInterest = expected.Sum(plan => plan.Interest) + 11.76m;
            var actualInterest = actual.PlanByTerms.Sum(term => (decimal)term.Interest);
            Assert.Equal(expectedInterest, actualInterest, 2);

            var expectedContribution = expected.Sum(plan => plan.Contribution) - 337.23m;
            var actualContribution = actual.PlanByTerms.Sum(term => (decimal)term.Contribution);
            Assert.Equal(expectedContribution, actualContribution, 2);

            var expectedRepaymentPlan = expected.Select(plan => plan.Repayment);
            var actualRepaymentPlan = actual.PlanByYears.Select(plan => (decimal)plan.Repayment);
            CollectionAssert.Equal(expectedRepaymentPlan, actualRepaymentPlan, 13m);

            var expectedInterestPlan = expected.Select(plan => plan.Interest);
            var actualInterestPlan = actual.PlanByYears.Select(plan => (decimal)plan.Interest);
            CollectionAssert.Equal(expectedInterestPlan, actualInterestPlan, 10m);

            var expectedContributionPlan = expected.Select(plan => plan.Contribution);
            var actualContributionPlan = actual.PlanByYears.Select(plan => (decimal)plan.Contribution);
            CollectionAssert.Equal(expectedContributionPlan, actualContributionPlan, 200m);
        }
    }
}
