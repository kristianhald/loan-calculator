using System;

namespace Koolawong.InterestCalculator.Model.Inside.Simple
{
    public class TotalPayout
    {
        private readonly decimal _value;

        private TotalPayout(decimal value)
        {
            _value = value;
        }

        public static TotalPayout From(decimal value)
        {
            return new TotalPayout(value);
        }

        public static explicit operator decimal(TotalPayout totalPayout)
        {
            return totalPayout._value;
        }

        public static OwnPayment operator -(TotalPayout totalPayout, UpperBankLoanLimit upperBankLoanLimit)
        {
            var ownPayment = totalPayout._value - (decimal)upperBankLoanLimit;
            if (ownPayment < 0m)
                ownPayment = 0;

            return OwnPayment.From(ownPayment);
        }

        public static BankAndMortgagePayout operator -(TotalPayout totalPayout, OwnPayment ownPayment)
        {
            var bankAndMortgagePayout = totalPayout._value - (decimal)ownPayment;

            return BankAndMortgagePayout.From(bankAndMortgagePayout);
        }

        public override bool Equals(object obj)
        {
            return Equals((dynamic)obj);
        }

        private bool Equals(TotalPayout obj)
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