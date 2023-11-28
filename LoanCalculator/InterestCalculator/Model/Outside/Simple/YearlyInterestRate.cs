using System;
using Koolawong.InterestCalculator.Model.Inside.Simple;

namespace Koolawong.InterestCalculator.Model.Outside.Simple
{
    public class YearlyInterestRate
    {
        private readonly decimal _value;

        private YearlyInterestRate(decimal value)
        {
            if (value < -1m)
                throw new ArgumentOutOfRangeException(nameof(value), "Value must be -100% or higher.");
            if (value > 1m)
                throw new ArgumentOutOfRangeException(nameof(value), "Value must be 100% or lower.");

            _value = value;
        }

        public static YearlyInterestRate From(decimal percentage)
        {
            return new YearlyInterestRate(percentage);
        }

        public static explicit operator decimal(YearlyInterestRate rate)
        {
            return rate._value;
        }

        public static TermInterestRate operator /(YearlyInterestRate yearlyInterestRate, TermsPerYear termsPerYear)
        {
            return TermInterestRate.From((decimal)yearlyInterestRate / (int)termsPerYear);
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

        private bool Equals(YearlyInterestRate obj)
        {
            return
                obj != null &&
                _value == obj._value;
        }
    }
}
