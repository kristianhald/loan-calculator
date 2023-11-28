using System;
using Koolawong.InterestCalculator.Model.Inside.Simple;

namespace Koolawong.InterestCalculator.Model.Inside.Complex
{
    public class TermPaymentPlan
    {
        private TermPaymentPlan(
            Term term,
            Repayment repayment,
            Interest interest,
            Contribution contribution,
            PaymentLeft paymentLeft)
        {
            if (term == null)
                throw new ArgumentNullException(nameof(term));
            if (repayment == null)
                throw new ArgumentNullException(nameof(repayment));
            if (interest == null)
                throw new ArgumentNullException(nameof(interest));
            if (contribution == null)
                throw new ArgumentNullException(nameof(contribution));
            if (paymentLeft == null)
                throw new ArgumentNullException(nameof(paymentLeft));

            Term = term;
            Repayment = repayment;
            Interest = interest;
            Contribution = contribution;
            PaymentLeft = paymentLeft;
        }

        public static TermPaymentPlan From(
            Term term,
            Repayment repayment,
            Interest interest,
            Contribution contribution,
            PaymentLeft paymentLeft)
        {
            return new TermPaymentPlan(
                term,
                repayment,
                interest,
                contribution,
                paymentLeft);
        }

        public Term Term { get; }

        public Repayment Repayment { get; }

        public Interest Interest { get; }

        public Contribution Contribution { get; }

        public PaymentLeft PaymentLeft { get; }

        public override string ToString()
        {
            return $"{Term} - {Repayment} - {Interest} - {Contribution} - {PaymentLeft}";
        }

        public override bool Equals(object obj)
        {
            return Equals((dynamic)obj);
        }

        public bool Equals(TermPaymentPlan obj)
        {
            return
                obj != null &&
                Term.Equals(obj.Term) &&
                Repayment.Equals(obj.Repayment) &&
                Interest.Equals(obj.Interest) &&
                Contribution.Equals(obj.Contribution) &&
                PaymentLeft.Equals(obj.PaymentLeft);
        }

        public override int GetHashCode()
        {
            return Term.GetHashCode();
        }
    }
}
