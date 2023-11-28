namespace Calculator
{
    public class ContributionRate
    {
        public ContributionRate(decimal upperPercentage, decimal rate)
        {
            UpperPercentage = upperPercentage;
            Rate = rate;
        }

        public decimal UpperPercentage { get; }

        public decimal Rate { get; }
    }
}