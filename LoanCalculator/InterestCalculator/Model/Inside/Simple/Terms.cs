using System;

namespace Koolawong.InterestCalculator.Model.Inside.Simple
{
    public class Terms
    {
        private readonly int _value;

        private Terms(int value, bool doValidation)
        {
            if (value <= 0 && doValidation)
                throw new ArgumentOutOfRangeException("value", "Value must be 1 or higher.");

            _value = value;
        }

        public static Terms From(int value)
        {
            return new Terms(value, true);
        }

        public static explicit operator int(Terms terms)
        {
            return terms._value;
        }

        public static Terms operator -(Terms terms)
        {
            return new Terms(-1 * terms._value, false);
        }

        public override bool Equals(object obj)
        {
            return Equals((dynamic)obj);
        }

        private bool Equals(Terms obj)
        {
            return
                obj != null &&
                _value == obj._value;
        }

        public override int GetHashCode()
        {
            return _value.GetHashCode();
        }

        public override string ToString()
        {
            return $"{_value.ToString()} terms ";
        }
    }
}
