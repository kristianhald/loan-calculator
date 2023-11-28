using System;

namespace Koolawong.InterestCalculator.Model.Outside.Simple
{
    public class ExchangeRate
    {
        private readonly decimal _value;

        private ExchangeRate(decimal value)
        {
            if (value < 0m)
                throw new ArgumentOutOfRangeException(nameof(value), "Value must be zero or higher.");
            if (value > 1.75m)
                throw new ArgumentOutOfRangeException(nameof(value), "Value must be 175% or lower.");

            _value = value;
        }

        public static ExchangeRate From(decimal percentage)
        {
            return new ExchangeRate(percentage);
        }

        public static explicit operator decimal(ExchangeRate rate)
        {
            return rate._value;
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

        private bool Equals(ExchangeRate obj)
        {
            return
                obj != null &&
                _value == obj._value;
        }
    }
}
