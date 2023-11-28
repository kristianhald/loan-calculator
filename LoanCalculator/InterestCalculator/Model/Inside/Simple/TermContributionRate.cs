using System;

namespace Koolawong.InterestCalculator.Model.Inside.Simple
{
    public class TermContributionRate
    {
        private readonly decimal _value;

        private TermContributionRate(decimal value)
        {
            if (value < 0m)
                throw new ArgumentOutOfRangeException(nameof(value), "Value must be zero or higher.");
            if (value > 1m)
                throw new ArgumentOutOfRangeException(nameof(value), "Value must be 100% or lower.");

            _value = value;
        }

        public static TermContributionRate From(decimal value)
        {
            return new TermContributionRate(value);
        }

        public static explicit operator decimal(TermContributionRate rate)
        {
            return rate._value;
        }

        public override bool Equals(object obj)
        {
            return Equals((dynamic)obj);
        }

        public override string ToString()
        {
            return $"{_value * 100m}% per term";
        }

        public override int GetHashCode()
        {
            return _value.GetHashCode();
        }

        private bool Equals(TermContributionRate obj)
        {
            return
                obj != null &&
                _value == obj._value;
        }
    }
}
