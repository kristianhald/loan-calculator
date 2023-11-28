using System;
using Koolawong.InterestCalculator.Model.Inside.Simple;

namespace Koolawong.InterestCalculator.Model.Outside.Simple
{
    public class TermsPerYear
    {
        private readonly int _value;

        private TermsPerYear(int value)
        {
            if (value <= 0)
                throw new ArgumentOutOfRangeException("value", "Value must be 1 or higher.");
            if (value > 12)
                throw new ArgumentOutOfRangeException("value", "Value must be 12 or lower. It does not support less than monthly terms");

            _value = value;
        }

        public static TermsPerYear From(int value)
        {
            return new TermsPerYear(value);
        }

        public static explicit operator int(TermsPerYear termsPerYear)
        {
            return termsPerYear._value;
        }

        // Not too happy about this one. Shouldn't need to have this as a cast, but should more be an operation
        // Might be needing MonthsPerYear definition
        public static explicit operator MonthsPerTerm(TermsPerYear termsPerYear)
        {
            return MonthsPerTerm.From(12 / termsPerYear._value);
        }

        public override bool Equals(object obj)
        {
            return Equals((dynamic)obj);
        }

        private bool Equals(TermsPerYear obj)
        {
            return
                obj != null &&
                _value == obj._value;
        }

        public override int GetHashCode()
        {
            return _value.GetHashCode();
        }

        public override string ToString()
        {
            return $"{_value.ToString()} terms per year";
        }
    }
}
