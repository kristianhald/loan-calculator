using Koolawong.InterestCalculator.Model.Outside.Simple;

namespace Koolawong.InterestCalculator.Model.Inside.Simple
{
    public class ValueOfRate
    {
        private readonly decimal _value;

        private ValueOfRate(decimal value)
        {
            _value = value;
        }

        public static ValueOfRate From(decimal value)
        {
            return new ValueOfRate(value);
        }

        public static Contribution operator *(ValueOfRate a, YearlyContributionRate b)
        {
            return Contribution.From(a._value * (decimal)b);
        }

        public override bool Equals(object obj)
        {
            return Equals((dynamic)obj);
        }

        private bool Equals(ValueOfRate obj)
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