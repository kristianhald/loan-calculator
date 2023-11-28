using System;
using System.Linq;
using Koolawong.InterestCalculator.Model.Inside.Simple;
using Koolawong.InterestCalculator.Model.Outside.Simple;
using Xunit;

namespace Koolawong.InterestCalculator.Tests.TotalKredit
{
    public class LoanTests
    {
        public class Example1_FastrenteHøjBelåning
        {
            private static readonly DateTime Date = new DateTime(2016, 2, 26);

            private const decimal ExpectedPrincipal = 3124000m;

            [Fact]
            public void PrincipalAsExpected()
            {
                var expected = Principal.From(ExpectedPrincipal - 63.01m);

                var loanAmount = 3096098m;
                var loanCosts = 17530m;
                var payout = MortgagePayout.From(loanAmount + loanCosts);

                var exchangeRate = ExchangeRate.From(0.9967m);

                var actual = payout / exchangeRate;

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void PaymentPlanAsExpected()
            {
                var principal = Principal.From(ExpectedPrincipal);
                var termsPerYear = TermsPerYear.From(4);
                var period = Period.From(30);
                var yearlyInterestRate = YearlyInterestRate.From(0.030m);
                var yearlyContributionRate = YearlyContributionRate.From(0.0061m);

                var actual = principal.Calculate(
                    CalculationDate.From(Date),
                    period * termsPerYear,
                    termsPerYear,
                    yearlyInterestRate / termsPerYear,
                    yearlyContributionRate / termsPerYear);

                var expectedRepayment = ExpectedPrincipal;
                var actualRepayment = actual.PlanByTerms.Sum(term => (decimal)term.Repayment);
                Assert.Equal(expectedRepayment, actualRepayment, 2);

                var expectedInterestAndContribution = 1956384m - 1182.24m;
                var actualInterestAndContribution = actual.PlanByTerms.Sum(term => (decimal)term.Contribution + (decimal)term.Interest);
                Assert.Equal(expectedInterestAndContribution, actualInterestAndContribution, 2);
            }
        }

        public class Example2_F3K_HøjBelåning
        {
            private static readonly DateTime Date = new DateTime(2016, 3, 24);

            private const decimal ExpectedPrincipal = 3120000m;

            [Fact]
            public void PrincipalAsExpected()
            {
                var expected = Principal.From(ExpectedPrincipal);

                var loanAmount = 3120000m;
                var loanCosts = 0m;
                var payout = MortgagePayout.From(loanAmount + loanCosts);

                var exchangeRate = ExchangeRate.From(1.000m);

                var actual = payout / exchangeRate;

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void PaymentPlanAsExpected()
            {
                var principal = Principal.From(ExpectedPrincipal);
                var termsPerYear = TermsPerYear.From(4);
                var period = Period.From(30);
                var yearlyInterestRate = YearlyInterestRate.From(0.005732m);
                var yearlyContributionRate = YearlyContributionRate.From(0.0075m);

                var actual = principal.Calculate(
                    CalculationDate.From(Date),
                    period * termsPerYear,
                    termsPerYear,
                    yearlyInterestRate / termsPerYear,
                    yearlyContributionRate / termsPerYear);

                var expectedRepayment = ExpectedPrincipal;
                var actualRepayment = actual.PlanByTerms.Sum(term => (decimal)term.Repayment);
                Assert.Equal(expectedRepayment, actualRepayment, 2);

                // Very large difference. Probably refinancing taking into account
                var expectedInterestAndContribution = 688188m - 46044.65m;
                var actualInterestAndContribution = actual.PlanByTerms.Sum(term => (decimal)term.Contribution + (decimal)term.Interest);
                Assert.Equal(expectedInterestAndContribution, actualInterestAndContribution, 2);
            }
        }
    }
}