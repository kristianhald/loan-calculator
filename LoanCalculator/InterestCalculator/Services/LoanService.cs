using Koolawong.InterestCalculator.Model.Inside.Complex;
using Koolawong.InterestCalculator.Model.Inside.Simple;
using Koolawong.InterestCalculator.Model.Outside.Complex;
using Koolawong.InterestCalculator.Model.Outside.Simple;
using Koolawong.InterestCalculator.Services.Data;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Koolawong.InterestCalculator.Services
{
    public class LoanService
    {
        public LoanInformation CalculatePaymentPlan(
            CalculationDate calculationDate,
            MortgagePayout payout,
            TermsPerYear termsPerYear,
            Period period,
            YearlyInterestRate yearlyInterestRate,
            ExchangeRate exchangeRate,
            ContributionRateStairCase contributionRateStairCase,
            HouseValue houseValue)
        {
            return CalculatePaymentPlan(
                calculationDate,
                houseValue,
                true,
                new[]
                {
                    new Loan(
                        payout,
                        termsPerYear,
                        period,
                        yearlyInterestRate,
                        exchangeRate,
                        contributionRateStairCase)
                });
        }

        public LoanInformation CalculatePaymentPlan(
            CalculationDate calculationDate,
            HouseValue houseValue,
            bool useTotalPayoutForContributionCalculation,
            IEnumerable<Loan> loans)
        {
            var totalPayout = MortgagePayout.From(loans.Sum(l => (decimal)l.Payout));

            var upperMortgageLimitPercentage = UpperMortgageLimitPercentage.From(0.8m); // TODO: Needs to be provided by the type
            var upperMortgageLimit = upperMortgageLimitPercentage * houseValue;
            var nonMortgagePayout = upperMortgageLimit - totalPayout;
            var totalPayoutWithBankLoanTakenOut = MortgagePayout.From((decimal)totalPayout - (decimal)nonMortgagePayout); // TODO: Needs to be refactored

            var priorPayout = MortgagePayout.From(0m);
            var loanInformations = new List<LoanInformation>();
            foreach (var loan in loans)
            {
                var payoutWithBankLoanTakenOut = MortgagePayout.From((decimal)loan.Payout - (((decimal)loan.Payout / (decimal)totalPayout) * (decimal)nonMortgagePayout)); // TODO: Needs to be refactored. This is a quicky
                var principal = payoutWithBankLoanTakenOut / loan.ExchangeRate;
                var yearlyContributionRate = useTotalPayoutForContributionCalculation
                    ? loan.ContributionRateStairCase.Calculate(
                        totalPayoutWithBankLoanTakenOut,
                        houseValue)
                    : loan.ContributionRateStairCase.Calculate(
                        payoutWithBankLoanTakenOut,
                        houseValue,
                        priorPayout);

                priorPayout = MortgagePayout.From((decimal)priorPayout + (decimal)payoutWithBankLoanTakenOut);

                loanInformations.Add(new LoanInformation(
                    principal,
                    principal.Calculate(
                        calculationDate,
                        loan.Period * loan.TermsPerYear,
                        loan.TermsPerYear,
                        loan.YearlyInterestRate / loan.TermsPerYear,
                        yearlyContributionRate / loan.TermsPerYear),
                    BankPayout.From(0m)));
            }

            var totalPrincipal = Principal.From(loanInformations.Sum(l => (decimal)l.Principal));

            var totalPaymentPlan = new List<TermPaymentPlan>();
            for (var termIndex = 0; termIndex < loanInformations.Max(l => l.PaymentPlan.PlanByTerms.Count()); termIndex++)
            {
                Term term = null;
                var totalRepayment = Repayment.From(0m);
                var totalInterest = Interest.From(0m);
                var totalContribution = Contribution.From(0m);
                var totalPaymentLeft = 0m;

                foreach (var information in loanInformations)
                {
                    var planByTerms = information.PaymentPlan.PlanByTerms;
                    if (planByTerms.Count() <= termIndex)
                        continue;

                    var termPaymentPlan = planByTerms.ElementAt(termIndex);
                    term = termPaymentPlan.Term;
                    totalRepayment += termPaymentPlan.Repayment;
                    totalInterest += termPaymentPlan.Interest;
                    totalContribution += termPaymentPlan.Contribution;
                    totalPaymentLeft += (decimal)termPaymentPlan.PaymentLeft;
                }

                if (term == null)
                    break;

                totalPaymentPlan.Add(
                    TermPaymentPlan.From(
                        term,
                        totalRepayment,
                        totalInterest,
                        totalContribution,
                        PaymentLeft.From(totalPaymentLeft)));
            }

            var termsPerYearInFirstLoan = loans.First().TermsPerYear;
            Debug.Assert(loans.All(l => l.TermsPerYear.Equals(termsPerYearInFirstLoan)), "LoanService does not support multiple loans having different terms per year.");

            return new LoanInformation(
                totalPrincipal,
                PeriodPaymentPlan.From(
                    totalPaymentPlan,
                    termsPerYearInFirstLoan,
                    calculationDate),
                nonMortgagePayout);
        }

        public PayoutDistribution CalculatePayoutDistribution(
            HouseValue houseValue,
            Savings savings,
            UpperMortgageLimitPercentage upperMortgageLimitPercentage,
            UpperBankLoanLimitPercentage upperBankLoanLimitPercentage)
        {
            var upperMortgageLimit = upperMortgageLimitPercentage * houseValue;
            var upperBankLoanLimit = upperBankLoanLimitPercentage * houseValue;

            var totalPayout = houseValue - savings;

            var ownPayment = totalPayout - upperBankLoanLimit;
            var bankAndMortgagePayout = totalPayout - ownPayment;
            var bankLoanPayout = bankAndMortgagePayout - upperMortgageLimit;
            var mortgagePayout = bankAndMortgagePayout - bankLoanPayout;

            return new PayoutDistribution(
                mortgagePayout,
                bankLoanPayout,
                ownPayment);
        }
    }
}
