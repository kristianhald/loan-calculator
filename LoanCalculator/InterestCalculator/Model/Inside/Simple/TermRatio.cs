using System;

namespace Koolawong.InterestCalculator.Model.Inside.Simple
{
    public class TermRatio
    {
        private readonly decimal _value;

        private TermRatio(decimal value)
        {
            if (value < 0m)
                throw new ArgumentOutOfRangeException(nameof(value), "Value must be zero or higher.");
            if (value > 1m)
                throw new ArgumentOutOfRangeException(nameof(value), "Value must be 100% or lower.");

            _value = value;
        }

        public static TermRatio From(decimal value)
        {
            return new TermRatio(value);
        }

        public static explicit operator decimal(TermRatio ratio)
        {
            return ratio._value;
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

        private bool Equals(TermRatio obj)
        {
            return
                obj != null &&
                _value == obj._value;
        }
    }
}
