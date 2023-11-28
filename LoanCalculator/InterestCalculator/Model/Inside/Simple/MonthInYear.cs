using System;

namespace Koolawong.InterestCalculator.Model.Inside.Simple
{
    public class MonthInYear
    {
        private readonly int _value;

        private MonthInYear(int value)
        {
            if (value <= 0)
                throw new ArgumentOutOfRangeException("value", "Value must be 1 or higher.");
            if (value > 12)
                throw new ArgumentOutOfRangeException("value", "Value must be 12 or lower. There are no more than 12 months in a year.");

            _value = value;
        }

        public static MonthInYear From(int value)
        {
            return new MonthInYear(value);
        }

        public static explicit operator int(MonthInYear monthInYear)
        {
            return monthInYear._value;
        }

        public static MonthInYear operator +(MonthInYear a, int b)
        {
            return From(a._value + b);
        }

        public override bool Equals(object obj)
        {
            return Equals((dynamic)obj);
        }

        private bool Equals(MonthInYear obj)
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