using System;
using System.Linq;
using Koolawong.InterestCalculator.Model.Inside.Simple;
using Koolawong.InterestCalculator.Model.Outside.Simple;
using Xunit;

namespace Koolawong.InterestCalculator.Tests.NordeaKredit.Fastrente
{
    public class Fastrente20Procent20år
    {
        private static readonly DateTime Date = new DateTime(2016, 4, 2);

        private const decimal ExpectedPrincipal = 3130000m;

        [Fact]
        public void PrincipalAsExpected()
        {
            // The principal is rounded to the nearest thousand up (It seems)
            var expected = Principal.From(ExpectedPrincipal - 0.91m);

            var loanAmount = 3040722m;
            var loanCosts = 57758m;
            var payout = MortgagePayout.From(loanAmount + loanCosts);

            var exchangeRate = ExchangeRate.From(0.98993m);

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
            var yearlyContributionRate = YearlyContributionRate.From(0.0065m);

            var actual = principal.Calculate(
                CalculationDate.From(Date),
                period * termsPerYear,
                termsPerYear,
                yearlyInterestRate / termsPerYear,
                yearlyContributionRate / termsPerYear);

            var expectedRepayment = ExpectedPrincipal;
            var actualRepayment = actual.PlanByTerms.Sum(term => (decimal)term.Repayment);
            Assert.Equal(expectedRepayment, actualRepayment, 2);

            var expectedInterestAndContribution = 894692m + 130.58m;
            var actualInterestAndContribution = actual.PlanByTerms.Sum(term => (decimal)term.Contribution + (decimal)term.Interest);
            Assert.Equal(expectedInterestAndContribution, actualInterestAndContribution, 2);
        }
    }
}
