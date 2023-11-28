using System;
using System.Collections.Generic;
using Koolawong.InterestCalculator.Model.Inside.Simple;
using Koolawong.InterestCalculator.Model.Outside.Simple;
using Xunit;

namespace Koolawong.InterestCalculator.Tests.Model
{
    public class CalculationTermTests
    {
        public static IEnumerable<object[]> CalculationTermsToDaysInTerm => new[]
        {
            new object[] { CalculationDate.From(new DateTime(2016, 5, 23)), TermsPerYear.From(1), TermDays.From(366)},
            new object[] { CalculationDate.From(new DateTime(2016, 1, 1)), TermsPerYear.From(2), TermDays.From(182)},
            new object[] { CalculationDate.From(new DateTime(2016, 6, 30)), TermsPerYear.From(2), TermDays.From(182)},
            new object[] { CalculationDate.From(new DateTime(2016, 7, 1)), TermsPerYear.From(2), TermDays.From(184)},
            new object[] { CalculationDate.From(new DateTime(2016, 12, 31)), TermsPerYear.From(2), TermDays.From(184)},
            new object[] { CalculationDate.From(new DateTime(2016, 1, 1)), TermsPerYear.From(4), TermDays.From(91)},
            new object[] { CalculationDate.From(new DateTime(2016, 3, 31)), TermsPerYear.From(4), TermDays.From(91)},
            new object[] { CalculationDate.From(new DateTime(2016, 4, 1)), TermsPerYear.From(4), TermDays.From(91) },
            new object[] { CalculationDate.From(new DateTime(2016, 6, 30)), TermsPerYear.From(4), TermDays.From(91) },
            new object[] { CalculationDate.From(new DateTime(2016, 7, 1)), TermsPerYear.From(4), TermDays.From(92)},
            new object[] { CalculationDate.From(new DateTime(2016, 9, 30)), TermsPerYear.From(4), TermDays.From(92) },
            new object[] { CalculationDate.From(new DateTime(2016, 10, 1)), TermsPerYear.From(4), TermDays.From(92) },
            new object[] { CalculationDate.From(new DateTime(2016, 12, 31)), TermsPerYear.From(4), TermDays.From(92) },
            new object[] { CalculationDate.From(new DateTime(2016, 1, 1)), TermsPerYear.From(12), TermDays.From(31)},
            new object[] { CalculationDate.From(new DateTime(2016, 1, 31)), TermsPerYear.From(12), TermDays.From(31)},
            new object[] { CalculationDate.From(new DateTime(2016, 2, 1)), TermsPerYear.From(12), TermDays.From(29)},
            new object[] { CalculationDate.From(new DateTime(2016, 2, 29)), TermsPerYear.From(12), TermDays.From(29) },
            new object[] { CalculationDate.From(new DateTime(2016, 3, 1)), TermsPerYear.From(12), TermDays.From(31)},
            new object[] { CalculationDate.From(new DateTime(2016, 3, 31)), TermsPerYear.From(12), TermDays.From(31)},
            new object[] { CalculationDate.From(new DateTime(2016, 4, 1)), TermsPerYear.From(12), TermDays.From(30)},
            new object[] { CalculationDate.From(new DateTime(2016, 4, 30)), TermsPerYear.From(12), TermDays.From(30) },
            new object[] { CalculationDate.From(new DateTime(2016, 5, 1)), TermsPerYear.From(12), TermDays.From(31)},
            new object[] { CalculationDate.From(new DateTime(2016, 5, 31)), TermsPerYear.From(12), TermDays.From(31)},
            new object[] { CalculationDate.From(new DateTime(2016, 6, 1)), TermsPerYear.From(12), TermDays.From(30) },
            new object[] { CalculationDate.From(new DateTime(2016, 6, 30)), TermsPerYear.From(12), TermDays.From(30) },
            new object[] { CalculationDate.From(new DateTime(2016, 7, 1)), TermsPerYear.From(12), TermDays.From(31)},
            new object[] { CalculationDate.From(new DateTime(2016, 7, 31)), TermsPerYear.From(12), TermDays.From(31)},
            new object[] { CalculationDate.From(new DateTime(2016, 8, 1)), TermsPerYear.From(12), TermDays.From(31)},
            new object[] { CalculationDate.From(new DateTime(2016, 8, 31)), TermsPerYear.From(12), TermDays.From(31)},
            new object[] { CalculationDate.From(new DateTime(2016, 9, 1)), TermsPerYear.From(12), TermDays.From(30) },
            new object[] { CalculationDate.From(new DateTime(2016, 9, 30)), TermsPerYear.From(12), TermDays.From(30) },
            new object[] { CalculationDate.From(new DateTime(2016, 10, 1)), TermsPerYear.From(12), TermDays.From(31)},
            new object[] { CalculationDate.From(new DateTime(2016, 10, 31)), TermsPerYear.From(12), TermDays.From(31)},
            new object[] { CalculationDate.From(new DateTime(2016, 11, 1)), TermsPerYear.From(12), TermDays.From(30) },
            new object[] { CalculationDate.From(new DateTime(2016, 11, 30)), TermsPerYear.From(12), TermDays.From(30) },
            new object[] { CalculationDate.From(new DateTime(2016, 12, 1)), TermsPerYear.From(12), TermDays.From(31)},
            new object[] { CalculationDate.From(new DateTime(2016, 12, 31)), TermsPerYear.From(12), TermDays.From(31)},
        };

