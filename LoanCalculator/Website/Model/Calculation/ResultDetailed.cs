namespace Website.Model.Calculation
{
    public class ResultDetailed
    {
        public int Year { get; set; }

        public decimal Payment { get; set; }

        public decimal Repayment { get; set; }

        public decimal InterestRate { get; set; }

        public decimal Contribution { get; set; }

        public decimal LoanLeft { get; set; }
    }
}