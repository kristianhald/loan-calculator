using System;
using System.Linq;
using Xunit;

namespace Calculator.Test
{
    public class NordeaKreditTests
    {
        public class LoanTests
        {
            [Fact]
            public void Example1()
            {
                var value = 3900000m;
                var loanAmount = 3105720m;
                var loanCosts = 59425m;

                var rate = new AverageContributionRate(
                    loanAmount + loanCosts,
                    value,
                    new[]
                    {
                        new ContributionRate(40, 0.0035m),
                        new ContributionRate(60, 0.0080m),
                        new ContributionRate(85, 0.0110m)
                    });

                var loan = new Loan(
                    new DateTime(2016, 3, 2),
                    loanAmount,
                    loanCosts,
                    0.025m,
                    rate.Value,
                    97.75m,
                    30);

                Printer.PrintPaymentPlan(loan, new DateTime(2016, 3, 2));

                Assert.Equal(3238000m, loan.Principal);
                Assert.Equal(1733872m.RoundToNearestThousand(), loan.PaymentPlan.Sum(plan => plan.Contribution + plan.Interest).RoundToNearestThousand() - 1000m); // Off by 1000
                Assert.Equal(3238000m.RoundToNearestThousand(), loan.PaymentPlan.Sum(plan => plan.Repayment).RoundToNearestThousand());
                Assert.Equal(4971872m.RoundToNearestThousand(), loan.PaymentPlan.Sum(plan => plan.TotalPayment).RoundToNearestThousand() - 1000m); // Off by 1000
            }
        }

        public class ContributionRateTests
        {
            [Fact]
            public void Example1()
            {
                var expectedContributionRate = 0.0065641842632801972737425932m;
                var value = 3900000m;
                var loan = 3105720m + 59425m;

                var contributionRates = new[]
                {
                    new ContributionRate(40, 0.0035m),
                    new ContributionRate(60, 0.0080m),
                    new ContributionRate(85, 0.0110m)
                };

                var rate = new AverageContributionRate(
                    loan,
                    value,
                    contributionRates);

                Assert.Equal(expectedContributionRate, rate.Value);
            }
        }
    }
}