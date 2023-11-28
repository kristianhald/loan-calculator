using Koolawong.InterestCalculator.Model.Inside.Simple;

namespace Koolawong.InterestCalculator.Services.Data
{
    public class PayoutDistribution
    {
        public PayoutDistribution(
            MortgagePayout mortgagePayout,
            BankPayout bankLoanPayout,
            OwnPayment ownPayment)
        {
            MortgagePayout = mortgagePayout;
            BankLoanPayout = bankLoanPayout;
            OwnPayment = ownPayment;
        }

        public MortgagePayout MortgagePayout { get; }

        public BankPayout BankLoanPayout { get; }

        public OwnPayment OwnPayment { get; }

        public override bool Equals(object obj)
        {
            return Equals((dynamic)obj);
        }

        private bool Equals(PayoutDistribution payoutDistribution)
        {
            return
                payoutDistribution != null &&
                payoutDistribution.MortgagePayout.Equals(MortgagePayout) &&
                payoutDistribution.BankLoanPayout.Equals(BankLoanPayout) &&
                payoutDistribution.OwnPayment.Equals(OwnPayment);
        }
    }
}