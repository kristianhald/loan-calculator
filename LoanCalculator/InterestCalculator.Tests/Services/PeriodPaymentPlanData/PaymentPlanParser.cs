using System;
using System.Globalization;
using Koolawong.InterestCalculator.Model.Inside.Complex;
using Koolawong.InterestCalculator.Model.Inside.Simple;

namespace Koolawong.InterestCalculator.Tests.Services.PeriodPaymentPlanData
{
    class PaymentPlanParser
    {
        public PaymentPlanParser(string line)
        {
            var culture = new CultureInfo("da-DK");
            var styles = NumberStyles.Any;

            var columns = line.Split(new[] { '\t' }, StringSplitOptions.RemoveEmptyEntries);
            Term = Term.From(Int32.Parse(columns[0], styles, culture));
            Repayment = Repayment.From(Decimal.Parse(columns[1].Replace("kr. ", ""), styles, culture));
            Interest = Interest.From(Decimal.Parse(columns[2].Replace("kr. ", ""), styles, culture));
            Contribution = Contribution.From(Decimal.Parse(columns[3].Replace("kr. ", ""), styles, culture));
            PaymentLeft = PaymentLeft.From(Decimal.Parse(columns[4].Replace("kr. ", ""), styles, culture));
        }

        public Term Term { get; }

        public Repayment Repayment { get; }

        public Interest Interest { get; }

        public Contribution Contribution { get; }

        public PaymentLeft PaymentLeft { get; }
    }
}
