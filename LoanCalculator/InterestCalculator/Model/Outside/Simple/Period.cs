using System;
using Koolawong.InterestCalculator.Model.Inside.Simple;

namespace Koolawong.InterestCalculator.Model.Outside.Simple
{
    public class Period
    {
        private readonly int _value;

        private Period(int value)
        {
            if (value <= 0)
                throw new ArgumentOutOfRangeException("value", "Value must be 1 or higher.");
            if (value > 30)
                throw new ArgumentOutOfRangeException("value", "Value must be 30 or lower.");

            _value = value;
        }

        public static Period From(int value)
        {
            return new Period(value);
        }

        public static explicit operator int(Period termsPerYear)
        {
            return termsPerYear._value;
        }

        public static Terms operator *(Period period, TermsPerYear termsPerYear)
        {
            return Terms.From((int)period * (int)termsPerYear);
        }

        public override bool Equals(object obj)
        {
            return Equals((dynamic)obj);
        }

        private bool Equals(Period obj)
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
            return $"{_value.ToString()} years";
        }
    }
}
