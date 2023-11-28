using Koolawong.InterestCalculator.Model.Inside.Simple;
using Koolawong.InterestCalculator.Model.Outside.Simple;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Koolawong.InterestCalculator.Tests.Services.PeriodPaymentPlanData
{
    class LoanInputParser
    {
        public LoanInputParser(IEnumerable<string> lines)
        {
            if (lines.Count() != 9)
                throw new ArgumentOutOfRangeException(nameof(lines), "Must be exactly nine lines.");

            var culture = new CultureInfo("da-DK");
            var styles = NumberStyles.Any;

            Payout = MortgagePayout.From(Decimal.Parse(lines.ElementAt(0).Substring(11), styles, culture));
            ExchangeRate = ExchangeRate.From(Decimal.Parse(lines.ElementAt(1).Substring(14), styles, culture) / 100m);
            YearlyInterestRate = YearlyInterestRate.From(Decimal.Parse(lines.ElementAt(2).Substring(21, 6), styles, culture) / 100m);
            YearlyContributionRate = YearlyContributionRate.From(Decimal.Parse(lines.ElementAt(3).Substring(25, 6), styles, culture) / 100m);
            Period = Period.From(Int32.Parse(lines.ElementAt(4).Substring(7), styles, culture));
            TermsPerYear = TermsPerYear.From(Int32.Parse(lines.ElementAt(5).Substring(15), styles, culture));
            HouseValue = HouseValue.From(Decimal.Parse(lines.ElementAt(6).Substring(16), styles, culture));
            TotalPayoutDecidesContributionRate = Boolean.Parse(lines.ElementAt(7).Substring(39));
            BankLoanPayout = BankPayout.From(Decimal.Parse(lines.ElementAt(8).Substring(14), styles, culture));
        }

        public MortgagePayout Payout { get; }

        public ExchangeRate ExchangeRate { get; }

        public YearlyInterestRate YearlyInterestRate { get; }

        public YearlyContributionRate YearlyContributionRate { get; }

        public Period Period { get; }

        public TermsPerYear TermsPerYear { get; }

        public HouseValue HouseValue { get; }

        public bool TotalPayoutDecidesContributionRate { get; }

        public BankPayout BankLoanPayout { get; }
    }
}
