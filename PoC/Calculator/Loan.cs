using System;
using System.Collections.Generic;

namespace Calculator
{
    public class Loan
    {
        public Loan(
            DateTime calculationDate,
            decimal payout,
            decimal costs,
            decimal interestRate,
            decimal contributionRate,
            decimal exchangeRate,
            int years)
        {
            Principal = ((payout + costs) / exchangeRate * 100).RoundToNearestThousand();

            var termsPerYear = 4m;
            var quarterlyInterestRate = interestRate / termsPerYear;
            var quarterlyContributionRate = contributionRate / termsPerYear;
            YearlyPaymentWithoutContribution = Principal * (quarterlyInterestRate / (1 - (decimal)Math.Pow(1d + (double)quarterlyInterestRate, (double)(-1 * years * termsPerYear)))) * termsPerYear;

            var loanLeft = Principal;
            var paymentPlan = new List<PaymentPlanYear>();

            // Calculating first year, that might be uneven as the current date is not always 1st of January
            var interest = 0m;
            var repayment = 0m;
            var contribution = 0m;
            var yearlyPaymentWithoutContribution = 0m;
            for (var term = (int)Math.Floor(calculationDate.Month / termsPerYear); term < termsPerYear; term++)
            {
                var avg = 1m;
                var monthsInTerm = (int)(12 / termsPerYear);
                if (interest == 0m) // Need the numbers of days already into the term as to make the average calculation
                {
                    var startOfCurrentTerm = new DateTime(calculationDate.Year, 1, 1).AddMonths(term * monthsInTerm);
                    var startOfNextTerm = startOfCurrentTerm.AddMonths(monthsInTerm);
                    var daysInTerm = (decimal)(startOfNextTerm - startOfCurrentTerm).TotalDays;
                    var daysLeftInTerm = (decimal)(startOfNextTerm - calculationDate).TotalDays - 1;

                    avg = daysLeftInTerm / daysInTerm;
                }

                var termInterest = (loanLeft * (interestRate / termsPerYear)) * avg;
                var termRepayment = ((YearlyPaymentWithoutContribution / termsPerYear) * avg) - termInterest;
                var termContribution = loanLeft * quarterlyContributionRate * avg;

                interest += termInterest;
                repayment += termRepayment;
                contribution += termContribution;
                loanLeft -= termRepayment;
                yearlyPaymentWithoutContribution += (YearlyPaymentWithoutContribution / termsPerYear) * avg;
            }
            paymentPlan.Add(new PaymentPlanYear(
                yearlyPaymentWithoutContribution,
                interest,
                repayment,
                contribution,
                loanLeft));

            // Calculating the rest of the years, except the last one
            for (var year = 1; year < years; year++)
            {
                interest = 0m;
                repayment = 0m;
                contribution = 0m;
                for (var term = 0; term < termsPerYear; term++)
                {
                    var termInterest = loanLeft * (interestRate / termsPerYear);
                    var termRepayment = (YearlyPaymentWithoutContribution / termsPerYear) - termInterest;
                    var termContribution = loanLeft * quarterlyContributionRate;

                    interest += termInterest;
                    repayment += termRepayment;
                    contribution += termContribution;
                    loanLeft -= termRepayment;
                }

                paymentPlan.Add(new PaymentPlanYear(
                    YearlyPaymentWithoutContribution,
                    interest,
                    repayment,
                    contribution,
                    loanLeft));
            }

            // Calculating the last year as it is also uneven, unless the calculation date is 1st of January
            interest = 0m;
            repayment = 0m;
            contribution = 0m;
            yearlyPaymentWithoutContribution = 0m;
            for (var term = 0; term < (int)Math.Floor(calculationDate.Month / termsPerYear) + 1; term++)
            {
                var avg = 1m;
                var monthsInTerm = (int)(12 / termsPerYear);
                if (term == (int)Math.Floor(calculationDate.Month / termsPerYear)) // Need the numbers of days already into the term as to make the average calculation
                {
                    var startOfCurrentTerm = new DateTime(calculationDate.Year, 1, 1).AddMonths(term * monthsInTerm);
                    var startOfNextTerm = startOfCurrentTerm.AddMonths(monthsInTerm);
                    var daysInTerm = (decimal)(startOfNextTerm - startOfCurrentTerm).TotalDays;
                    var daysIntoTerm = (decimal)(calculationDate - startOfCurrentTerm).TotalDays + 1;

                    avg = daysIntoTerm / daysInTerm;
                }

                var termInterest = (loanLeft * (interestRate / termsPerYear)) * avg;
                var termRepayment = ((YearlyPaymentWithoutContribution / termsPerYear) - termInterest) * avg;
                var termContribution = loanLeft * quarterlyContributionRate * avg;

                interest += termInterest;
                repayment += termRepayment;
                contribution += termContribution;
                loanLeft -= termRepayment;
                yearlyPaymentWithoutContribution += (YearlyPaymentWithoutContribution / termsPerYear) * avg;
            }
            paymentPlan.Add(new PaymentPlanYear(
                yearlyPaymentWithoutContribution,
                interest,
                repayment,
                contribution,
                0m));

            PaymentPlan = paymentPlan;
        }

        public decimal Principal { get; }

        public decimal YearlyPaymentWithoutContribution { get; }

        public IEnumerable<PaymentPlanYear> PaymentPlan { get; }
    }
}