using Koolawong.InterestCalculator.Model.Inside.Complex;
using Koolawong.InterestCalculator.Model.Inside.Simple;
using Koolawong.InterestCalculator.Model.Outside.Complex;
using Koolawong.InterestCalculator.Model.Outside.Simple;
using System;
using System.Linq;

namespace Koolawong.InterestCalculator.Tests.Services.PeriodPaymentPlanData
{
    class LoanDataReader
    {
        private readonly LoanInputParser _input;
        private readonly LoanCalculationParser _calculation;

        public LoanDataReader(string data)
        {
            var lines = data.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            _input = new LoanInputParser(lines.Take(9));
            ContributionRateStairCase = ContributionRateStairCase.From(lines
                .Skip(11)
                .Take(3)
                .Select(line => ContributionRateStepParser.Parse(line)));
            _calculation = new LoanCalculationParser(lines.Skip(15).Take(5));
            PeriodPaymentPlan = PeriodPaymentPlan.From(lines
                .Skip(21)
                .Select(line => new PaymentPlanParser(line))
                .Where(d => d.Repayment != Repayment.From(0m))
                .Select(d => TermPaymentPlan.From(
                    d.Term,
                    d.Repayment,
                    d.Interest,
                    d.Contribution,
                    d.PaymentLeft))
                .ToList(),
                TermsPerYear,
                CalculationDate);
        }

        public CalculationDate CalculationDate { get { return CalculationDate.From(new DateTime(2016, 01, 01)); } }

        public MortgagePayout Payout { get { return _input.Payout; } }

        public ExchangeRate ExchangeRate { get { return _input.ExchangeRate; } }

        public YearlyInterestRate YearlyInterestRate { get { return _input.YearlyInterestRate; } }

        public YearlyContributionRate YearlyContributionRate { get { return _input.YearlyContributionRate; } }

        public Period Period { get { return _input.Period; } }

        public TermsPerYear TermsPerYear { get { return _input.TermsPerYear; } }

        public HouseValue HouseValue { get { return _input.HouseValue; } }

        public bool TotalPayoutDecidesContributionRate { get { return _input.TotalPayoutDecidesContributionRate; } }

        public ContributionRateStairCase ContributionRateStairCase { get; }

        public Principal Principal { get { return _calculation.Principal; } }

        public Terms Terms { get { return _calculation.Terms; } }

        public TermInterestRate TermInterestRate { get { return _calculation.TermInterestRate; } }

        public TermContributionRate TermContributionRate { get { return _calculation.TermContributionRate; } }

        public PeriodPaymentPlan PeriodPaymentPlan { get; }

        public BankPayout BankLoanPayout { get { return _input.BankLoanPayout; } }
    }
}