using Koolawong.InterestCalculator.Model.Outside.Complex;
using Koolawong.InterestCalculator.Model.Outside.Simple;
using System.Collections.Generic;
using Koolawong.InterestCalculator.Model.Inside.Simple;
using Xunit;

namespace Koolawong.InterestCalculator.Tests.Model
{
    public sealed class ContributionRateStairCaseTests
    {
        public sealed class CalculateRateByTotalOnly
        {
            private readonly ContributionRateStairCase _stairCase = ContributionRateStairCase.From(new[]
            {
                ContributionRateStep.From(LoanToValue.From(0.40m), YearlyContributionRate.From(0.0050m)),
                ContributionRateStep.From(LoanToValue.From(0.60m), YearlyContributionRate.From(0.0100m)),
                ContributionRateStep.From(LoanToValue.From(0.80m), YearlyContributionRate.From(0.0120m)),
                ContributionRateStep.From(LoanToValue.From(1.00m), YearlyContributionRate.From(0.0080m)),
            });

            [Fact]
            public void AtEighty()
            {
                var payout = MortgagePayout.From(3200000m);
                var houseValue = HouseValue.From(4000000m);

                Assert.Equal(
                    YearlyContributionRate.From(0.008000m),
                    _stairCase.Calculate(payout, houseValue));
            }

            [Fact]
            public void AboveEighty()
            {
                var payout = MortgagePayout.From(3500000m);
                var houseValue = HouseValue.From(4000000m);

                Assert.Equal(
                    YearlyContributionRate.From(0.008000m),
                    _stairCase.Calculate(payout, houseValue));
            }

            [Fact]
            public void AtFifty()
            {
                var payout = MortgagePayout.From(2000000m);
                var houseValue = HouseValue.From(4000000m);

                Assert.Equal(
                    YearlyContributionRate.From(0.006000m),
                    _stairCase.Calculate(payout, houseValue));
            }

            [Fact]
            public void AtThirty()
            {
                var payout = MortgagePayout.From(1200000m);
                var houseValue = HouseValue.From(4000000m);

                Assert.Equal(
                    YearlyContributionRate.From(0.005000m),
                    _stairCase.Calculate(payout, houseValue));
            }
        }

        public sealed class CalculateRateUsingFirstPriorityLoans
        {
            private readonly ContributionRateStairCase _stairCase = ContributionRateStairCase.From(new[]
            {
                ContributionRateStep.From(LoanToValue.From(0.40m), YearlyContributionRate.From(0.0050m)),
                ContributionRateStep.From(LoanToValue.From(0.60m), YearlyContributionRate.From(0.0100m)),
                ContributionRateStep.From(LoanToValue.From(0.80m), YearlyContributionRate.From(0.0120m)),
                ContributionRateStep.From(LoanToValue.From(1.00m), YearlyContributionRate.From(0.0080m)),
            });

            public static IEnumerable<object[]> PayoutToContributionRate => new[]
            {
                new object[] { MortgagePayout.From(3200000m), HouseValue.From(4000000m), PriorityLoan.From(0m), YearlyContributionRate.From(0.008000m) },
                new object[] { MortgagePayout.From(3500000m), HouseValue.From(4000000m), PriorityLoan.From(0m), YearlyContributionRate.From(0.008000m) },
                new object[] { MortgagePayout.From(2000000m), HouseValue.From(4000000m), PriorityLoan.From(0m), YearlyContributionRate.From(0.006000m) },
                new object[] { MortgagePayout.From(1200000m), HouseValue.From(4000000m), PriorityLoan.From(0m), YearlyContributionRate.From(0.005000m) },
                new object[] { MortgagePayout.From(800000m), HouseValue.From(4000000m), PriorityLoan.From(1600000m), YearlyContributionRate.From(0.010000m) },
                new object[] { MortgagePayout.From(800000m), HouseValue.From(4000000m), PriorityLoan.From(2400000m), YearlyContributionRate.From(0.012000m) },
                new object[] { MortgagePayout.From(1600000m), HouseValue.From(4000000m), PriorityLoan.From(1600000m), YearlyContributionRate.From(0.011000m) },
                new object[] { MortgagePayout.From(1200000m), HouseValue.From(4000000m), PriorityLoan.From(1600000m), YearlyContributionRate.From(0.010666666666666666666666666700m) },
                new object[] { MortgagePayout.From(2200000m), HouseValue.From(4000000m), PriorityLoan.From(600000m), YearlyContributionRate.From(0.008090909090909090909090909100m) }
            };

            [Theory]
            [MemberData("PayoutToContributionRate")]
            public void GivenProvidedData_ThenExpectedContributionRateIsReturned(
                MortgagePayout payout,
                HouseValue houseValue,
                PriorityLoan priorityLoans,
                YearlyContributionRate expected)
            {
                var actual = _stairCase.Calculate(payout, houseValue, priorityLoans);

                Assert.Equal(
                    expected,
                    actual);
            }
        }
    }
}