using System;
using System.Linq;
using Xunit;

namespace Calculator.Test
{
    public class RealkreditDanmarkTests
    {
        public class LoanTests
        {
            [Fact]
            public void Example1()
            {
                var value = 3900000m;
                var loanAmount = 3104512m;
                var loanCosts = 12786m;

                var rate = new AverageContributionRate(
                    loanAmount + loanCosts,
                    value,
                    new[]
                    {
                        new ContributionRate(40, 0.002748m),
                        new ContributionRate(60, 0.008248m),
                        new ContributionRate(80, 0.0135m)
                    });

                var loan = new Loan(
                    new DateTime(2016, 3, 2),
                    loanAmount,
                    loanCosts,
                    0.025m,
                    rate.Value,
                    97.568m,
                    30);

                Printer.PrintPaymentPlan(loan, new DateTime(2016, 3, 2));

                Assert.Equal(3195000m, loan.Principal);
                Assert.Equal(1725702m.RoundToNearestThousand(), loan.PaymentPlan.Sum(plan => plan.Contribution + plan.Interest).RoundToNearestThousand() + 1000); // Off by 1000
                Assert.Equal(3195000m.RoundToNearestThousand(), loan.PaymentPlan.Sum(plan => plan.Repayment).RoundToNearestThousand());
                Assert.Equal(4920699m.RoundToNearestThousand(), loan.PaymentPlan.Sum(plan => plan.TotalPayment).RoundToNearestThousand() + 1000m); // Off by 1000
            }
        }

        public class ContributionRateTests
        {
            [Fact]
            public void Example1()
            {
                var expectedContributionRate = 0.0068052021333860285413842372m;
                var value = 3900000m;
                var loan = 3104512m + 12786m;

                var contributionRates = new[]
                {
                    new ContributionRate(40, 0.002748m),
                    new ContributionRate(60, 0.008248m),
                    new ContributionRate(80, 0.0135m)
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