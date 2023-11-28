using System;

namespace Koolawong.InterestCalculator.Model.Inside.Simple
{
    public class PeriodicalPayment
    {
        private readonly decimal _value;

        private PeriodicalPayment(decimal value)
        {
            _value = value;
        }

        public static PeriodicalPayment From(decimal value)
        {
            return new PeriodicalPayment(value);
        }

        public static explicit operator decimal(PeriodicalPayment periodicalPayment)
        {
            return periodicalPayment._value;
        }

        public static Repayment operator -(PeriodicalPayment periodicalPayment, Interest interest)
        {
            return Repayment.From(periodicalPayment._value - (decimal)interest);
        }
        public override bool Equals(object obj)
        {
            return Equals((dynamic)obj);
        }

        private bool Equals(PeriodicalPayment obj)
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
