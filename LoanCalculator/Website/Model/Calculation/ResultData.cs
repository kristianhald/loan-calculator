using System.Collections.Generic;

namespace Website.Model.Calculation
{
    public class ResultData
    {
        public IEnumerable<MortgageCompanyResultData> Results { get; set; }

        public BankResultData BankResult { get; set; }

        public OwnPaymentResultData OwnPaymentResult { get; set; }
    }
}