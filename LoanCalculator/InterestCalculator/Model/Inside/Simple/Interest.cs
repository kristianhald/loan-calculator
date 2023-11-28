using System;

namespace Koolawong.InterestCalculator.Model.Inside.Simple
{
    public class Interest
    {
        private readonly decimal _value;

        private Interest(decimal value)
        {
            _value = value;
        }

        public static Interest From(decimal value)
        {
            return new Interest(value);
        }

        public static explicit operator decimal(Interest interest)
        {
            return interest._value;
        }

        public static Interest operator +(Interest a, Interest b)
        {
            return From(a._value + b._value);
        }

        public static Interest operator *(Interest interest, TermRatio termRatio)
        {
            return From(interest._value * (decimal)termRatio);
        }

        public override bool Equals(object obj)
        {
            return Equals((dynamic)obj);
        }

        private bool Equals(Interest obj)
        {
            return
                obj != null &&
                Math.Abs(_value - obj._value) <= 0.05m;
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
