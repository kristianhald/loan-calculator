using System;

namespace Koolawong.InterestCalculator.Model.Inside.Simple
{
    public class PaymentLeft
    {
        private readonly decimal _value;

        public Repayment LastTermPayment => Repayment.From(_value);

        private PaymentLeft(decimal value)
        {
            _value = value;
        }

        public static PaymentLeft From(decimal value)
        {
            return new PaymentLeft(value);
        }

        public static explicit operator decimal(PaymentLeft paymentLeft)
        {
            return paymentLeft._value;
        }

        public static bool operator <(PaymentLeft a, PaymentLeft b)
        {
            return a._value < b._value;
        }

        public static bool operator <=(PaymentLeft a, PaymentLeft b)
        {
            return a._value <= b._value;
        }

        public static bool operator >(PaymentLeft a, PaymentLeft b)
        {
            return !(a <= b);
        }

        public static bool operator >=(PaymentLeft a, PaymentLeft b)
        {
            return !(a < b);
        }

        public static Interest operator *(PaymentLeft paymentLeft, TermInterestRate interestRate)
        {
            return Interest.From(paymentLeft._value * (decimal)interestRate);
        }

        public static Contribution operator *(PaymentLeft paymentLeft, TermContributionRate contributionRate)
        {
            return Contribution.From(paymentLeft._value * (decimal)contributionRate);
        }

        public static PaymentLeft operator -(PaymentLeft paymentLeft, Repayment repayment)
        {
            return From(paymentLeft._value - (decimal)repayment);
        }

        public override bool Equals(object obj)
        {
            return Equals((dynamic)obj);
        }

        private bool Equals(PaymentLeft obj)
        {
            return
                obj != null &&
                Math.Abs(_value - obj._value) <= 0.05m;
        }

        public override int GetHashCode()
        {
            return _value.GetHashCode();
        }

        public override string ToString()
        {
            return $"{_value.ToString("C")}";
        }
    }
}