        [Theory]
        [MemberData("CalculationTermsToDaysInTerm")]
        public void ProvidedWithTheData_ThenTheExpectedTermDaysIsFound(
            CalculationDate calculationDate,
            TermsPerYear termsPerYear,
            TermDays expected)
        {
            var calculationTerm = calculationDate.InTerm(termsPerYear);
            var actual = calculationTerm.DaysInTerm;

            Assert.Equal(expected, actual);
        }

        public static IEnumerable<object[]> CalculationTermsToDayLeftsInTerm => new[]
        {
            new object[] { CalculationDate.From(new DateTime(2016, 1, 1)), TermsPerYear.From(1), TermDays.From(366)},
            new object[] { CalculationDate.From(new DateTime(2016, 12, 31)), TermsPerYear.From(1), TermDays.From(1)},
            new object[] { CalculationDate.From(new DateTime(2016, 1, 1)), TermsPerYear.From(2), TermDays.From(182)},
            new object[] { CalculationDate.From(new DateTime(2016, 6, 30)), TermsPerYear.From(2), TermDays.From(1)},
            new object[] { CalculationDate.From(new DateTime(2016, 7, 1)), TermsPerYear.From(2), TermDays.From(184)},
            new object[] { CalculationDate.From(new DateTime(2016, 12, 31)), TermsPerYear.From(2), TermDays.From(1)},
            new object[] { CalculationDate.From(new DateTime(2016, 1, 1)), TermsPerYear.From(4), TermDays.From(91)},
            new object[] { CalculationDate.From(new DateTime(2016, 3, 31)), TermsPerYear.From(4), TermDays.From(1)},
            new object[] { CalculationDate.From(new DateTime(2016, 4, 1)), TermsPerYear.From(4), TermDays.From(91) },
            new object[] { CalculationDate.From(new DateTime(2016, 6, 30)), TermsPerYear.From(4), TermDays.From(1) },
            new object[] { CalculationDate.From(new DateTime(2016, 7, 1)), TermsPerYear.From(4), TermDays.From(92)},
            new object[] { CalculationDate.From(new DateTime(2016, 9, 30)), TermsPerYear.From(4), TermDays.From(1) },
            new object[] { CalculationDate.From(new DateTime(2016, 10, 1)), TermsPerYear.From(4), TermDays.From(92) },
            new object[] { CalculationDate.From(new DateTime(2016, 12, 31)), TermsPerYear.From(4), TermDays.From(1) },
            new object[] { CalculationDate.From(new DateTime(2016, 1, 1)), TermsPerYear.From(12), TermDays.From(31)},
            new object[] { CalculationDate.From(new DateTime(2016, 1, 31)), TermsPerYear.From(12), TermDays.From(1)},
            new object[] { CalculationDate.From(new DateTime(2016, 2, 1)), TermsPerYear.From(12), TermDays.From(29)},
            new object[] { CalculationDate.From(new DateTime(2016, 2, 29)), TermsPerYear.From(12), TermDays.From(1) },
            new object[] { CalculationDate.From(new DateTime(2016, 3, 1)), TermsPerYear.From(12), TermDays.From(31)},
            new object[] { CalculationDate.From(new DateTime(2016, 3, 31)), TermsPerYear.From(12), TermDays.From(1)},
            new object[] { CalculationDate.From(new DateTime(2016, 4, 1)), TermsPerYear.From(12), TermDays.From(30)},
            new object[] { CalculationDate.From(new DateTime(2016, 4, 30)), TermsPerYear.From(12), TermDays.From(1) },
            new object[] { CalculationDate.From(new DateTime(2016, 5, 1)), TermsPerYear.From(12), TermDays.From(31)},
            new object[] { CalculationDate.From(new DateTime(2016, 5, 31)), TermsPerYear.From(12), TermDays.From(1)},
            new object[] { CalculationDate.From(new DateTime(2016, 6, 1)), TermsPerYear.From(12), TermDays.From(30) },
            new object[] { CalculationDate.From(new DateTime(2016, 6, 30)), TermsPerYear.From(12), TermDays.From(1) },
            new object[] { CalculationDate.From(new DateTime(2016, 7, 1)), TermsPerYear.From(12), TermDays.From(31)},
            new object[] { CalculationDate.From(new DateTime(2016, 7, 31)), TermsPerYear.From(12), TermDays.From(1)},
            new object[] { CalculationDate.From(new DateTime(2016, 8, 1)), TermsPerYear.From(12), TermDays.From(31)},
            new object[] { CalculationDate.From(new DateTime(2016, 8, 31)), TermsPerYear.From(12), TermDays.From(1)},
            new object[] { CalculationDate.From(new DateTime(2016, 9, 1)), TermsPerYear.From(12), TermDays.From(30) },
            new object[] { CalculationDate.From(new DateTime(2016, 9, 30)), TermsPerYear.From(12), TermDays.From(1) },
            new object[] { CalculationDate.From(new DateTime(2016, 10, 1)), TermsPerYear.From(12), TermDays.From(31)},
            new object[] { CalculationDate.From(new DateTime(2016, 10, 31)), TermsPerYear.From(12), TermDays.From(1)},
            new object[] { CalculationDate.From(new DateTime(2016, 11, 1)), TermsPerYear.From(12), TermDays.From(30) },
            new object[] { CalculationDate.From(new DateTime(2016, 11, 30)), TermsPerYear.From(12), TermDays.From(1) },
            new object[] { CalculationDate.From(new DateTime(2016, 12, 1)), TermsPerYear.From(12), TermDays.From(31)},
            new object[] { CalculationDate.From(new DateTime(2016, 12, 31)), TermsPerYear.From(12), TermDays.From(1)},

            new object[] { CalculationDate.From(new DateTime(2016, 7, 23)), TermsPerYear.From(1), TermDays.From(162)},
            new object[] { CalculationDate.From(new DateTime(2016, 7, 23)), TermsPerYear.From(2), TermDays.From(162)},

            new object[] { CalculationDate.From(new DateTime(2016, 2, 11)), TermsPerYear.From(4), TermDays.From(50)},
            new object[] { CalculationDate.From(new DateTime(2016, 3, 16)), TermsPerYear.From(4), TermDays.From(16)},
            new object[] { CalculationDate.From(new DateTime(2016, 5, 11)), TermsPerYear.From(4), TermDays.From(51) },
            new object[] { CalculationDate.From(new DateTime(2016, 6, 16)), TermsPerYear.From(4), TermDays.From(15) },
            new object[] { CalculationDate.From(new DateTime(2016, 7, 11)), TermsPerYear.From(4), TermDays.From(82)},
            new object[] { CalculationDate.From(new DateTime(2016, 9, 16)), TermsPerYear.From(4), TermDays.From(15) },
            new object[] { CalculationDate.From(new DateTime(2016, 10, 11)), TermsPerYear.From(4), TermDays.From(82) },
            new object[] { CalculationDate.From(new DateTime(2016, 12, 16)), TermsPerYear.From(4), TermDays.From(16) },

            new object[] { CalculationDate.From(new DateTime(2016, 1, 11)), TermsPerYear.From(12), TermDays.From(21)},
            new object[] { CalculationDate.From(new DateTime(2016, 2, 11)), TermsPerYear.From(12), TermDays.From(19)},
            new object[] { CalculationDate.From(new DateTime(2016, 3, 11)), TermsPerYear.From(12), TermDays.From(21)},
            new object[] { CalculationDate.From(new DateTime(2016, 4, 11)), TermsPerYear.From(12), TermDays.From(20)},
            new object[] { CalculationDate.From(new DateTime(2016, 5, 11)), TermsPerYear.From(12), TermDays.From(21)},
            new object[] { CalculationDate.From(new DateTime(2016, 6, 11)), TermsPerYear.From(12), TermDays.From(20) },
            new object[] { CalculationDate.From(new DateTime(2016, 7, 11)), TermsPerYear.From(12), TermDays.From(21)},
            new object[] { CalculationDate.From(new DateTime(2016, 8, 11)), TermsPerYear.From(12), TermDays.From(21)},
            new object[] { CalculationDate.From(new DateTime(2016, 9, 11)), TermsPerYear.From(12), TermDays.From(20) },
            new object[] { CalculationDate.From(new DateTime(2016, 10, 11)), TermsPerYear.From(12), TermDays.From(21)},
            new object[] { CalculationDate.From(new DateTime(2016, 11, 11)), TermsPerYear.From(12), TermDays.From(20) },
            new object[] { CalculationDate.From(new DateTime(2016, 12, 11)), TermsPerYear.From(12), TermDays.From(21)},
        };

        [Theory]
        [MemberData("CalculationTermsToDayLeftsInTerm")]
        public void ProvidedWithTheData_ThenTheExpectedTermDayLeftsIsFound(
            CalculationDate calculationDate,
            TermsPerYear termsPerYear,
            TermDays expected)
        {
            var calculationTerm = calculationDate.InTerm(termsPerYear);
            var actual = calculationTerm.DaysLeftInTerm;

            Assert.Equal(expected, actual);
        }
    }
}
