using System;
using System.Collections.Generic;
using Koolawong.InterestCalculator.Model.Inside.Complex;
using Koolawong.InterestCalculator.Model.Outside.Simple;
using Xunit;

namespace Koolawong.InterestCalculator.Tests.Model
{
    public class CalculationDateTests
    {
        public static IEnumerable<object[]> CalculationDateToInTerm => new[]
        {
            new object[] { CalculationDate.From(DateTime.Today), TermsPerYear.From(1), Term.From(1)},
            new object[] { CalculationDate.From(new DateTime(2016, 1, 1)), TermsPerYear.From(2), Term.From(1)},
            new object[] { CalculationDate.From(new DateTime(2016, 6, 30)), TermsPerYear.From(2), Term.From(1)},
            new object[] { CalculationDate.From(new DateTime(2016, 7, 1)), TermsPerYear.From(2), Term.From(2)},
            new object[] { CalculationDate.From(new DateTime(2016, 12, 31)), TermsPerYear.From(2), Term.From(2)},
            new object[] { CalculationDate.From(new DateTime(2016, 1, 1)), TermsPerYear.From(4), Term.From(1)},
            new object[] { CalculationDate.From(new DateTime(2016, 3, 31)), TermsPerYear.From(4), Term.From(1)},
            new object[] { CalculationDate.From(new DateTime(2016, 4, 1)), TermsPerYear.From(4), Term.From(2)},
            new object[] { CalculationDate.From(new DateTime(2016, 6, 30)), TermsPerYear.From(4), Term.From(2)},
            new object[] { CalculationDate.From(new DateTime(2016, 7, 1)), TermsPerYear.From(4), Term.From(3)},
            new object[] { CalculationDate.From(new DateTime(2016, 9, 30)), TermsPerYear.From(4), Term.From(3)},
            new object[] { CalculationDate.From(new DateTime(2016, 10, 1)), TermsPerYear.From(4), Term.From(4)},
            new object[] { CalculationDate.From(new DateTime(2016, 12, 31)), TermsPerYear.From(4), Term.From(4)},
            new object[] { CalculationDate.From(new DateTime(2016, 1, 1)), TermsPerYear.From(12), Term.From(1)},
            new object[] { CalculationDate.From(new DateTime(2016, 1, 31)), TermsPerYear.From(12), Term.From(1)},
            new object[] { CalculationDate.From(new DateTime(2016, 2, 1)), TermsPerYear.From(12), Term.From(2)},
            new object[] { CalculationDate.From(new DateTime(2016, 2, 29)), TermsPerYear.From(12), Term.From(2)},
            new object[] { CalculationDate.From(new DateTime(2016, 3, 1)), TermsPerYear.From(12), Term.From(3)},
            new object[] { CalculationDate.From(new DateTime(2016, 3, 31)), TermsPerYear.From(12), Term.From(3)},
            new object[] { CalculationDate.From(new DateTime(2016, 4, 1)), TermsPerYear.From(12), Term.From(4)},
            new object[] { CalculationDate.From(new DateTime(2016, 4, 30)), TermsPerYear.From(12), Term.From(4)},
            new object[] { CalculationDate.From(new DateTime(2016, 5, 1)), TermsPerYear.From(12), Term.From(5)},
            new object[] { CalculationDate.From(new DateTime(2016, 5, 31)), TermsPerYear.From(12), Term.From(5)},
            new object[] { CalculationDate.From(new DateTime(2016, 6, 1)), TermsPerYear.From(12), Term.From(6)},
            new object[] { CalculationDate.From(new DateTime(2016, 6, 30)), TermsPerYear.From(12), Term.From(6)},
            new object[] { CalculationDate.From(new DateTime(2016, 7, 1)), TermsPerYear.From(12), Term.From(7)},
            new object[] { CalculationDate.From(new DateTime(2016, 7, 31)), TermsPerYear.From(12), Term.From(7)},
            new object[] { CalculationDate.From(new DateTime(2016, 8, 1)), TermsPerYear.From(12), Term.From(8)},
            new object[] { CalculationDate.From(new DateTime(2016, 8, 31)), TermsPerYear.From(12), Term.From(8)},
            new object[] { CalculationDate.From(new DateTime(2016, 9, 1)), TermsPerYear.From(12), Term.From(9)},
            new object[] { CalculationDate.From(new DateTime(2016, 9, 30)), TermsPerYear.From(12), Term.From(9)},
            new object[] { CalculationDate.From(new DateTime(2016, 10, 1)), TermsPerYear.From(12), Term.From(10)},
            new object[] { CalculationDate.From(new DateTime(2016, 10, 31)), TermsPerYear.From(12), Term.From(10)},
            new object[] { CalculationDate.From(new DateTime(2016, 11, 1)), TermsPerYear.From(12), Term.From(11)},
            new object[] { CalculationDate.From(new DateTime(2016, 11, 30)), TermsPerYear.From(12), Term.From(11)},
            new object[] { CalculationDate.From(new DateTime(2016, 12, 1)), TermsPerYear.From(12), Term.From(12)},
            new object[] { CalculationDate.From(new DateTime(2016, 12, 31)), TermsPerYear.From(12), Term.From(12)},
        };

        [Theory]
        [MemberData("CalculationDateToInTerm")]
        public void ProvidedWithTheData_ThenTheExpectedTermIsFound(
            CalculationDate calculationDate,
            TermsPerYear termsPerYear,
            Term expected)
        {
            var actual = calculationDate.InTerm(termsPerYear);

            Assert.Equal(expected, actual);
        }
    }
}