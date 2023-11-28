using System;

namespace Koolawong.InterestCalculator.Model.Inside.Simple
{
    public class TermInterestRate
    {
        private readonly decimal _value;

        private TermInterestRate(decimal value)
        {
            if (value < -10m)
                throw new ArgumentOutOfRangeException(nameof(value), "Value must be -1000% or higher.");
            if (value > 10m)
                throw new ArgumentOutOfRangeException(nameof(value), "Value must be 1000% or lower.");

            _value = value;
        }

        public static TermInterestRate HundredPercent => From(1);

        public static TermInterestRate From(decimal value)
        {
            return new TermInterestRate(value);
        }

        public static explicit operator decimal(TermInterestRate rate)
        {
            return rate._value;
        }

        public static TermInterestRate operator +(TermInterestRate a, TermInterestRate b)
        {
            return From(a._value + b._value);
        }

        public static TermInterestRate operator -(TermInterestRate a, TermInterestRate b)
        {
            return From(a._value - b._value);
        }

        public static Ratio operator /(TermInterestRate a, TermInterestRate b)
        {
            return Ratio.From(a._value / b._value);
        }

        public static TermInterestRate operator ^(TermInterestRate rate, Terms terms)
        {
            return From((decimal)Math.Pow((double)rate._value, (double)(decimal)terms));
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

        private bool Equals(TermInterestRate obj)
        {
            return
                obj != null &&
                _value == obj._value;
        }
    }
}
