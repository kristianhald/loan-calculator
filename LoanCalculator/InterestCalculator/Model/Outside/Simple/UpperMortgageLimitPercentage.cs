using Koolawong.InterestCalculator.Model.Inside.Simple;
using System;

namespace Koolawong.InterestCalculator.Model.Outside.Simple
{
    public class UpperMortgageLimitPercentage
    {
        private readonly decimal _value;

        private UpperMortgageLimitPercentage(decimal value)
        {
            if (value < 0.6m)
                throw new ArgumentOutOfRangeException(nameof(value), "Value must be 60% or higher.");
            if (value > 0.8m)
                throw new ArgumentOutOfRangeException(nameof(value), "Value must be 80% or lower.");

            _value = value;
        }

        public static UpperMortgageLimitPercentage From(decimal upperMortgageLimit)
        {
            return new UpperMortgageLimitPercentage(upperMortgageLimit);
        }

        public static explicit operator decimal(UpperMortgageLimitPercentage upperMortgageLimit)
        {
            return upperMortgageLimit._value;
        }

        public static UpperMortgageLimit operator *(UpperMortgageLimitPercentage upperMortgageLimitPercentage, HouseValue houseValue)
        {
            return UpperMortgageLimit.From((decimal)houseValue * upperMortgageLimitPercentage._value);
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

        private bool Equals(UpperMortgageLimitPercentage obj)
        {
            return
                obj != null &&
                _value == obj._value;
        }
    }
}