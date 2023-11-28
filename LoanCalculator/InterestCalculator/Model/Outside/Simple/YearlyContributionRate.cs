using System;
using Koolawong.InterestCalculator.Model.Inside.Simple;

namespace Koolawong.InterestCalculator.Model.Outside.Simple
{
    public class YearlyContributionRate
    {
        private readonly decimal _value;

        private YearlyContributionRate(decimal value)
        {
            if (value < 0m)
                throw new ArgumentOutOfRangeException(nameof(value), "Value must be zero or higher.");
            if (value > 1m)
                throw new ArgumentOutOfRangeException(nameof(value), "Value must be 100% or lower.");

            _value = value;
        }

        public static YearlyContributionRate From(decimal percentage)
        {
            return new YearlyContributionRate(percentage);
        }

        public static explicit operator decimal(YearlyContributionRate rate)
        {
            return rate._value;
        }

        public static TermContributionRate operator /(YearlyContributionRate yearlyContributionRate, TermsPerYear termsPerYear)
        {
            return TermContributionRate.From((decimal)yearlyContributionRate / (int)termsPerYear);
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

        private bool Equals(YearlyContributionRate obj)
        {
            return
                obj != null &&
                _value == obj._value;
        }
    }
}