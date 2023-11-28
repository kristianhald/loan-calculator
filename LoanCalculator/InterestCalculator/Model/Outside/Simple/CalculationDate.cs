using System;
using Koolawong.InterestCalculator.Model.Inside.Complex;
using Koolawong.InterestCalculator.Model.Inside.Simple;

namespace Koolawong.InterestCalculator.Model.Outside.Simple
{
    public class CalculationDate
    {
        private readonly DateTime _value;

        private CalculationDate(DateTime value)
        {
            if (value == DateTime.MinValue || value == DateTime.MaxValue)
                throw new ArgumentOutOfRangeException("value", "A value must be provided");

            if (value.Date != value)
                throw new ArgumentOutOfRangeException("value", "Value must only contain the date component.");

            _value = value;
        }

        public static CalculationDate From(DateTime value)
        {
            return new CalculationDate(value);
        }

        public static explicit operator DateTime(CalculationDate calculationDate)
        {
            return calculationDate._value;
        }

        public static bool operator <(CalculationDate a, MonthInYear b)
        {
            return a._value.Month < (int)b;
        }

        public static bool operator <=(CalculationDate a, MonthInYear b)
        {
            return a._value.Month <= (int)b;
        }

        public static bool operator >(CalculationDate a, MonthInYear b)
        {
            return !(a <= b);
        }

        public static bool operator >=(CalculationDate a, MonthInYear b)
        {
            return !(a < b);
        }

        public Year Year => Year.From(_value.Year);

        public override bool Equals(object obj)
        {
            return Equals((dynamic)obj);
        }

        private bool Equals(CalculationDate obj)
        {
            return
                obj != null &&
                _value.Equals(obj._value);
        }

        public override int GetHashCode()
        {
            return _value.GetHashCode();
        }

        public override string ToString()
        {
            return $"{_value.ToString("d")}";
        }

        public CalculationTerm InTerm(TermsPerYear termsPerYear)
        {
            var monthsPerTerm = (MonthsPerTerm)termsPerYear;
            for (var term = Term.From(1); term <= termsPerYear; term++)
            {
                if (this <= monthsPerTerm * term)
                    return CalculationTerm.From(term, this, termsPerYear);
            }

            throw new InvalidOperationException("The calculation date could not be put into a term.");
        }
    }
}
