using System;

namespace Koolawong.InterestCalculator.Model.Inside.Simple
{
    public class OwnPayment
    {
        private readonly decimal _value;

        private OwnPayment(decimal value)
        {
            _value = value;
        }

        public static OwnPayment From(decimal value)
        {
            return new OwnPayment(value);
        }

        public static explicit operator decimal(OwnPayment payment)
        {
            return payment._value;
        }

        public override bool Equals(object obj)
        {
            return Equals((dynamic)obj);
        }

        private bool Equals(OwnPayment obj)
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