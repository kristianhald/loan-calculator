using System;

namespace Koolawong.InterestCalculator.Model.Inside.Simple
{
    // Not too happy of this one
    public class Ratio
    {
        private readonly decimal _value;

        private Ratio(decimal value)
        {
            _value = value;
        }

        public static Ratio From(decimal value)
        {
            return new Ratio(value);
        }

        public static explicit operator decimal(Ratio ratio)
        {
            return ratio._value;
        }

        public override bool Equals(object obj)
        {
            return Equals((dynamic)obj);
        }

        private bool Equals(Ratio obj)
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