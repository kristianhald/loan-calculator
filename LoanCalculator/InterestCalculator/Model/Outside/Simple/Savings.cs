using System;

namespace Koolawong.InterestCalculator.Model.Outside.Simple
{
    public class Savings
    {
        private readonly decimal _value;

        private Savings(decimal value)
        {
            if (value < 0m)
                throw new ArgumentOutOfRangeException(nameof(value), "Value cannot be less than zero.");

            _value = value;
        }

        public static Savings From(decimal value)
        {
            return new Savings(value);
        }

        public static explicit operator decimal(Savings savings)
        {
            return savings._value;
        }

        public override bool Equals(object obj)
        {
            return Equals((dynamic)obj);
        }

        private bool Equals(Savings obj)
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