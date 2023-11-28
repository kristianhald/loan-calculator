using System;

namespace Koolawong.InterestCalculator.Model.Inside.Simple
{
    public class BankPayout
    {
        private readonly decimal _value;

        private BankPayout(decimal value)
        {
            _value = value;
        }

        public static BankPayout From(decimal value)
        {
            return new BankPayout(value);
        }

        public static explicit operator decimal(BankPayout payout)
        {
            return payout._value;
        }

        public override bool Equals(object obj)
        {
            return Equals((dynamic)obj);
        }

        private bool Equals(BankPayout obj)
        {
            return
                obj != null &&
                Math.Round(_value, 2) == Math.Round(obj._value, 2);
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