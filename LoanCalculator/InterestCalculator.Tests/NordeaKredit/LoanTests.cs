using System;
using System.Linq;
using Koolawong.InterestCalculator.Model.Inside.Simple;
using Koolawong.InterestCalculator.Model.Outside.Simple;
using Xunit;

namespace Koolawong.InterestCalculator.Tests.NordeaKredit
{
    public class LoanTests
    {
        public class Example1_FastrenteAfdrag_HøjBelåning
        {
            private static readonly DateTime Date = new DateTime(2016, 3, 20);

            private const decimal ExpectedPrincipal = 3238000m;

            [Fact]
            public void PrincipalAsExpected()
            {
                var expected = Principal.From(ExpectedPrincipal);

                var loanAmount = 3105720m;
                var loanCosts = 59425m;
                var payout = MortgagePayout.From(loanAmount + loanCosts);

                var exchangeRate = ExchangeRate.From(0.97750m);

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

                var expectedInterestAndContribution = 1733872m - 2284.31m;
                var actualInterestAndContribution = actual.PlanByTerms.Sum(term => (decimal)term.Contribution + (decimal)term.Interest);
                Assert.Equal(expectedInterestAndContribution, actualInterestAndContribution, 2);
            }
        }

        public class Example2_F3Afdrag_HøjBelåning
        {
            private static readonly DateTime Date = new DateTime(2016, 3, 24);

            private const decimal ExpectedPrincipal = 3179000m;

            [Fact]
            public void PrincipalAsExpected()
            {
                var expected = Principal.From(ExpectedPrincipal);

                var loanAmount = 3120461m;
                var loanCosts = 58539m;
                var payout = MortgagePayout.From(loanAmount + loanCosts);

                var exchangeRate = ExchangeRate.From(1.00m);

                var actual = payout / exchangeRate;

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void PaymentPlanAsExpected()
            {
                var principal = Principal.From(ExpectedPrincipal);
                var termsPerYear = TermsPerYear.From(4);
                var period = Period.From(30);
                var yearlyInterestRate = YearlyInterestRate.From(0.0021m);
                var yearlyContributionRate = YearlyContributionRate.From(0.0080m);

                var actual = principal.Calculate(
                    CalculationDate.From(Date),
                    period * termsPerYear,
                    termsPerYear,
                    yearlyInterestRate / termsPerYear,
                    yearlyContributionRate / termsPerYear);

                var expectedRepayment = ExpectedPrincipal;
                var actualRepayment = actual.PlanByTerms.Sum(term => (decimal)term.Repayment);
                Assert.Equal(expectedRepayment, actualRepayment, 2);

                // This is a very large difference. Probably refinancing.
                var expectedInterestAndContribution = 545682m - 54995.03m;
                var actualInterestAndContribution = actual.PlanByTerms.Sum(term => (decimal)term.Contribution + (decimal)term.Interest);
                Assert.Equal(expectedInterestAndContribution, actualInterestAndContribution, 2);
            }
        }
    }
}