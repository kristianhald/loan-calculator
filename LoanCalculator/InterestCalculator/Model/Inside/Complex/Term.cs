using System;
using Koolawong.InterestCalculator.Model.Inside.Simple;
using Koolawong.InterestCalculator.Model.Outside.Simple;

namespace Koolawong.InterestCalculator.Model.Inside.Complex
{
    public class Term
    {
        private readonly int _value;

        private Term(int value)
        {
            if (value <= 0)
                throw new ArgumentOutOfRangeException("value", "Value must be 1 or higher.");

            _value = value;
        }

        public static Term First => From(1);

        public static Term From(int value)
        {
            return new Term(value);
        }

        public static explicit operator int(Term term)
        {
            return term._value;
        }

        public static Term operator ++(Term term)
        {
            return From(term._value + 1);
        }

        public static Term operator +(Term a, Term b)
        {
            return From(a._value + b._value);
        }

        public static TermOffset operator -(Term a, Term b)
        {
            return TermOffset.From(a._value - b._value);
        }

        public static Term operator +(Term a, TermOffset b)
        {
            return From(a._value + (int)b);
        }

        public static Year operator /(Term term, TermsPerYear termsPerYear)
        {
            return Year.From((int)Math.Ceiling(term._value / (decimal)(int)termsPerYear));
        }

        public static bool operator <(Term a, TermsPerYear b)
        {
            return a._value < (int)b;
        }

        public static bool operator <=(Term a, TermsPerYear b)
        {
            return a._value <= (int)b;
        }

        public static bool operator >(Term a, TermsPerYear b)
        {
            return !(a <= b);
        }

        public static bool operator >=(Term a, TermsPerYear b)
        {
            return !(a < b);
        }

        public static bool operator <(Term a, Terms b)
        {
            return a._value < (int)b;
        }

        public static bool operator <=(Term a, Terms b)
        {
            return a._value <= (int)b;
        }

        public static bool operator >(Term a, Terms b)
        {
            return !(a <= b);
        }

        public static bool operator >=(Term a, Terms b)
        {
            return !(a < b);
        }

        public override bool Equals(object obj)
        {
            return Equals((dynamic)obj);
        }

        private bool Equals(Term obj)
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
            return $"{_value.ToString()} term";
        }
    }
}
