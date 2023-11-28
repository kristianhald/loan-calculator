using System;
using System.Collections.Generic;
using System.Linq;
using Koolawong.InterestCalculator.Model.Inside.Simple;
using Koolawong.InterestCalculator.Model.Outside.Simple;

namespace Koolawong.InterestCalculator.Model.Inside.Complex
{
    public class PeriodPaymentPlan
    {
        // Q: What does a payment plan show of information?
        // A: It should contain a list of payment information by term.
        //    Inside each payment term it should have:
        //      - Payment with contribution
        //      - Repayment
        //      - Interest
        //      - Contribution
        //      - Loan left
        //
        //    Also it should allow access both to the payment plan by term, but 
        //    also an aggregated version per year. This requires the payments 
        //    per term to know which term and year they are part of. It might also
        //    use the initial calculation date to move the terms into the correct year.

        private readonly TermsPerYear _termsPerYear;
        private readonly CalculationDate _calculationDate;

        private PeriodPaymentPlan(
            IEnumerable<TermPaymentPlan> termPaymentPlan,
            TermsPerYear termsPerYear,
            CalculationDate calculationDate)
        {
            if (termPaymentPlan == null)
                throw new ArgumentNullException(nameof(termPaymentPlan));
            if (!termPaymentPlan.Any())
                throw new ArgumentOutOfRangeException(nameof(termPaymentPlan), "Plan must contain atleast one term.");
            if (termsPerYear == null)
                throw new ArgumentNullException(nameof(termsPerYear));
            if (calculationDate == null)
                throw new ArgumentNullException(nameof(calculationDate));

            PlanByTerms = termPaymentPlan;
            _termsPerYear = termsPerYear;
            _calculationDate = calculationDate;
        }

        public static PeriodPaymentPlan From(
            IEnumerable<TermPaymentPlan> periodPaymentPlan,
            TermsPerYear termsPerYear,
            CalculationDate calculationDate)
        {
            return new PeriodPaymentPlan(periodPaymentPlan, termsPerYear, calculationDate);
        }

        public IEnumerable<TermPaymentPlan> PlanByTerms { get; }

        public IEnumerable<YearlyPaymentPlan> PlanByYears
        {
            get
            {
                var startTermOfCalculation = _calculationDate.InTerm(_termsPerYear);
                var termOffset = startTermOfCalculation - Term.First;

                var repayment = Repayment.From(0);
                var interest = Interest.From(0);
                var contribution = Contribution.From(0);
                var paymentLeft = PaymentLeft.From(0);

                // Might want the 'PlanByTerms.Count()' to be something like 'GetNumberOfTerms'
                for (var term = Term.First; term <= Terms.From(PlanByTerms.Count()); term++)
                {
                    repayment += GetTerm(term).Repayment;
                    interest += GetTerm(term).Interest;
                    contribution += GetTerm(term).Contribution;

                    // Make this a part of the term
                    if ((int)(term + termOffset) % (int)_termsPerYear == 0 ||
                        term.Equals(Term.From(PlanByTerms.Count())))
                    {
                        yield return YearlyPaymentPlan.From(
                            (term + termOffset) / _termsPerYear,
                            repayment,
                            interest,
                            contribution,
                            GetTerm(term).PaymentLeft);

                        repayment = Repayment.From(0);
                        interest = Interest.From(0);
                        contribution = Contribution.From(0);
                    }
                }
            }
        }

        public TermPaymentPlan GetTerm(Term term)
        {
            return PlanByTerms.ElementAt((int)term - 1);
        }

        public override string ToString()
        {
            return String.Join(Environment.NewLine, PlanByTerms);
        }

        public override bool Equals(object obj)
        {
            return Equals((dynamic)obj);
        }

        public bool Equals(PeriodPaymentPlan obj)
        {
            return
                obj != null &&
                _termsPerYear.Equals(obj._termsPerYear) &&
                PlanByTerms.SequenceEqual(obj.PlanByTerms);
        }

        public override int GetHashCode()
        {
            return PlanByTerms.Count().GetHashCode();
        }
    }
}
