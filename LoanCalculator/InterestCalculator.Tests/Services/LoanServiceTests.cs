using Koolawong.InterestCalculator.Model.Inside.Complex;
using Koolawong.InterestCalculator.Model.Inside.Simple;
using Koolawong.InterestCalculator.Model.Outside.Simple;
using Koolawong.InterestCalculator.Services;
using Koolawong.InterestCalculator.Services.Data;
using Koolawong.InterestCalculator.Tests.Services.PeriodPaymentPlanData;
using System.Linq;
using Xunit;

namespace Koolawong.InterestCalculator.Tests.Services
{
    public class LoanServiceTests
    {
        public class CalculatePaymentPlan
        {
            private readonly LoanService _loanService = new LoanService();

            [Fact]
            public void GivenSingleLoan_ThenPaymentPlanIsJustReturned()
            {
                var dataReader = new LoanDataReader(PeriodPaymentPlanDataFiles.LoanWithOnlyOneProduct);

                var loanInformation = _loanService.CalculatePaymentPlan(
                    dataReader.CalculationDate,
                    dataReader.Payout,
                    dataReader.TermsPerYear,
                    dataReader.Period,
                    dataReader.YearlyInterestRate,
                    dataReader.ExchangeRate,
                    dataReader.ContributionRateStairCase,
                    dataReader.HouseValue);

                Assert.Equal(dataReader.Principal, loanInformation.Principal);
                Assert.Equal(dataReader.PeriodPaymentPlan, loanInformation.PaymentPlan);
                Assert.Equal(dataReader.BankLoanPayout, loanInformation.BankLoanPayout);
            }

            [Fact]
            public void GivenLoanWithNoInterestRate_ThenPaymentPlanIsJustReturned()
            {
                var dataReader = new LoanDataReader(PeriodPaymentPlanDataFiles.LoanWithZeroInterestRate);

                var loanInformation = _loanService.CalculatePaymentPlan(
                    dataReader.CalculationDate,
                    dataReader.Payout,
                    dataReader.TermsPerYear,
                    dataReader.Period,
                    dataReader.YearlyInterestRate,
                    dataReader.ExchangeRate,
                    dataReader.ContributionRateStairCase,
                    dataReader.HouseValue);

                Assert.Equal(dataReader.Principal, loanInformation.Principal);
                Assert.Equal(dataReader.PeriodPaymentPlan, loanInformation.PaymentPlan);
                Assert.Equal(dataReader.BankLoanPayout, loanInformation.BankLoanPayout);
            }

            [Fact]
            public void GivenTwoLoans_WhenTotalPayoutIsUsedToFindContributionRate_ThenPaymentPlanIsJustReturned()
            {
                var firstPriority =
                    new LoanDataReader(
                        PeriodPaymentPlanDataFiles
                            .LoanWithTwoProducts_EntirePayoutDeterminesContributionRate_FirstPriority);
                var secondPriority =
                    new LoanDataReader(
                        PeriodPaymentPlanDataFiles
                            .LoanWithTwoProducts_EntirePayoutDeterminesContributionRate_SecondPriority);

                var loanInformation = _loanService.CalculatePaymentPlan(
                    firstPriority.CalculationDate,
                    firstPriority.HouseValue,
                    firstPriority.TotalPayoutDecidesContributionRate,
                    new[]
                    {
                        new Loan(
                            firstPriority.Payout,
                            firstPriority.TermsPerYear,
                            firstPriority.Period,
                            firstPriority.YearlyInterestRate,
                            firstPriority.ExchangeRate,
                            firstPriority.ContributionRateStairCase),
                        new Loan(
                            secondPriority.Payout,
                            secondPriority.TermsPerYear,
                            secondPriority.Period,
                            secondPriority.YearlyInterestRate,
                            secondPriority.ExchangeRate,
                            secondPriority.ContributionRateStairCase),
                    });

                Assert.Equal(
                    Principal.From((decimal)firstPriority.Principal + (decimal)secondPriority.Principal),
                    loanInformation.Principal);

                var expectedPaymentPlanByTerms = firstPriority.PeriodPaymentPlan.PlanByTerms.Join(
                    secondPriority.PeriodPaymentPlan.PlanByTerms,
                    first => first.Term,
                    second => second.Term,
                    (first, second) => TermPaymentPlan.From(
                        first.Term,
                        first.Repayment + second.Repayment,
                        first.Interest + second.Interest,
                        first.Contribution + second.Contribution,
                        PaymentLeft.From((decimal)first.PaymentLeft + (decimal)second.PaymentLeft)));

                var expectedPaymentPlan = PeriodPaymentPlan.From(
                    expectedPaymentPlanByTerms,
                    firstPriority.TermsPerYear,
                    firstPriority.CalculationDate);
                Assert.Equal(expectedPaymentPlan, loanInformation.PaymentPlan);
            }

