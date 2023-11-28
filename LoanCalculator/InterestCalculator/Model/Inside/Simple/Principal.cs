using System;
using System.Collections.Generic;
using Koolawong.InterestCalculator.Model.Inside.Complex;
using Koolawong.InterestCalculator.Model.Outside.Simple;

namespace Koolawong.InterestCalculator.Model.Inside.Simple
{
    public class Principal
    {
        private readonly decimal _value;

        private Principal(decimal value)
        {
            _value = value;
        }

        public static Principal From(decimal value)
        {
            return new Principal(value);
        }

        public static explicit operator decimal(Principal principal)
        {
            return principal._value;
        }

        public static PeriodicalPayment operator *(Principal principal, Ratio ratio)
        {
            return PeriodicalPayment.From(principal._value * (decimal)ratio);
        }

        public static PeriodicalPayment operator /(Principal principal, Terms terms)
        {
            return PeriodicalPayment.From(principal._value / (decimal)terms);
        }

        public override bool Equals(object obj)
        {
            return Equals((dynamic)obj);
        }

        private bool Equals(Principal obj)
        {
            return
                obj != null &&
                Math.Round(_value, 2) == Math.Round(obj._value, 2);
        }

        public override int GetHashCode()
        {
            return _value.GetHashCode();
        }

        public override string ToString()
        {
            return $"{_value.ToString("C")}";
        }

        // Calculation used: https://www.regneregler.dk/annuitetslaan
        public PeriodicalPayment Calculate(
            TermInterestRate interestRate,
            Terms terms)
        {
            if (interestRate.Equals(TermInterestRate.From(0m)))
                return this / terms;

            return this * (interestRate / (TermInterestRate.HundredPercent - ((TermInterestRate.HundredPercent + interestRate) ^ -terms))); // This should be changed to a calculate, which returns PeriodicalPaymentRatio
        }

        public PeriodPaymentPlan Calculate(
            CalculationDate calculationDate,
            Terms terms,
            TermsPerYear termsPerYear,
            TermInterestRate interestRate,
            TermContributionRate contributionRate)
        {
            var paymentPlan = new List<TermPaymentPlan>();

            var periodicalPayment = Calculate(interestRate, terms);
            var paymentLeft = PaymentLeft.From(_value);

            // Calculating first term, that might be uneven as the current date is not always at the beginning of the term
            {
                CalculationTerm startingTermInYear = calculationDate.InTerm(termsPerYear);
                var daysInTerm = startingTermInYear.DaysInTerm;
                var daysLeft = startingTermInYear.DaysLeftInTerm;
                var termRatio = daysLeft / daysInTerm;

                var fullInterest = paymentLeft * interestRate;
                var interest = fullInterest * termRatio;
                var repayment = (periodicalPayment - fullInterest) * termRatio;
                var contribution = (paymentLeft * contributionRate) * termRatio;
                paymentLeft -= repayment;

                paymentPlan.Add(TermPaymentPlan.From(
                    Term.First,
                    repayment,
                    interest,
                    contribution,
                    paymentLeft));
            }

            // Calculating the rest of the terms, except the last one
            for (var term = Term.From(2); term <= terms; term++)
            {
                var interest = paymentLeft * interestRate;
                var repayment = periodicalPayment - interest;
                var contribution = paymentLeft * contributionRate;
                paymentLeft -= repayment;

                paymentPlan.Add(TermPaymentPlan.From(
                    term,
                    repayment,
                    interest,
                    contribution,
                    paymentLeft));
            }

            // Calculating the last term as it is also uneven, unless the calculation date is the beginning of a term
            // Basically the last term is calculated as if the periodical payment is the payment left
            if (paymentLeft > PaymentLeft.From(1m))
            {
                var interest = paymentLeft * interestRate;
                var repayment = paymentLeft.LastTermPayment;
                var contribution = paymentLeft * contributionRate;
                paymentLeft -= repayment;

                paymentPlan.Add(TermPaymentPlan.From(
                    Term.From((int)terms + 1), // TODO: Must be put into the model
                    repayment,
                    interest,
                    contribution,
                    paymentLeft));
            }

            return PeriodPaymentPlan.From(paymentPlan, termsPerYear, calculationDate);
        }
    }
}