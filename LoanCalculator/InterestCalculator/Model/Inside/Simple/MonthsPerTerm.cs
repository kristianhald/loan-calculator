using System;
using Koolawong.InterestCalculator.Model.Inside.Complex;

namespace Koolawong.InterestCalculator.Model.Inside.Simple
{
    public class MonthsPerTerm
    {
        private readonly int _value;

        private MonthsPerTerm(int value)
        {
            if (value <= 0)
                throw new ArgumentOutOfRangeException("value", "Value must be 1 or higher. It does not support less than monthly terms");
            if (value > 12)
                throw new ArgumentOutOfRangeException("value", "Value must be 12 or lower. It does not support more than yearly terms");

            _value = value;
        }

        public static MonthsPerTerm From(int value)
        {
            return new MonthsPerTerm(value);
        }

        public static explicit operator int(MonthsPerTerm monthsPerTerm)
        {
            return monthsPerTerm._value;
        }

        public static MonthInYear operator *(MonthsPerTerm a, Term b)
        {
            return MonthInYear.From(a._value * (int)b);
        }

        public override bool Equals(object obj)
        {
            return Equals((dynamic)obj);
        }

        private bool Equals(MonthsPerTerm obj)
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
            return $"{_value.ToString()} months per term";
        }
    }
}