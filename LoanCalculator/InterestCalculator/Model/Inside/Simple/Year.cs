using System;

namespace Koolawong.InterestCalculator.Model.Inside.Simple
{
    public class Year
    {
        private readonly int _value;

        private Year(int value)
        {
            if (value <= 0)
                throw new ArgumentOutOfRangeException("value", "Value must be 1 or higher.");

            _value = value;
        }

        public static Year From(int value)
        {
            return new Year(value);
        }

        public static explicit operator int(Year year)
        {
            return year._value;
        }

        public static Year operator ++(Year year)
        {
            return From(year._value + 1);
        }

        public override bool Equals(object obj)
        {
            return Equals((dynamic)obj);
        }

        private bool Equals(Year obj)
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
            return $"Year {_value.ToString()}";
        }
    }
}
