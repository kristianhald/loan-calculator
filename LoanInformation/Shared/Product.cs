using System;

namespace Shared
{
    public class Product
    {
        public Product(
            ProductType productType,
            int period,
            decimal interestRate,
            decimal exchangeRate)
        {
            ProductType = productType;
            Period = period;
            InterestRate = interestRate;
            ExchangeRate = exchangeRate;
        }

        public ProductType ProductType { get; }

        public int Period { get; }

        public decimal InterestRate { get; }

        public decimal ExchangeRate { get; }

        public override bool Equals(object obj)
        {
            var castedObj = obj as Product;
            return Equals(castedObj);
        }

        private bool Equals(Product obj)
        {
            return
                obj != null &&
                ProductType == obj.ProductType &&
                Period == obj.Period &&
                InterestRate == obj.InterestRate &&
                ExchangeRate == obj.ExchangeRate;
        }

        public override int GetHashCode()
        {
            return InterestRate.GetHashCode();
        }

        public override string ToString()
        {
            return string.Format(
                "{0} - {1} - {2} - {3}",
                Enum.GetName(typeof(ProductType), ProductType),
                Period,
                InterestRate,
                ExchangeRate);
        }
    }
}
