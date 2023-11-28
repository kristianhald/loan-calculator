using System;
using Koolawong.InterestCalculator.Model.Inside.Simple;

namespace Koolawong.InterestCalculator.Model.Outside.Simple
{
    // https://en.wikipedia.org/wiki/Loan-to-value_ratio
    public class LoanToValue
    {
        private readonly decimal _value;

        private LoanToValue(decimal value)
        {
            if (value < 0m)
                throw new ArgumentOutOfRangeException(nameof(value), "Value must be zero or higher.");
            if (value > 1m)
                throw new ArgumentOutOfRangeException(nameof(value), "Value must be 100% or lower.");

            _value = value;
        }

        public static LoanToValue From(decimal value)
        {
            return new LoanToValue(value);
        }

        public static bool operator <(LoanToValue a, LoanToValue b)
        {
            return a._value < b._value;
        }

        public static bool operator <=(LoanToValue a, LoanToValue b)
        {
            return a._value <= b._value;
        }

        public static bool operator >(LoanToValue a, LoanToValue b)
        {
            return !(a <= b);
        }

        public static bool operator >=(LoanToValue a, LoanToValue b)
        {
            return !(a < b);
        }

        public static LoanToValue operator -(LoanToValue a, LoanToValue b)
        {
            return From(a._value - b._value);
        }

        public static ValueOfRate operator *(LoanToValue a, HouseValue b)
        {
            return ValueOfRate.From(a._value * (decimal)b);
        }

        public override bool Equals(object obj)
        {
            return Equals((dynamic)obj);
        }

        public override string ToString()
        {
            return $"{_value * 100m}%";
        }

        public override int GetHashCode()
        {
            return _value.GetHashCode();
        }

        private bool Equals(LoanToValue obj)
        {
            return
                obj != null &&
                _value == obj._value;
        }
    }
}