using System;

namespace Koolawong.InterestCalculator.Model.Inside.Simple
{
    public class TermPayment
    {
        private readonly decimal _value;

        private TermPayment(decimal value)
        {
            _value = value;
        }

        public static TermPayment From(decimal value)
        {
            return new TermPayment(value);
        }

        public static explicit operator decimal(TermPayment termPayment)
        {
            return termPayment._value;
        }

        public override bool Equals(object obj)
        {
            return Equals((dynamic)obj);
        }

        private bool Equals(TermPayment obj)
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
