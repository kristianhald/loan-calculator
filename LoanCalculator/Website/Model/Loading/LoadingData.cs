using System.Collections.Generic;

namespace Website.Model.Loading
{
    public class LoadingData
    {
        public IEnumerable<ProductData> Products { get; set; }

        public IEnumerable<DefaultSetting> DefaultSettings { get; set; }
    }
}