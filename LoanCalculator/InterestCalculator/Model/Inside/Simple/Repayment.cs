using System;

namespace Koolawong.InterestCalculator.Model.Inside.Simple
{
    public class Repayment
    {
        private readonly decimal _value;

        private Repayment(decimal value)
        {
            _value = value;
        }

        public static Repayment From(decimal value)
        {
            return new Repayment(value);
        }

        public static explicit operator decimal(Repayment repayment)
        {
            return repayment._value;
        }

        public static Repayment operator +(Repayment a, Repayment b)
        {
            return From(a._value + b._value);
        }

        public static Repayment operator *(Repayment repayment, TermRatio termRatio)
        {
            return From(repayment._value * (decimal)termRatio);
        }

        public override bool Equals(object obj)
        {
            return Equals((dynamic)obj);
        }

        private bool Equals(Repayment obj)
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
