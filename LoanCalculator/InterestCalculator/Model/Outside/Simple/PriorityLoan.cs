using System;

namespace Koolawong.InterestCalculator.Model.Outside.Simple
{
    public class PriorityLoan // Not too happy about this name. Should maybe be something with payout?
    {
        private readonly decimal _value;

        private PriorityLoan(decimal value)
        {
            _value = value;
        }

        public static PriorityLoan From(decimal value)
        {
            return new PriorityLoan(value);
        }

        public static explicit operator decimal(PriorityLoan priority)
        {
            return priority._value;
        }

        public static LoanToValue operator /(PriorityLoan priority, HouseValue value)
        {
            return LoanToValue.From(priority._value / (decimal)value);
        }

        public override bool Equals(object obj)
        {
            return Equals((dynamic)obj);
        }

        private bool Equals(PriorityLoan obj)
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