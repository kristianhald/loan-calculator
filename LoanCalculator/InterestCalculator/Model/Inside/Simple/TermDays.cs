using System;

namespace Koolawong.InterestCalculator.Model.Inside.Simple
{
    public class TermDays
    {
        private readonly int _value;

        private TermDays(int value)
        {
            if (value <= 0)
                throw new ArgumentOutOfRangeException("value", "Value must be 1 or higher.");
            if (value > 366)
                throw new ArgumentOutOfRangeException(nameof(value), "Value must be 366 or lower as there cannot be more than the number of days in a single year");

            _value = value;
        }

        public static TermDays From(int value)
        {
            return new TermDays(value);
        }

        public static TermRatio operator /(TermDays a, TermDays b)
        {
            return TermRatio.From((decimal)a._value / (decimal)b._value);
        }

        public override bool Equals(object obj)
        {
            return Equals((dynamic)obj);
        }

        private bool Equals(TermDays obj)
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
            return $"{_value.ToString()} days";
        }
    }
}