            [Fact]
            public void GivenTwoLoans_WhenPriorityDefinesContributionRate_ThenPaymentPlanIsJustReturned()
            {
                var firstPriority =
                    new LoanDataReader(
                        PeriodPaymentPlanDataFiles.LoanWithTwoProducts_PriorityDeterminesContributionRate_FirstPriority);
                var secondPriority =
                    new LoanDataReader(
                        PeriodPaymentPlanDataFiles.LoanWithTwoProducts_PriorityDeterminesContributionRate_SecondPriority);

                var loanInformation = _loanService.CalculatePaymentPlan(
                    firstPriority.CalculationDate,
                    firstPriority.HouseValue,
                    firstPriority.TotalPayoutDecidesContributionRate,
                    new[]
                    {
                        new Loan(
                            firstPriority.Payout,
                            firstPriority.TermsPerYear,
                            firstPriority.Period,
                            firstPriority.YearlyInterestRate,
                            firstPriority.ExchangeRate,
                            firstPriority.ContributionRateStairCase),
                        new Loan(
                            secondPriority.Payout,
                            secondPriority.TermsPerYear,
                            secondPriority.Period,
                            secondPriority.YearlyInterestRate,
                            secondPriority.ExchangeRate,
                            secondPriority.ContributionRateStairCase),
                    });

                Assert.Equal(
                    Principal.From((decimal)firstPriority.Principal + (decimal)secondPriority.Principal),
                    loanInformation.Principal);

                var expectedPaymentPlanByTerms = firstPriority.PeriodPaymentPlan.PlanByTerms.Join(
                    secondPriority.PeriodPaymentPlan.PlanByTerms,
                    first => first.Term,
                    second => second.Term,
                    (first, second) => TermPaymentPlan.From(
                        first.Term,
                        first.Repayment + second.Repayment,
                        first.Interest + second.Interest,
                        first.Contribution + second.Contribution,
                        PaymentLeft.From((decimal)first.PaymentLeft + (decimal)second.PaymentLeft)));

                var expectedPaymentPlan = PeriodPaymentPlan.From(
                    expectedPaymentPlanByTerms,
                    firstPriority.TermsPerYear,
                    firstPriority.CalculationDate);
                Assert.Equal(expectedPaymentPlan, loanInformation.PaymentPlan);
                Assert.Equal(loanInformation.BankLoanPayout, loanInformation.BankLoanPayout);
            }

            [Fact]
            public void GivenSingleLoanWithMoreThanEightyPercent_ThenBankLoanPayoutIsSet()
            {
                var dataReader = new LoanDataReader(PeriodPaymentPlanDataFiles.LoanWithBankLoan);

                var loanInformation = _loanService.CalculatePaymentPlan(
                    dataReader.CalculationDate,
                    dataReader.Payout,
                    dataReader.TermsPerYear,
                    dataReader.Period,
                    dataReader.YearlyInterestRate,
                    dataReader.ExchangeRate,
                    dataReader.ContributionRateStairCase,
                    dataReader.HouseValue);

                Assert.Equal(dataReader.Principal, loanInformation.Principal);
                Assert.Equal(dataReader.PeriodPaymentPlan, loanInformation.PaymentPlan);
                Assert.Equal(dataReader.BankLoanPayout, loanInformation.BankLoanPayout);
            }

