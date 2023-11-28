using System;
using Koolawong.InterestCalculator.Model.Outside.Simple;

namespace Koolawong.InterestCalculator.Model.Inside.Simple
{
    public class MortgagePayout
    {
        private readonly decimal _value;

        private MortgagePayout(decimal value)
        {
            _value = value;
        }

        public static MortgagePayout From(decimal value)
        {
            return new MortgagePayout(value);
        }

        public static explicit operator decimal(MortgagePayout payout)
        {
            return payout._value;
        }

        public static implicit operator PriorityLoan(MortgagePayout payout)
        {
            return PriorityLoan.From(payout._value);
        }

        public static Principal operator /(MortgagePayout payout, ExchangeRate exchangeRate)
        {
            return Principal.From((decimal)payout / (decimal)exchangeRate);
        }

        public static LoanToValue operator /(MortgagePayout payout, HouseValue value)
        {
            return LoanToValue.From(payout._value / (decimal)value);
        }

        public static PriorityLoan operator +(MortgagePayout payout, PriorityLoan priorityLoan)
        {
            return PriorityLoan.From(payout._value + (decimal)priorityLoan);
        }

        public override bool Equals(object obj)
        {
            return Equals((dynamic)obj);
        }

        private bool Equals(MortgagePayout obj)
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