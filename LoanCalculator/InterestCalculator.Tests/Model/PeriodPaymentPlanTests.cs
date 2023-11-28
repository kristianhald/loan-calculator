using System;
using System.Collections.Generic;
using Koolawong.InterestCalculator.Model.Inside.Complex;
using Koolawong.InterestCalculator.Model.Inside.Simple;
using Koolawong.InterestCalculator.Model.Outside.Simple;
using Koolawong.InterestCalculator.Tests.Model.PeriodPaymentPlanData;
using Xunit;

namespace Koolawong.InterestCalculator.Tests.Model
{
    public class PeriodPaymentPlanTests
    {
        public static IEnumerable<object[]> PrincipalToPaymentPlan => new[]
        {
            PeriodPaymentPlanDataReader.ReadData(PeriodPaymentPlanDataFiles.OneYearOneTermPerYear),
            PeriodPaymentPlanDataReader.ReadData(PeriodPaymentPlanDataFiles.OneYearTwoTermsPerYear),
            PeriodPaymentPlanDataReader.ReadData(PeriodPaymentPlanDataFiles.OneYearThreeTermsPerYear),
            PeriodPaymentPlanDataReader.ReadData(PeriodPaymentPlanDataFiles.OneYearFourTermsPerYear),
            PeriodPaymentPlanDataReader.ReadData(PeriodPaymentPlanDataFiles.OneYearSixTermsPerYear),
            PeriodPaymentPlanDataReader.ReadData(PeriodPaymentPlanDataFiles.OneYearTwelveTermsPerYear),
            PeriodPaymentPlanDataReader.ReadData(PeriodPaymentPlanDataFiles.TwoYearsFourTermsPerYear),
            PeriodPaymentPlanDataReader.ReadData(PeriodPaymentPlanDataFiles.ThreeYearsFourTermsPerYear),
            PeriodPaymentPlanDataReader.ReadData(PeriodPaymentPlanDataFiles.FourYearsFourTermsPerYear),
            PeriodPaymentPlanDataReader.ReadData(PeriodPaymentPlanDataFiles.FiveYearsFourTermsPerYear),
            PeriodPaymentPlanDataReader.ReadData(PeriodPaymentPlanDataFiles.TenYearsFourTermsPerYear),
            PeriodPaymentPlanDataReader.ReadData(PeriodPaymentPlanDataFiles.FifteenYearsFourTermsPerYear),
            PeriodPaymentPlanDataReader.ReadData(PeriodPaymentPlanDataFiles.TwentyYearsFourTermsPerYear),
            PeriodPaymentPlanDataReader.ReadData(PeriodPaymentPlanDataFiles.ThirtyYearsFourTermsPerYear),
        };

        [Theory]
        [MemberData("PrincipalToPaymentPlan")]
        public void GivenProvidedData_ThenPaymentPlanMatchesExpected(
            Principal principal,
            Terms terms,
            TermsPerYear termsPerYear,
            TermInterestRate interestRate,
            TermContributionRate contributionRate,
            PeriodPaymentPlan expected)
        {
            var calculationDate = CalculationDate.From(new DateTime(2016, 1, 1));

            var periodPaymentPlan = principal.Calculate(
                calculationDate,
                terms,
                termsPerYear,
                interestRate,
                contributionRate);

            Assert.Equal(expected, periodPaymentPlan);
        }

