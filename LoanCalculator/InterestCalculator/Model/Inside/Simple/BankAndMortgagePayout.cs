using System;

namespace Koolawong.InterestCalculator.Model.Inside.Simple
{
    public class BankAndMortgagePayout
    {
        private readonly decimal _value;

        private BankAndMortgagePayout(decimal value)
        {
            _value = value;
        }

        public static BankAndMortgagePayout From(decimal value)
        {
            return new BankAndMortgagePayout(value);
        }

        public static explicit operator decimal(BankAndMortgagePayout bankAndMortgagePayout)
        {
            return bankAndMortgagePayout._value;
        }

        public static BankPayout operator -(BankAndMortgagePayout bankAndMortgagePayout, UpperMortgageLimit upperMortgageLimit)
        {
            var bankLoanPayout = bankAndMortgagePayout._value - (decimal)upperMortgageLimit;
            if (bankLoanPayout < 0m)
                bankLoanPayout = 0;

            return BankPayout.From(bankLoanPayout);
        }

        public static MortgagePayout operator -(BankAndMortgagePayout bankAndMortgagePayout, BankPayout bankLoanPayout)
        {
            return MortgagePayout.From(bankAndMortgagePayout._value - (decimal)bankLoanPayout);
        }

        public override bool Equals(object obj)
        {
            return Equals((dynamic)obj);
        }

        private bool Equals(BankAndMortgagePayout obj)
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