using System;
using System.Globalization;
using Koolawong.InterestCalculator.Model.Outside.Complex;
using Koolawong.InterestCalculator.Model.Outside.Simple;

namespace Koolawong.InterestCalculator.Tests.Services.PeriodPaymentPlanData
{
    static class ContributionRateStepParser
    {
        internal static ContributionRateStep Parse(string line)
        {
            var culture = new CultureInfo("da-DK");
            var styles = NumberStyles.Any;

            var columns = line.Split(new[] { '%', '\t' }, StringSplitOptions.RemoveEmptyEntries);

            return ContributionRateStep.From(
                LoanToValue.From(Decimal.Parse(columns[0], styles, culture) / 100m),
                YearlyContributionRate.From(Decimal.Parse(columns[1], styles, culture) / 100m));
        }
    }
}