            [Fact]
            public void GivenSingleloanWithLessThanEightyPercent_ThenBankLoanIsZero()
            {
                var dataReader =
                    new LoanDataReader(PeriodPaymentPlanDataFiles.LoanWithLessThanUpperLimitPercentagePayout);

                var loanInformation = _loanService.CalculatePaymentPlan(
                    dataReader.CalculationDate,
                    dataReader.Payout,
                    dataReader.TermsPerYear,
                    dataReader.Period,
                    dataReader.YearlyInterestRate,
                    dataReader.ExchangeRate,
                    dataReader.ContributionRateStairCase,
                    dataReader.HouseValue);

                Assert.Equal(dataReader.Principal, loanInformation.Principal);
                Assert.Equal(dataReader.PeriodPaymentPlan, loanInformation.PaymentPlan);
                Assert.Equal(dataReader.BankLoanPayout, loanInformation.BankLoanPayout);
            }
        }

        public class CalculatePayoutDistribution
        {
            private readonly LoanService _loanService = new LoanService();
            private readonly UpperMortgageLimitPercentage _upperMortgageLimitPercentage = UpperMortgageLimitPercentage.From(0.8m);
            private readonly UpperBankLoanLimitPercentage _upperBankLoanLimitPercentage = UpperBankLoanLimitPercentage.From(0.95m);

            [Fact]
            public void GivenPayoutIsEightyPercentOfHouseValue_ThenAllPayoutIsInTheMortgage()
            {
                var expected = new PayoutDistribution(
                    MortgagePayout.From(3200000m),
                    BankPayout.From(0m),
                    OwnPayment.From(0m));

                var houseValue = HouseValue.From(3200000m / 0.8m);
                var savings = Savings.From(800000m);

                var actual = _loanService.CalculatePayoutDistribution(
                    houseValue,
                    savings,
                    _upperMortgageLimitPercentage,
                    _upperBankLoanLimitPercentage);

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void GivenPayoutIsLessThanEightyPercentOfHouseValue_ThenAllPayoutIsInTheMortgage()
            {
                var expected = new PayoutDistribution(
                    MortgagePayout.From(2200000m),
                    BankPayout.From(0m),
                    OwnPayment.From(0m));

                var houseValue = HouseValue.From(3200000m / 0.8m);
                var savings = Savings.From(1800000m);

                var actual = _loanService.CalculatePayoutDistribution(
                    houseValue,
                    savings,
                    _upperMortgageLimitPercentage,
                    _upperBankLoanLimitPercentage);

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void GivenPayoutIsMoreThanEightyPercentOfHouseValue_WhenPayoutIsLessThanNinetyFivePercentOfHouseValue_ThenEverythingAboveEightyIsPlacedInBankLoan()
            {
                var expected = new PayoutDistribution(
                    MortgagePayout.From(3200000m),
                    BankPayout.From(400000m),
                    OwnPayment.From(0m));

                var houseValue = HouseValue.From(3200000m / 0.8m);
                var savings = Savings.From(400000m);

                var actual = _loanService.CalculatePayoutDistribution(
                    houseValue,
                    savings,
                    _upperMortgageLimitPercentage,
                    _upperBankLoanLimitPercentage);

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void GivenPayoutIsNinetyFivePercentOfHouseValue_ThenNothingIsPlacedInOwnPayment()
            {
                var expected = new PayoutDistribution(
                    MortgagePayout.From(3200000m),
                    BankPayout.From(600000m),
                    OwnPayment.From(0m));

                var houseValue = HouseValue.From(3200000m / 0.8m);
                var savings = Savings.From(200000m);

                var actual = _loanService.CalculatePayoutDistribution(
                    houseValue,
                    savings,
                    _upperMortgageLimitPercentage,
                    _upperBankLoanLimitPercentage);

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void GivenPayoutIsMoreThanNinetyFivePercentOfHouseValue_ThenEverythingAboveNinetyFiveIsPlacedInOwnPayment()
            {
                var expected = new PayoutDistribution(
                    MortgagePayout.From(3200000m),
                    BankPayout.From(600000m),
                    OwnPayment.From(100000m));

                var houseValue = HouseValue.From(3200000m / 0.8m);
                var savings = Savings.From(100000m);

                var actual = _loanService.CalculatePayoutDistribution(
                    houseValue,
                    savings,
                    _upperMortgageLimitPercentage,
                    _upperBankLoanLimitPercentage);

                Assert.Equal(expected, actual);
            }
        }
    }
}
