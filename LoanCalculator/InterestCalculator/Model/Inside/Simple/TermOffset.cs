using System;

namespace Koolawong.InterestCalculator.Model.Inside.Simple
{
    public class TermOffset
    {
        private readonly int _value;

        private TermOffset(int value)
        {
            if (value < 0)
                throw new ArgumentOutOfRangeException("value", "Value must be 0 or higher.");

            _value = value;
        }

        public static TermOffset Zero => From(0);

        public static TermOffset From(int value)
        {
            return new TermOffset(value);
        }

        public static explicit operator int(TermOffset termOffset)
        {
            return termOffset._value;
        }

        public override bool Equals(object obj)
        {
            return Equals((dynamic)obj);
        }

        private bool Equals(TermOffset obj)
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
            return $"{_value.ToString()} term offset";
        }
    }
}
