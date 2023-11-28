using System.Collections.Generic;

namespace Website.Model.Loading
{
    public class PeriodData
    {
        public int Period { get; set; }

        public IEnumerable<InterestRateData> InterestRate { get; set; }
    }
}
