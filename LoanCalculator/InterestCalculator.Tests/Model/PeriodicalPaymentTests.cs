using System.Collections.Generic;
using Koolawong.InterestCalculator.Model.Inside.Simple;
using Xunit;

namespace Koolawong.InterestCalculator.Tests.Model
{
    public class PeriodicalPaymentTests
    {
        public static IEnumerable<object[]> CalculatePeriodicalPaymentData => new[]
        {
            new object[] { Principal.From(100000m), TermInterestRate.From(0.01500m), Terms.From(1), PeriodicalPayment.From(101500m)},
            new object[] { Principal.From(100000m), TermInterestRate.From(0.00750m), Terms.From(2), PeriodicalPayment.From(50563.20m)},
            new object[] { Principal.From(100000m), TermInterestRate.From(0.00500m), Terms.From(3), PeriodicalPayment.From(33667.22m)},
            new object[] { Principal.From(100000m), TermInterestRate.From(0.00375m), Terms.From(4), PeriodicalPayment.From(25234.81m)},
            new object[] { Principal.From(100000m), TermInterestRate.From(0.00250m), Terms.From(6), PeriodicalPayment.From(16812.80m)},
            new object[] { Principal.From(100000m), TermInterestRate.From(0.00125m), Terms.From(12), PeriodicalPayment.From(8401.20m)},
            new object[] { Principal.From(100000m), TermInterestRate.From(0.00375m), Terms.From(8), PeriodicalPayment.From(12711.86m)},
            new object[] { Principal.From(100000m), TermInterestRate.From(0.00375m), Terms.From(12), PeriodicalPayment.From(8537.85m)},
            new object[] { Principal.From(100000m), TermInterestRate.From(0.00375m), Terms.From(16), PeriodicalPayment.From(6451.08m)},
            new object[] { Principal.From(100000m), TermInterestRate.From(0.00375m), Terms.From(20), PeriodicalPayment.From(5199.21m)},
            new object[] { Principal.From(100000m), TermInterestRate.From(0.00375m), Terms.From(40), PeriodicalPayment.From(2696.86m)},
            new object[] { Principal.From(100000m), TermInterestRate.From(0.00375m), Terms.From(60), PeriodicalPayment.From(1864.30m)},
            new object[] { Principal.From(100000m), TermInterestRate.From(0.00375m), Terms.From(80), PeriodicalPayment.From(1449.19m)},
            new object[] { Principal.From(100000m), TermInterestRate.From(0.00375m), Terms.From(100), PeriodicalPayment.From(1201.04m)},
            new object[] { Principal.From(100000m), TermInterestRate.From(0.00375m), Terms.From(120), PeriodicalPayment.From(1036.38m)},
        };

        [Theory]
        [MemberData("CalculatePeriodicalPaymentData")]
        public void GivenProvidedData_ThenPeriodicalPaymentIsCalculated(
            Principal principal,
            TermInterestRate termInterestRate,
            Terms terms,
            PeriodicalPayment expected)
        {
            var actual = principal.Calculate(termInterestRate, terms);

            Assert.Equal(expected, actual);
        }
    }
}
