using System;
using System.Collections.Generic;
using Website.Model.Loading;

namespace Website.Tests.Model.Loading
{
    public class LoadingDataBuilder
    {
        private List<ProductData> _products = new List<ProductData>();

        public LoadingDataBuilder WithProduct(Action<ProductDataBuilder> builderFunc)
        {
            var builder = new ProductDataBuilder();
            builderFunc(builder);
            _products.Add(builder.Build());
            return this;
        }

        public LoadingData Build()
        {
            return new LoadingData
            {
                Products = _products
            };
        }
    }
}