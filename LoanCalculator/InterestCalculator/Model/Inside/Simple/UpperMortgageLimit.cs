using System;

namespace Koolawong.InterestCalculator.Model.Inside.Simple
{
    public class UpperMortgageLimit
    {
        private readonly decimal _value;

        private UpperMortgageLimit(decimal value)
        {
            _value = value;
        }

        public static UpperMortgageLimit From(decimal value)
        {
            return new UpperMortgageLimit(value);
        }

        public static explicit operator decimal(UpperMortgageLimit upperMortgageLimit)
        {
            return upperMortgageLimit._value;
        }

        public static BankPayout operator -(UpperMortgageLimit upperMortgageLimit, MortgagePayout payout) // TODO: Delete this. Not a proper calculation
        {
            return BankPayout.From(Math.Max((decimal)payout - upperMortgageLimit._value, 0m));
        }

        public static bool operator <(UpperMortgageLimit upperMortgageLimit, TotalPayout totalPayout)
        {
            return upperMortgageLimit._value < (decimal)totalPayout;
        }

        public static bool operator >(UpperMortgageLimit upperMortgageLimit, TotalPayout totalPayout)
        {
            return upperMortgageLimit._value > (decimal)totalPayout;
        }

        public static bool operator >=(UpperMortgageLimit upperMortgageLimit, TotalPayout totalPayout)
        {
            return !(upperMortgageLimit < totalPayout);
        }

        public static bool operator <=(UpperMortgageLimit upperMortgageLimit, TotalPayout totalPayout)
        {
            return !(upperMortgageLimit > totalPayout);
        }

        public override bool Equals(object obj)
        {
            return Equals((dynamic)obj);
        }

        private bool Equals(UpperMortgageLimit obj)
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