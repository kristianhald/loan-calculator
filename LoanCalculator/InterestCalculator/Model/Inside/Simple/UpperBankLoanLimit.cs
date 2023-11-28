using System;

namespace Koolawong.InterestCalculator.Model.Inside.Simple
{
    public class UpperBankLoanLimit
    {
        private readonly decimal _value;

        private UpperBankLoanLimit(decimal value)
        {
            _value = value;
        }

        public static UpperBankLoanLimit From(decimal value)
        {
            return new UpperBankLoanLimit(value);
        }

        public static explicit operator decimal(UpperBankLoanLimit upperBankLoanLimit)
        {
            return upperBankLoanLimit._value;
        }

        public override bool Equals(object obj)
        {
            return Equals((dynamic)obj);
        }

        private bool Equals(UpperBankLoanLimit obj)
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