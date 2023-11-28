using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Koolawong.InterestCalculator.Model.Inside.Simple;
using Koolawong.InterestCalculator.Model.Outside.Simple;

namespace Koolawong.InterestCalculator.Tests.Services.PeriodPaymentPlanData
{
    class LoanCalculationParser
    {
        public LoanCalculationParser(IEnumerable<string> lines)
        {
            if (lines.Count() != 5)
                throw new ArgumentOutOfRangeException(nameof(lines), "Must be exactly five lines.");

            var culture = new CultureInfo("da-DK");
            var styles = NumberStyles.Any;

            Principal = Principal.From(Decimal.Parse(lines.ElementAt(0).Substring(14), styles, culture));
            Terms = Terms.From(Int32.Parse(lines.ElementAt(1).Substring(6), styles, culture));
            TermsPerYear = TermsPerYear.From(Int32.Parse(lines.ElementAt(2).Substring(15), styles, culture));
            TermInterestRate = TermInterestRate.From(Decimal.Parse(lines.ElementAt(3).Substring(19, 6), styles, culture) / 100m);
            TermContributionRate = TermContributionRate.From(Decimal.Parse(lines.ElementAt(4).Substring(23, 6), styles, culture) / 100m);
        }

        public Principal Principal { get; }

        public Terms Terms { get; }

        public TermsPerYear TermsPerYear { get; }

        public TermInterestRate TermInterestRate { get; }

        public TermContributionRate TermContributionRate { get; }
    }
}
