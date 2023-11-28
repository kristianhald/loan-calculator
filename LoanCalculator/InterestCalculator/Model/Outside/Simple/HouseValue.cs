
using Koolawong.InterestCalculator.Model.Inside.Simple;

namespace Koolawong.InterestCalculator.Model.Outside.Simple
{
    public class HouseValue
    {
        private readonly decimal _value;

        private HouseValue(decimal value)
        {
            _value = value;
        }

        public static HouseValue From(decimal value)
        {
            return new HouseValue(value);
        }

        public static explicit operator decimal(HouseValue houseValue)
        {
            return houseValue._value;
        }

        public static TotalPayout operator -(HouseValue houseValue, Savings savings)
        {
            return TotalPayout.From(houseValue._value - (decimal)savings);
        }

        public override bool Equals(object obj)
        {
            return Equals((dynamic)obj);
        }

        private bool Equals(HouseValue obj)
        {
            return
                obj != null &&
                System.Math.Round(_value, 2) == System.Math.Round(obj._value, 2);
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