using System;
using System.Linq;
using Xunit;

namespace Calculator.Test
{
    public class TotalKreditTests
    {
        public class LoanTests
        {
            [Fact]
            public void Example1()
            {
                var value = 3900000m;
                var loanAmount = 3096098m;
                var loanCosts = 17530m;

                var rate = new AverageContributionRate(
                    loanAmount + loanCosts,
                    value,
                    new[]
                    {
                        new ContributionRate(40, 0.0035m),
                        new ContributionRate(60, 0.0075m),
                        new ContributionRate(90, 0.0100m)
                    });

                var loan = new Loan(
                    new DateTime(2016, 2, 26),
                    loanAmount,
                    loanCosts,
                    0.03m,
                    rate.Value,
                    99.67m,
                    30);

                Printer.PrintPaymentPlan(loan, new DateTime(2016, 2, 26));

                Assert.Equal(3124000m, loan.Principal);
                Assert.Equal(1956384m.RoundToNearestThousand(), loan.PaymentPlan.Sum(plan => plan.Contribution + plan.Interest).RoundToNearestThousand());
                Assert.Equal(3124000m.RoundToNearestThousand(), loan.PaymentPlan.Sum(plan => plan.Repayment).RoundToNearestThousand());
                Assert.Equal(5080384m.RoundToNearestThousand(), loan.PaymentPlan.Sum(plan => plan.TotalPayment).RoundToNearestThousand());
            }
        }

        public class ContributionRateTests
        {
            [Fact]
            public void Example1()
            {
                var expectedContributionRate = 0.0061170698619102860071916106m;
                var value = 3900000m;
                var loan = 3096098m + 17530m;

                var contributionRates = new[]
                {
                    new ContributionRate(40, 0.0035m),
                    new ContributionRate(60, 0.0075m),
                    new ContributionRate(90, 0.0100m)
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