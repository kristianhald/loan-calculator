using Koolawong.InterestCalculator.Tests.Support;
using System;
using System.Linq;
using Koolawong.InterestCalculator.Model.Inside.Simple;
using Koolawong.InterestCalculator.Model.Outside.Simple;
using Xunit;

namespace Koolawong.InterestCalculator.Tests.RealkreditDanmark
{
    public class LoanTests
    {
        public class Example1_Fastrente_HøjBelåning
        {
            private static readonly DateTime Date = new DateTime(2016, 3, 2);

            private const decimal ExpectedPrincipal = 3195000m;

            [Fact]
            public void PrincipalAsExpected()
            {
                var expected = Principal.From(ExpectedPrincipal + 0.41m);

                var loanAmount = 3104512m;
                var loanCosts = 12786m;
                var payout = MortgagePayout.From(loanAmount + loanCosts);

                var exchangeRate = ExchangeRate.From(0.97568m);

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
                var yearlyContributionRate = YearlyContributionRate.From(0.006812m);

                var actual = principal.Calculate(
                    CalculationDate.From(Date),
                    period * termsPerYear,
                    termsPerYear,
                    yearlyInterestRate / termsPerYear,
                    yearlyContributionRate / termsPerYear);

                var expectedRepayment = ExpectedPrincipal;
                var actualRepayment = actual.PlanByTerms.Sum(term => (decimal)term.Repayment);
                Assert.Equal(expectedRepayment, actualRepayment, 2);

                var expectedInterestAndContribution = 1725702m - 186.25m;
                var actualInterestAndContribution = actual.PlanByTerms.Sum(term => (decimal)term.Contribution + (decimal)term.Interest);
                Assert.Equal(expectedInterestAndContribution, actualInterestAndContribution, 2);
            }
        }

        public class Example2_F5K_HøjBelåning
        {
            private static readonly DateTime Date = new DateTime(2016, 3, 24);

            private const decimal ExpectedPrincipal = 3120000m;

