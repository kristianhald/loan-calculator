using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Koolawong.InterestCalculator.Model.Inside.Complex;
using Koolawong.InterestCalculator.Model.Inside.Simple;
using Koolawong.InterestCalculator.Model.Outside.Simple;

namespace Koolawong.InterestCalculator.Tests.Model.PeriodPaymentPlanData
{
    public static class PeriodPaymentPlanDataReader
    {
        public static object[] ReadData(string data)
        {
            var lines = data.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            var inputData = new InputData(lines.Take(5));

            var periodPaymentPlan = lines
                .Skip(6)
                .Select(line => new OutputData(line))
                .Where(d => d.Repayment != 0m)
                .Select(d => TermPaymentPlan.From(
                    Term.From(d.Term),
                    Repayment.From(d.Repayment),
                    Interest.From(d.Interest),
                    Contribution.From(d.Contribution),
                    PaymentLeft.From(d.PaymentLeft)))
                .ToList();

            return new object[]
            {
                Principal.From(inputData.Principal),
                Terms.From(inputData.Terms),
                TermsPerYear.From(inputData.TermsPerYear),
                TermInterestRate.From(inputData.TermInterestRate),
                TermContributionRate.From(inputData.TermContributionRate),
                PeriodPaymentPlan.From(periodPaymentPlan, TermsPerYear.From(inputData.TermsPerYear), CalculationDate.From(new DateTime(2016, 1, 1)))
            };
        }

        private class InputData
        {
            public InputData(IEnumerable<string> lines)
            {
                if (lines.Count() != 5)
                    throw new ArgumentOutOfRangeException(nameof(lines), "Must be exactly five lines.");

                var culture = new CultureInfo("da-DK");
                var styles = NumberStyles.Any;

                Principal = Decimal.Parse(lines.ElementAt(0).Substring(14), styles, culture);
                Terms = Int32.Parse(lines.ElementAt(1).Substring(6), styles, culture);
                TermsPerYear = Int32.Parse(lines.ElementAt(2).Substring(15), styles, culture);
                TermInterestRate = Decimal.Parse(lines.ElementAt(3).Substring(19, 10), styles, culture) / 100m;
                TermContributionRate = Decimal.Parse(lines.ElementAt(4).Substring(23, 10), styles, culture) / 100m;
            }

            public decimal Principal { get; }

            public int Terms { get; }

            public int TermsPerYear { get; }

            public decimal TermInterestRate { get; }

            public decimal TermContributionRate { get; }
        }

        private class OutputData
        {
            public OutputData(string line)
            {
                var culture = new CultureInfo("da-DK");
                var styles = NumberStyles.Any;

                var columns = line.Split(new[] { '\t' }, StringSplitOptions.RemoveEmptyEntries);
                Term = Int32.Parse(columns[0], styles, culture);
                Repayment = Decimal.Parse(columns[1].Replace("kr. ", ""), styles, culture);
                Interest = Decimal.Parse(columns[2].Replace("kr. ", ""), styles, culture);
                Contribution = Decimal.Parse(columns[3].Replace("kr. ", ""), styles, culture);
                PaymentLeft = Decimal.Parse(columns[4].Replace("kr. ", ""), styles, culture);
            }

            public int Term { get; }

            public decimal Repayment { get; }

            public decimal Interest { get; }

            public decimal Contribution { get; }

            public decimal PaymentLeft { get; }
        }
    }
}