using System;
using Koolawong.InterestCalculator.Model.Outside.Simple;

namespace Koolawong.InterestCalculator.Model.Inside.Simple
{
    public class Contribution
    {
        private readonly decimal _value;

        private Contribution(decimal value)
        {
            _value = value;
        }

        public static Contribution From(decimal value)
        {
            return new Contribution(value);
        }

        public static explicit operator decimal(Contribution contribution)
        {
            return contribution._value;
        }

        public static Contribution operator +(Contribution a, Contribution b)
        {
            return From(a._value + b._value);
        }

        public static YearlyContributionRate operator /(Contribution contribution, MortgagePayout payout)
        {
            return YearlyContributionRate.From(contribution._value / (decimal)payout);
        }

        public static Contribution operator *(Contribution contribution, TermRatio termRatio)
        {
            return From(contribution._value * (decimal)termRatio);
        }

        public override bool Equals(object obj)
        {
            return Equals((dynamic)obj);
        }

        private bool Equals(Contribution obj)
        {
            return
                obj != null &&
                Math.Abs(_value - obj._value) <= 0.05m;
        }

        public override int GetHashCode()
        {
            return _value.GetHashCode();
        }

        public override string ToString()
        {
            return $"{_value.ToString("C")}";
        }
    }
}