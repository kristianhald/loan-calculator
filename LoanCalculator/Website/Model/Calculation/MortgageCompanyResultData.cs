using System.Collections.Generic;

namespace Website.Model.Calculation
{
    public class MortgageCompanyResultData
    {
        public string CompanyName { get; set; }

        public ResultOverview Overview { get; set; }

        public IEnumerable<ResultDetailed> PaymentPlan { get; set; }
    }
}