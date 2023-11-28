using System.Collections.Generic;

namespace Website.Model.Loading
{
    public class ProductData
    {
        public string Name { get; set; }

        public IEnumerable<PeriodData> Periods { get; set; }
    }
}
