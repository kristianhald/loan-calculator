using Koolawong.InterestCalculator.Model.Inside.Simple;
using System;

namespace Koolawong.InterestCalculator.Model.Outside.Simple
{
    public class UpperBankLoanLimitPercentage
    {
        private readonly decimal _value;

        private UpperBankLoanLimitPercentage(decimal value)
        {
            if (value < 0.95m)
                throw new ArgumentOutOfRangeException(nameof(value), "Value must be 95% or higher.");
            if (value > 1.0m)
                throw new ArgumentOutOfRangeException(nameof(value), "Value must be 100% or lower.");

            _value = value;
        }

        public static UpperBankLoanLimitPercentage From(decimal upperBankLoanLimit)
        {
            return new UpperBankLoanLimitPercentage(upperBankLoanLimit);
        }

        public static explicit operator decimal(UpperBankLoanLimitPercentage upperBankLoanLimit)
        {
            return upperBankLoanLimit._value;
        }

        public static UpperBankLoanLimit operator *(UpperBankLoanLimitPercentage upperBankLoanLimitPercentage, HouseValue houseValue)
        {
            return UpperBankLoanLimit.From((decimal)houseValue * upperBankLoanLimitPercentage._value);
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

        private bool Equals(UpperBankLoanLimitPercentage obj)
        {
            return
                obj != null &&
                _value == obj._value;
        }
    }
}