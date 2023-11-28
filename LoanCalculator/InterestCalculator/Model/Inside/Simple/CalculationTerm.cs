using System;
using Koolawong.InterestCalculator.Model.Inside.Complex;
using Koolawong.InterestCalculator.Model.Outside.Simple;

namespace Koolawong.InterestCalculator.Model.Inside.Simple
{
    public class CalculationTerm
    {
        private readonly Term _term;
        private readonly CalculationDate _dateInTerm;
        private readonly TermsPerYear _termsPerYear;

        private CalculationTerm(Term term, CalculationDate dateInTerm, TermsPerYear termsPerYear)
        {
            if (term == null)
                throw new ArgumentNullException(nameof(term));
            if ((int)term > 366)
                throw new ArgumentOutOfRangeException("term", "Value must be 366 or lower as there are no more days in the year, than that.");
            if (dateInTerm == null)
                throw new ArgumentNullException(nameof(dateInTerm));
            if (termsPerYear == null)
                throw new ArgumentNullException(nameof(termsPerYear));

            _term = term;
            _dateInTerm = dateInTerm;
            _termsPerYear = termsPerYear;
        }

        public TermDays DaysInTerm
        {
            get
            {
                var monthsPerTerm = (MonthsPerTerm)_termsPerYear;
                var startOfTerm = new DateTime((int)_dateInTerm.Year, 1, 1).AddMonths(((int)_term - 1) * (int)monthsPerTerm);
                var endOfTerm = startOfTerm.AddMonths((int)monthsPerTerm);
                var durationBetweenDates = (endOfTerm - startOfTerm);

                return TermDays.From((int)durationBetweenDates.TotalDays);
            }
}

        public TermDays DaysLeftInTerm
        {
            get
            {
                var monthsPerTerm = (MonthsPerTerm)_termsPerYear;
                var startOfTerm = new DateTime((int)_dateInTerm.Year, 1, 1).AddMonths(((int)_term - 1) * (int)monthsPerTerm);
                var endOfTerm = startOfTerm.AddMonths((int)monthsPerTerm);
                var durationBetweenDates = (endOfTerm - (DateTime)_dateInTerm);

                return TermDays.From((int)durationBetweenDates.TotalDays);
            }
        }

public static CalculationTerm From(Term term, CalculationDate dateInTerm, TermsPerYear termsPerYear)
{
    return new CalculationTerm(term, dateInTerm, termsPerYear);
}

public static implicit operator Term(CalculationTerm calculationTerm)
{
    return calculationTerm._term;
}

public override bool Equals(object obj)
{
    return Equals((dynamic)obj);
}

private bool Equals(CalculationTerm obj)
{
    return
        obj != null &&
        _term.Equals(obj._term) &&
        _dateInTerm.Equals(obj._dateInTerm) &&
        _termsPerYear.Equals(obj._termsPerYear);
}

public override int GetHashCode()
{
    return _dateInTerm.GetHashCode();
}

public override string ToString()
{
    return $"{_term.ToString()} of {_termsPerYear} at the {_dateInTerm}";
}
    }
}
