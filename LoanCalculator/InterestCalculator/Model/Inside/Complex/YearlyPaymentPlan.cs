using System;
using Koolawong.InterestCalculator.Model.Inside.Simple;

namespace Koolawong.InterestCalculator.Model.Inside.Complex
{
    public class YearlyPaymentPlan
    {
        private YearlyPaymentPlan(
            Year year,
            Repayment repayment,
            Interest interest,
            Contribution contribution,
            PaymentLeft paymentLeft)
        {
            if (year == null)
                throw new ArgumentNullException(nameof(year));
            if (repayment == null)
                throw new ArgumentNullException(nameof(repayment));
            if (interest == null)
                throw new ArgumentNullException(nameof(interest));
            if (contribution == null)
                throw new ArgumentNullException(nameof(contribution));
            if (paymentLeft == null)
                throw new ArgumentNullException(nameof(paymentLeft));

            Year = year;
            Repayment = repayment;
            Interest = interest;
            Contribution = contribution;
            PaymentLeft = paymentLeft;
        }

        public static YearlyPaymentPlan From(
            Year year,
            Repayment repayment,
            Interest interest,
            Contribution contribution,
            PaymentLeft paymentLeft)
        {
            return new YearlyPaymentPlan(
                year,
                repayment,
                interest,
                contribution,
                paymentLeft);
        }

        public Year Year { get; }

        public Repayment Repayment { get; }

        public Interest Interest { get; }

        public Contribution Contribution { get; }

        public PaymentLeft PaymentLeft { get; }

        public override string ToString()
        {
            return $"{Year} - {Repayment} - {Interest} - {Contribution} - {PaymentLeft}";
        }

        public override bool Equals(object obj)
        {
            return Equals((dynamic)obj);
        }

        public bool Equals(YearlyPaymentPlan obj)
        {
            return
                obj != null &&
                Year.Equals(obj.Year) &&
                Repayment.Equals(obj.Repayment) &&
                Interest.Equals(obj.Interest) &&
                Contribution.Equals(obj.Contribution) &&
                PaymentLeft.Equals(obj.PaymentLeft);
        }

        public override int GetHashCode()
        {
            return Year.GetHashCode();
        }
    }
}
