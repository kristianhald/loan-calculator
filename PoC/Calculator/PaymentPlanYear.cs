namespace Calculator
{
    public class PaymentPlanYear
    {
        public PaymentPlanYear(
            decimal paymentWithoutContribution,
            decimal interest,
            decimal repayment,
            decimal contribution,
            decimal loanLeft)
        {
            PaymentWithoutContribution = paymentWithoutContribution;
            Interest = interest;
            Repayment = repayment;
            Contribution = contribution;
            LoanLeft = loanLeft;
        }

        public decimal PaymentWithoutContribution { get; }

        public decimal Interest { get; }

        public decimal Repayment { get; }

        public decimal Contribution { get; }

        public decimal LoanLeft { get; }

        public decimal TotalPayment => PaymentWithoutContribution + Contribution;

        public override string ToString()
        {
            return $"Payment: {PaymentWithoutContribution} - Interest: {Interest} - Repayment: {Repayment} - Contribution: {Contribution} - LoanLeft: {LoanLeft}";
        }
    }
}