using System;
using System.Linq;
using Xunit;

namespace Calculator.Test
{
    public class BrfKreditTests
    {
        public class LoanTests
        {
            [Fact]
            public void Example1()
            {
                var value = 3900000m;
                var loanAmount = 3000000m;
                var loanCosts = 40560m + 27296m;

                var rate = new AverageContributionRate(
                    loanAmount + loanCosts,
                    value,
                    new[]
                    {
                        new ContributionRate(40, 0.00325m),
                        new ContributionRate(60, 0.00800m),
                        new ContributionRate(80, 0.01000m)
                    });

                var loan = new Loan(
                    new DateTime(2016, 3, 20),
                    loanAmount,
                    loanCosts,
                    0.025m,
                    rate.Value,
                    97.650m,
                    30);

                Printer.PrintPaymentPlan(loan, new DateTime(2016, 3, 20));

                Assert.Equal(3142000m, loan.Principal);
                Assert.Equal(149184m, loan.YearlyPaymentWithoutContribution, 0);
                Assert.Equal((149184m * 30).RoundToNearestThousand(), (loan.PaymentPlan.Sum(year => year.Repayment) + loan.PaymentPlan.Sum(year => year.Interest)).RoundToNearestThousand(), 0);

                var expectedPaymentPlan = new[]
                {
                    new PaymentPlanYear(116396m, 60928m, 55469m, 14768m, 3086531m),
                    new PaymentPlanYear(149184.38344994001793786022844m, 76485m, 72699m, 18539m, 3013831m),
                    new PaymentPlanYear(149184.38344994001793786022844m, 74651m, 74534m, 18094m, 2939297m),
                    new PaymentPlanYear(149184.38344994001793786022844m, 72770m, 76415m, 17638m, 2862882m),
                    new PaymentPlanYear(149184.38344994001793786022844m, 70841m, 78343m, 17171m, 2784539m),
                    new PaymentPlanYear(149184.38344994001793786022844m, 68864m, 80320m, 16691m, 2704219m),
                    new PaymentPlanYear(149184.38344994001793786022844m, 66837m, 82347m, 16200m, 2621872m),
                    new PaymentPlanYear(149184.38344994001793786022844m, 64759m, 84425m, 15696m, 2537446m),
                    new PaymentPlanYear(149184.38344994001793786022844m, 62629m, 86556m, 15180m, 2450891m),
                    new PaymentPlanYear(149184.38344994001793786022844m, 60445m, 88740m, 14651m, 2362151m),
                    new PaymentPlanYear(149184.38344994001793786022844m, 58205m, 90979m, 14108m, 2271171m),
                    new PaymentPlanYear(149184.38344994001793786022844m, 55909m, 93275m, 13551m, 2177896m),
                    new PaymentPlanYear(149184.38344994001793786022844m, 53556m, 95629m, 12981m, 2082267m),
                    new PaymentPlanYear(149184.38344994001793786022844m, 51142m, 98042m, 12396m, 1984224m),
                    new PaymentPlanYear(149184.38344994001793786022844m, 48668m, 100517m, 11796m, 1883708m),
                    new PaymentPlanYear(149184.38344994001793786022844m, 46132m, 103053m, 11181m, 1780655m),
                    new PaymentPlanYear(149184.38344994001793786022844m, 43531m, 105654m, 10551m, 1675001m),
                    new PaymentPlanYear(149184.38344994001793786022844m, 40865m, 108320m, 9905m, 1566681m),
                    new PaymentPlanYear(149184.38344994001793786022844m, 38131m, 111053m, 9242m, 1455628m),
                    new PaymentPlanYear(149184.38344994001793786022844m, 35329m, 113856m, 8563m, 1341772m),
                    new PaymentPlanYear(149184.38344994001793786022844m, 32456m, 116729m, 7867m, 1225043m),
                    new PaymentPlanYear(149184.38344994001793786022844m, 29510m, 119675m, 7153m, 1105368m),
                    new PaymentPlanYear(149184.38344994001793786022844m, 26490m, 122695m, 6421m, 982674m),
                    new PaymentPlanYear(149184.38344994001793786022844m, 23394m, 125791m, 5670m, 856883m),
                    new PaymentPlanYear(149184.38344994001793786022844m, 20219m, 128965m, 4901m, 727917m),
                    new PaymentPlanYear(149184.38344994001793786022844m, 16965m, 132220m, 4112m, 595697m),
                    new PaymentPlanYear(149184.38344994001793786022844m, 13628m, 135556m, 3303m, 460141m),
                    new PaymentPlanYear(149184.38344994001793786022844m, 10207m, 138977m, 2474m, 321163m),
                    new PaymentPlanYear(149184.38344994001793786022844m, 6700m, 142484m, 1624m, 178679m),
                    new PaymentPlanYear(149184.38344994001793786022844m, 3105m, 146080m, 946m, 32599m),
                    new PaymentPlanYear(33028m, 204m, 32599m, 225m, 0)
                };
                Assert.Equal(expectedPaymentPlan.Sum(plan => plan.Contribution).RoundToNearestThousand(), loan.PaymentPlan.Sum(plan => plan.Contribution).RoundToNearestThousand() + 1000m); // Off by 1000
                Assert.Equal(expectedPaymentPlan.Sum(plan => plan.Interest).RoundToNearestThousand(), loan.PaymentPlan.Sum(plan => plan.Interest).RoundToNearestThousand());
                Assert.Equal(expectedPaymentPlan.Sum(plan => plan.Repayment).RoundToNearestThousand(), loan.PaymentPlan.Sum(plan => plan.Repayment).RoundToNearestThousand());
                Assert.Equal(expectedPaymentPlan.Sum(plan => plan.LoanLeft).RoundToNearestThousand(), loan.PaymentPlan.Sum(plan => plan.LoanLeft).RoundToNearestThousand() + 1000m); // Off by 1000
                Assert.Equal(expectedPaymentPlan.Sum(plan => plan.TotalPayment).RoundToNearestThousand(), loan.PaymentPlan.Sum(plan => plan.TotalPayment).RoundToNearestThousand());
            }
        }

        public class ContributionRateTests
        {
            [Fact]
            public void Example1()
            {
                var expectedContributionRate = 0.0060591370651034468371396832m;
                var value = 3900000m;
                var loan = 3000000m + 40560m + 27296m;

                var contributionRates = new[]
                {
                    new ContributionRate(40, 0.00325m),
                    new ContributionRate(60, 0.00800m),
                    new ContributionRate(80, 0.01000m)
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