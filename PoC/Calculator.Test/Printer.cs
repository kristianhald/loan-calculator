using System;
using System.Linq;

namespace Calculator.Test
{
    public static class Printer
    {
        public static void PrintPaymentPlan(Loan loan, DateTime now = default(DateTime))
        {
            if (now == default(DateTime))
                now = DateTime.Now;
            var currentDate = new DateTime(now.Year, now.AddMonths(1).Month, 1);

            Console.WriteLine("        Year       Ydelse    Afdrag    Rente     Bidrag    Restgæld");

            var lastYear = currentDate.Year;
            var yearlyYdelse = 0m;
            var yearlyRepayment = 0m;
            var yearlyInterest = 0m;
            var yearlyContribution = 0m;
            for (var month = 0; month < loan.PaymentPlan.Count(); month++)
            {
                //var calculationDate = currentDate.AddMonths(month);
                var calculationDate = currentDate.AddYears(month);

                var paymentMonth = loan.PaymentPlan.ElementAt(month);
                //if (PaymentPlanMonth.Empty.Equals(paymentMonth))
                //    break;

                if (lastYear != calculationDate.Year)
                {
                    Console.WriteLine(
                        "{0, 15}{1, 10}{2, 10}{3, 10}{4, 10}{5, 13}",
                        calculationDate.AddYears(-1).ToString("yyyy"),
                        Math.Round(yearlyYdelse, 0).ToString("##,###"),
                        Math.Round(yearlyRepayment, 0).ToString("##,###"),
                        Math.Round(yearlyInterest, 0).ToString("##,###"),
                        Math.Round(yearlyContribution, 0).ToString("##,###"),
                        Math.Round(loan.PaymentPlan.ElementAt(month - 1).LoanLeft, 0).ToString("##,###"));
                    //Console.WriteLine();

                    yearlyYdelse = 0m;
                    yearlyRepayment = 0m;
                    yearlyInterest = 0m;
                    yearlyContribution = 0m;
                    lastYear = calculationDate.Year;
                }

                //var ydelse = paymentMonth.Contribution + loan.MonthlyPaymentWithoutContribution;
                yearlyYdelse += paymentMonth.TotalPayment;

                yearlyInterest += paymentMonth.Interest;
                yearlyContribution += paymentMonth.Contribution;
                yearlyRepayment += paymentMonth.Repayment;

                //Console.WriteLine(
                //    "{0, 15}{1, 10}{2, 10}{3, 10}{4, 10}{5, 13}",
                //    calculationDate.ToString("yyyy MMMM"),
                //    Math.Round(paymentMonth.TotalPayment, 0).ToString("##,###"),
                //    Math.Round(paymentMonth.Repayment, 0).ToString("##,###"),
                //    Math.Round(paymentMonth.Interest, 0).ToString("##,###"),
                //    Math.Round(/*paymentMonth.Contribution*/0m, 0).ToString("##,###"),
                //    Math.Round(paymentMonth.LoanLeft, 0).ToString("##,###"));
            }

            Console.WriteLine(
                "{0, 15}{1, 10}{2, 10}{3, 10}{4, 10}{5, 13}",
                currentDate.AddYears(loan.PaymentPlan.Count() - 1).ToString("yyyy"),
                Math.Round(yearlyYdelse, 0).ToString("##,###"),
                Math.Round(yearlyRepayment, 0).ToString("##,###"),
                Math.Round(yearlyInterest, 0).ToString("##,###"),
                Math.Round(yearlyContribution, 0).ToString("##,###"),
                Math.Round(loan.PaymentPlan.Last().LoanLeft, 0).ToString("##,###"));
        }
    }
}