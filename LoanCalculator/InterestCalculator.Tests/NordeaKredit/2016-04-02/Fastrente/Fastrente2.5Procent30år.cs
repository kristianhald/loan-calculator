using System;
using System.Linq;
using Koolawong.InterestCalculator.Model.Inside.Simple;
using Koolawong.InterestCalculator.Model.Outside.Simple;
using Xunit;

namespace Koolawong.InterestCalculator.Tests.NordeaKredit.Fastrente
{
    public class Fastrente25Procent30år
    {
        private static readonly DateTime Date = new DateTime(2016, 4, 2);

        private const decimal ExpectedPrincipal = 3135000m;

        [Fact]
        public void PrincipalAsExpected()
        {
            // The principal is rounded to the nearest thousand up (It seems)
            var expected = Principal.From(ExpectedPrincipal - 0.61m);

            var loanAmount = 3041276m;
            var loanCosts = 57859m;
            var payout = MortgagePayout.From(loanAmount + loanCosts);

            var exchangeRate = ExchangeRate.From(0.98856m);

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

            var expectedInterestAndContribution = 1676336m - -170.3m;
            var actualInterestAndContribution = actual.PlanByTerms.Sum(term => (decimal)term.Contribution + (decimal)term.Interest);
            Assert.Equal(expectedInterestAndContribution, actualInterestAndContribution, 2);
        }
    }
}
