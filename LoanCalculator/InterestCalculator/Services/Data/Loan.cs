using System;
using Koolawong.InterestCalculator.Model.Inside.Simple;
using Koolawong.InterestCalculator.Model.Outside.Complex;
using Koolawong.InterestCalculator.Model.Outside.Simple;

namespace Koolawong.InterestCalculator.Services.Data
{
    public class Loan
    {
        public Loan(
            MortgagePayout payout,
            TermsPerYear termsPerYear,
            Period period,
            YearlyInterestRate yearlyInterestRate,
            ExchangeRate exchangeRate,
            ContributionRateStairCase contributionRateStairCase)
        {
            if (payout == null)
                throw new ArgumentNullException(nameof(payout));
            if (termsPerYear == null)
                throw new ArgumentNullException(nameof(termsPerYear));
            if (period == null)
                throw new ArgumentNullException(nameof(period));
            if (yearlyInterestRate == null)
                throw new ArgumentNullException(nameof(yearlyInterestRate));
            if (exchangeRate == null)
                throw new ArgumentNullException(nameof(exchangeRate));
            if (contributionRateStairCase == null)
                throw new ArgumentNullException(nameof(contributionRateStairCase));

            Payout = payout;
            TermsPerYear = termsPerYear;
            Period = period;
            YearlyInterestRate = yearlyInterestRate;
            ExchangeRate = exchangeRate;
            ContributionRateStairCase = contributionRateStairCase;
        }

        public ContributionRateStairCase ContributionRateStairCase { get; }

        public ExchangeRate ExchangeRate { get; }

        public MortgagePayout Payout { get; }

        public Period Period { get; }

        public TermsPerYear TermsPerYear { get; }

        public YearlyInterestRate YearlyInterestRate { get; }
    }
}