        [Fact]
        public void GivenOneTermInOneYearProvidedData_ThenPaymentPlanIsAggregatedToYearly()
        {
            var paymentPlan = PeriodPaymentPlan.From(
                new[]
                {
                    TermPaymentPlan.From(Term.From(1), Repayment.From(1000m), Interest.From(100m), Contribution.From(200m), PaymentLeft.From(0m))
                },
                TermsPerYear.From(1),
                CalculationDate.From(DateTime.Today));

            var expected = new[] { YearlyPaymentPlan.From(Year.From(1), Repayment.From(1000m), Interest.From(100m), Contribution.From(200m), PaymentLeft.From(0m)) };

            var actual = paymentPlan.PlanByYears;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GivenTwoTermsInOneYearProvidedData_ThenPaymentPlanIsAggregatedToYearly()
        {
            var paymentPlan = PeriodPaymentPlan.From(
                new[]
                {
                    TermPaymentPlan.From(Term.From(1), Repayment.From(1000m), Interest.From(100m), Contribution.From(200m), PaymentLeft.From(3000m)),
                    TermPaymentPlan.From(Term.From(2), Repayment.From(1000m), Interest.From(100m), Contribution.From(200m), PaymentLeft.From(0m)),
                },
                TermsPerYear.From(2),
                CalculationDate.From(new DateTime(2016, 1, 1)));

            var expected = new[] { YearlyPaymentPlan.From(Year.From(1), Repayment.From(2000m), Interest.From(200m), Contribution.From(400m), PaymentLeft.From(0m)) };

            var actual = paymentPlan.PlanByYears;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GivenSingleTermInTwoYears_ThenPaymentPlanIsAggregatedToYearly()
        {
            var paymentPlan = PeriodPaymentPlan.From(
                new[]
                {
                    TermPaymentPlan.From(Term.From(1), Repayment.From(1000m), Interest.From(100m), Contribution.From(200m), PaymentLeft.From(3000m)),
                    TermPaymentPlan.From(Term.From(2), Repayment.From(1000m), Interest.From(100m), Contribution.From(200m), PaymentLeft.From(0m)),
                },
                TermsPerYear.From(1),
                CalculationDate.From(DateTime.Today));

            var expected = new[]
            {
                YearlyPaymentPlan.From(Year.From(1), Repayment.From(1000m), Interest.From(100m), Contribution.From(200m), PaymentLeft.From(3000m)),
                YearlyPaymentPlan.From(Year.From(2), Repayment.From(1000m), Interest.From(100m), Contribution.From(200m), PaymentLeft.From(0m))
            };

            var actual = paymentPlan.PlanByYears;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GivenTwoTermsInTwoYears_ThenPaymentPlanIsAggregatedToYearly()
        {
            var paymentPlan = PeriodPaymentPlan.From(
                new[]
                {
                    TermPaymentPlan.From(Term.From(1), Repayment.From(1000m), Interest.From(100m), Contribution.From(200m), PaymentLeft.From(1234567m)),
                    TermPaymentPlan.From(Term.From(2), Repayment.From(1000m), Interest.From(100m), Contribution.From(200m), PaymentLeft.From(3000m)),
                    TermPaymentPlan.From(Term.From(3), Repayment.From(2000m), Interest.From(200m), Contribution.From(400m), PaymentLeft.From(1234567m)),
                    TermPaymentPlan.From(Term.From(4), Repayment.From(2000m), Interest.From(200m), Contribution.From(400m), PaymentLeft.From(0m)),
                },
                TermsPerYear.From(2),
                CalculationDate.From(new DateTime(2016, 1, 1)));

            var expected = new[]
            {
                YearlyPaymentPlan.From(Year.From(1), Repayment.From(2000m), Interest.From(200m), Contribution.From(400m), PaymentLeft.From(3000m)),
                YearlyPaymentPlan.From(Year.From(2), Repayment.From(4000m), Interest.From(400m), Contribution.From(800m), PaymentLeft.From(0m))
            };

            var actual = paymentPlan.PlanByYears;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GivenTwoTermsInTwoYears_WhenCalculationDateStartsInSecondTerm_ThenPaymentPlanIsAggregatedToYearly()
        {
            var paymentPlan = PeriodPaymentPlan.From(
                new[]
                {
                    TermPaymentPlan.From(Term.From(1), Repayment.From(1000m), Interest.From(100m), Contribution.From(200m), PaymentLeft.From(5000m)),
                    TermPaymentPlan.From(Term.From(2), Repayment.From(1000m), Interest.From(100m), Contribution.From(200m), PaymentLeft.From(1234567m)),
                    TermPaymentPlan.From(Term.From(3), Repayment.From(2000m), Interest.From(200m), Contribution.From(400m), PaymentLeft.From(3000m)),
                    TermPaymentPlan.From(Term.From(4), Repayment.From(2000m), Interest.From(200m), Contribution.From(400m), PaymentLeft.From(0m)),
                },
                TermsPerYear.From(2),
                CalculationDate.From(new DateTime(2016, 7, 1)));

            var expected = new[]
            {
                YearlyPaymentPlan.From(Year.From(1), Repayment.From(1000m), Interest.From(100m), Contribution.From(200m), PaymentLeft.From(5000m)),
                YearlyPaymentPlan.From(Year.From(2), Repayment.From(3000m), Interest.From(300m), Contribution.From(600m), PaymentLeft.From(3000m)),
                YearlyPaymentPlan.From(Year.From(3), Repayment.From(2000m), Interest.From(200m), Contribution.From(400m), PaymentLeft.From(0m))
            };

            var actual = paymentPlan.PlanByYears;

            Assert.Equal(expected, actual);
        }
    }
}
