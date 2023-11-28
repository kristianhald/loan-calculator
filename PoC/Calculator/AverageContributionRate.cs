using System.Collections.Generic;

namespace Calculator
{
    public class AverageContributionRate
    {
        public AverageContributionRate(
            decimal amount,
            decimal value,
            IEnumerable<ContributionRate> rates)
        {
            var ltv = amount / value * 100m;

            var totalContribution = 0m;
            ContributionRate lastContributionRate = new ContributionRate(0m, 0m);
            foreach (var rate in rates)
            {
                if (lastContributionRate.UpperPercentage > ltv)
                    break;

                var valueOfRate = rate.UpperPercentage < ltv
                    ? (rate.UpperPercentage - lastContributionRate.UpperPercentage) / 100m * value
                    : (ltv - lastContributionRate.UpperPercentage) / 100m * value;
                totalContribution += valueOfRate * rate.Rate;

                lastContributionRate = rate;
            }

            Value = totalContribution / amount;
        }

        public decimal Value { get; }
    }
}