using Koolawong.InterestCalculator.Model.Inside.Complex;
using Koolawong.InterestCalculator.Model.Inside.Simple;
using System;

namespace Koolawong.InterestCalculator.Services.Data
{
    public class LoanInformation
    {
        public readonly Principal Principal;
        public readonly PeriodPaymentPlan PaymentPlan;
        public readonly BankPayout BankLoanPayout;

        public LoanInformation(Principal principal, PeriodPaymentPlan paymentPlan, BankPayout bankLoanPayout)
        {
            if (principal == null)
                throw new ArgumentNullException(nameof(principal));
            if (paymentPlan == null)
                throw new ArgumentNullException(nameof(paymentPlan));
            if(bankLoanPayout == null)
                throw new ArgumentNullException(nameof(bankLoanPayout));

            Principal = principal;
            PaymentPlan = paymentPlan;
            BankLoanPayout = bankLoanPayout;
        }

        public override bool Equals(object obj)
        {
            return Equals((dynamic)obj);
        }

        private bool Equals(LoanInformation obj)
        {
            return
                obj != null &&
                Principal.Equals(obj.Principal) &&
                PaymentPlan.Equals(obj.PaymentPlan);
        }

        public override int GetHashCode()
        {
            return Principal.GetHashCode();
        }

        public override string ToString()
        {
            return $"{Principal} - {PaymentPlan}";
        }
    }
}