            [Fact]
            public void PrincipalAsExpected()
            {
                var expected = Principal.From(ExpectedPrincipal);

                var loanAmount = 3063860m;
                var loanCosts = 56140m;
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
                var yearlyInterestRate = YearlyInterestRate.From(0.005732m);
                var yearlyContributionRate = YearlyContributionRate.From(0.008564m);

                var actual = principal.Calculate(
                    CalculationDate.From(Date),
                    period * termsPerYear,
                    termsPerYear,
                    yearlyInterestRate / termsPerYear,
                    yearlyContributionRate / termsPerYear);

                var expected = new[]
                {
                    new {TotalPayment = 107848m, Repayment = 73686m, ContributionAndInterest = 34162m, Loanleft = 3046314m},
                    new {TotalPayment = 138975m, Repayment = 95938m, ContributionAndInterest = 43036m, Loanleft = 2950376m},
                    new {TotalPayment = 138151m, Repayment = 96489m, ContributionAndInterest = 41662m, Loanleft = 2853887m},
                    new {TotalPayment = 137323m, Repayment = 97043m, ContributionAndInterest = 40280m, Loanleft = 2756844m},
                    new {TotalPayment = 136490m, Repayment = 97601m, ContributionAndInterest = 38889m, Loanleft = 2659243m},
                    new {TotalPayment = 135617m, Repayment = 98189m, ContributionAndInterest = 37428m, Loanleft = 2561054m},
                    new {TotalPayment = 134763m, Repayment = 98760m, ContributionAndInterest = 36003m, Loanleft = 2462294m},
                    new {TotalPayment = 133915m, Repayment = 99324m, ContributionAndInterest = 34592m, Loanleft = 2362970m},
                    new {TotalPayment = 133063m, Repayment = 99891m, ContributionAndInterest = 33172m, Loanleft = 2263079m},
                    new {TotalPayment = 132206m, Repayment = 100462m, ContributionAndInterest = 31744m, Loanleft = 2162617m},
                    new {TotalPayment = 131304m, Repayment = 101079m, ContributionAndInterest = 30225m, Loanleft = 2061538m},
                    new {TotalPayment = 130424m, Repayment = 101666m, ContributionAndInterest = 28757m, Loanleft = 1959872m},
                    new {TotalPayment = 129551m, Repayment = 102242m, ContributionAndInterest = 27309m, Loanleft = 1857630m},
                    new {TotalPayment = 128674m, Repayment = 102820m, ContributionAndInterest = 25853m, Loanleft = 1754810m},
                    new {TotalPayment = 127791m, Repayment = 103402m, ContributionAndInterest = 24389m, Loanleft = 1651407m},
                    new {TotalPayment = 126844m, Repayment = 104043m, ContributionAndInterest = 22801m, Loanleft = 1547364m},
                    new {TotalPayment = 125931m, Repayment = 104642m, ContributionAndInterest = 21289m, Loanleft = 1442722m},
                    new {TotalPayment = 125033m, Repayment = 105224m, ContributionAndInterest = 19809m, Loanleft = 1337498m},
                    new {TotalPayment = 124130m, Repayment = 105810m, ContributionAndInterest = 18321m, Loanleft = 1231688m},
                    new {TotalPayment = 123222m, Repayment = 106398m, ContributionAndInterest = 16824m, Loanleft = 1125290m},
                    new {TotalPayment = 122216m, Repayment = 107068m, ContributionAndInterest = 15148m, Loanleft = 1018222m},
                    new {TotalPayment = 121267m, Repayment = 107670m, ContributionAndInterest = 13597m, Loanleft = 910552m},
                    new {TotalPayment = 120343m, Repayment = 108246m, ContributionAndInterest = 12097m, Loanleft = 802306m},
                    new {TotalPayment = 119414m, Repayment = 108825m, ContributionAndInterest = 10588m, Loanleft = 693480m},
                    new {TotalPayment = 118480m, Repayment = 109408m, ContributionAndInterest = 9072m, Loanleft = 584073m},
                    new {TotalPayment = 117315m, Repayment = 110177m, ContributionAndInterest = 7138m, Loanleft = 473896m},
                    new {TotalPayment = 116294m, Repayment = 110728m, ContributionAndInterest = 5566m, Loanleft = 363168m},
                    new {TotalPayment = 115344m, Repayment = 111206m, ContributionAndInterest = 4138m, Loanleft = 251963m},
                    new {TotalPayment = 114390m, Repayment = 111685m, ContributionAndInterest = 2705m, Loanleft = 140227m},
                    new {TotalPayment = 113531m, Repayment = 112167m, ContributionAndInterest = 1364m, Loanleft = 28110m},
                    new {TotalPayment = 28347m, Repayment = 28116m, ContributionAndInterest = 230m, Loanleft = -6m},
                };

                var expectedRepayment = expected.Sum(plan => plan.Repayment) - 5m;
                var actualRepayment = actual.PlanByTerms.Sum(term => (decimal)term.Repayment);
                Assert.Equal(expectedRepayment, actualRepayment, 2);

                var expectedInterestAndContribution = expected.Sum(plan => plan.ContributionAndInterest) + 5590.81m;
                var actualInterestAndContribution = actual.PlanByTerms.Sum(term => (decimal)term.Interest + (decimal)term.Contribution);
                Assert.Equal(expectedInterestAndContribution, actualInterestAndContribution, 2);

                var expectedRepaymentPlan = expected.Select(plan => plan.Repayment);
                var actualRepaymentPlan = actual.PlanByYears.Select(plan => (decimal)plan.Repayment);
                CollectionAssert.Equal(expectedRepaymentPlan, actualRepaymentPlan, 2500m);

                var expectedInterestContributionPlan = expected.Select(plan => plan.ContributionAndInterest);
                var actualInterestContributionPlan = actual.PlanByYears.Select(plan => (decimal)plan.Interest + (decimal)plan.Contribution);
                CollectionAssert.Equal(expectedInterestContributionPlan, actualInterestContributionPlan, 700m);
            }
        }
    }
}