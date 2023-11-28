using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DefaultDataSetupper
{
    class Program
    {
        private static readonly IEnumerable<ProductType> ProductTypes = new[]
        {
            new ProductType(1, "FixedRate", "Fast rente"),
            new ProductType(2, "FixedRateInterestOnly", "Fast rente med afdragsfrihed"),
            new ProductType(3, "Flex1", "Flex 1 år"),
            new ProductType(4, "Flex2", "Flex 2 år"),
            new ProductType(5, "Flex3", "Flex 3 år"),
            new ProductType(6, "Flex4", "Flex 4 år"),
            new ProductType(7, "Flex5", "Flex 5 år"),
            new ProductType(8, "Flex6", "Flex 6 år"),
            new ProductType(9, "Flex7", "Flex 7 år"),
            new ProductType(10, "Flex8", "Flex 8 år"),
            new ProductType(11, "Flex9", "Flex 9 år"),
            new ProductType(12, "Flex10", "Flex 10 år"),
            new ProductType(13, "FShort", "Kort rente"),
            new ProductType(14, "RateMax", "RenteMax"),
        };

        private static readonly IEnumerable<Company> Companies = new[]
        {
            new Company(1, "BRFKredit", "BRFkredit"),
            new Company(2, "Nordea Kredit", "Nordea Kredit"),
            new Company(3, "Realkredit Danmark", "Realkredit Danmark"),
            new Company(4, "TotalKredit", "Totalkredit"),
            new Company(5, "Nykredit", "Nykredit"), 
        };

        private static readonly IEnumerable<CompanyContributionRateStairCases> CompanyContributionRateStairCases = new[]
        {
            new CompanyContributionRateStairCases(
                1,
                new[]
                {
                    new ContributionRateStairCase(1, new[] {"FixedRate"}, new [] { new ContributionRateStairCaseStep(40m, 0.3250m), new ContributionRateStairCaseStep(60m, 0.4833m), new ContributionRateStairCaseStep(80m, 0.6125m), new ContributionRateStairCaseStep(100m, 0.4125m) }),
                    new ContributionRateStairCase(2, new[] {"FixedRateInterestOnly"}, new [] { new ContributionRateStairCaseStep(40m, 0.4000m), new ContributionRateStairCaseStep(60m, 0.6000m), new ContributionRateStairCaseStep(80m, 0.8875m), new ContributionRateStairCaseStep(100m, 0.6125m) }),
                    new ContributionRateStairCase(3, new[] {"Flex1", "Flex2", "Flex3", "Flex4", "Flex5", "Flex6", "Flex7", "Flex8", "Flex9", "Flex10"}, new [] { new ContributionRateStairCaseStep(40m, 0.4250m), new ContributionRateStairCaseStep(60m, 0.6333m), new ContributionRateStairCaseStep(80m, 0.8500m), new ContributionRateStairCaseStep(100m, 0.6500m) }),
                    new ContributionRateStairCase(4, new[] {"FShort"}, new [] { new ContributionRateStairCaseStep(40m, 0.3750m), new ContributionRateStairCaseStep(60m, 0.5667m), new ContributionRateStairCaseStep(80m, 0.7500m), new ContributionRateStairCaseStep(100m, 0.4525m) }),
                }),
            new CompanyContributionRateStairCases(
                2,
                new[]
                {
                    new ContributionRateStairCase(1, new[] {"FixedRate"}, new [] { new ContributionRateStairCaseStep(40m, 0.3750m), new ContributionRateStairCaseStep(60m, 0.8250m), new ContributionRateStairCaseStep(80m, 1.1250m), new ContributionRateStairCaseStep(100m, 0.6750m) }),
                    new ContributionRateStairCase(2, new[] {"FixedRateInterestOnly"}, new [] { new ContributionRateStairCaseStep(40m, 0.5250m), new ContributionRateStairCaseStep(60m, 1.1250m), new ContributionRateStairCaseStep(80m, 1.8250m), new ContributionRateStairCaseStep(100m, 1.0000m) }),
                    new ContributionRateStairCase(3, new[] {"Flex5", "FShort" }, new [] { new ContributionRateStairCaseStep(40m, 0.5000m), new ContributionRateStairCaseStep(60m, 1.0250m), new ContributionRateStairCaseStep(80m, 1.3750m), new ContributionRateStairCaseStep(100m, 0.8500m) }),
                    new ContributionRateStairCase(4, new[] {"Flex3" }, new [] { new ContributionRateStairCaseStep(40m, 0.7000m), new ContributionRateStairCaseStep(60m, 1.2250m), new ContributionRateStairCaseStep(80m, 1.5750m), new ContributionRateStairCaseStep(100m, 1.0500m) }),
                    new ContributionRateStairCase(5, new[] {"Flex1" }, new [] { new ContributionRateStairCaseStep(40m, 0.7500m), new ContributionRateStairCaseStep(60m, 1.3500m), new ContributionRateStairCaseStep(80m, 1.7500m), new ContributionRateStairCaseStep(100m, 1.1500m) }),
                }),
            new CompanyContributionRateStairCases(
                3,
                new[]
                {
                    new ContributionRateStairCase(1, new[] {"FixedRate"}, new[] { new ContributionRateStairCaseStep(40m, 0.2748m), new ContributionRateStairCaseStep(60m, 0.8248m), new ContributionRateStairCaseStep(80m, 1.3500m), new ContributionRateStairCaseStep(100m, 0.6812m) }),
                    new ContributionRateStairCase(2, new[] {"FixedRateInterestOnly"}, new[] { new ContributionRateStairCaseStep(40m, 0.3748m), new ContributionRateStairCaseStep(60m, 0.9248m), new ContributionRateStairCaseStep(80m, 1.6752m), new ContributionRateStairCaseStep(100m, 0.7812m) }),
                    new ContributionRateStairCase(3, new[] {"Flex5", "Flex6", "Flex7", "Flex8", "Flex9", "Flex10", "FShort"}, new[] { new ContributionRateStairCaseStep(40m, 0.4500m), new ContributionRateStairCaseStep(60m, 1.0000m), new ContributionRateStairCaseStep(80m, 1.5252m ), new ContributionRateStairCaseStep(100m, 0.8564m) }),
                    new ContributionRateStairCase(4, new[] {"Flex1", "Flex2"}, new[] { new ContributionRateStairCaseStep(40m, 0.7000m), new ContributionRateStairCaseStep(60m, 1.2500m), new ContributionRateStairCaseStep(80m, 1.7752m ), new ContributionRateStairCaseStep(100m, 1.1064m) }),
                    new ContributionRateStairCase(5, new[] {"Flex3", "Flex4"}, new[] { new ContributionRateStairCaseStep(40m, 0.6500m), new ContributionRateStairCaseStep(60m, 1.2000m), new ContributionRateStairCaseStep(80m, 1.7252m ), new ContributionRateStairCaseStep(100m, 1.0564m) }),
                }),
            new CompanyContributionRateStairCases(
                4,
                new[]
                {
                    new ContributionRateStairCase(1, new[] {"FixedRate"}, new [] { new ContributionRateStairCaseStep(40m, 0.4500m), new ContributionRateStairCaseStep(60m, 0.8500m), new ContributionRateStairCaseStep(80m, 1.2000m), new ContributionRateStairCaseStep(100m, 0.7375m) }),
                    new ContributionRateStairCase(2, new[] {"FixedRateInterestOnly"}, new [] { new ContributionRateStairCaseStep(40m, 0.5500m), new ContributionRateStairCaseStep(60m, 1.1500m), new ContributionRateStairCaseStep(80m, 2.0000m), new ContributionRateStairCaseStep(100m, 1.0675m) }),
                    new ContributionRateStairCase(3, new[] {"RateMax", "Flex5", "Flex6", "Flex7", "Flex8", "Flex9", "Flex10"}, new [] { new ContributionRateStairCaseStep(40m, 0.5000m), new ContributionRateStairCaseStep(60m, 1.0500m), new ContributionRateStairCaseStep(80m, 1.4500m), new ContributionRateStairCaseStep(100m, 0.8750m) }),
                    new ContributionRateStairCase(4, new[] {"FShort"}, new [] { new ContributionRateStairCaseStep(40m, 0.5000m), new ContributionRateStairCaseStep(60m, 1.0500m), new ContributionRateStairCaseStep(80m, 1.5500m), new ContributionRateStairCaseStep(100m, 0.9000m) }),
                    new ContributionRateStairCase(5, new[] {"Flex3", "Flex4"}, new [] { new ContributionRateStairCaseStep(40m, 0.7000m), new ContributionRateStairCaseStep(60m, 1.2500m), new ContributionRateStairCaseStep(80m, 1.6500m), new ContributionRateStairCaseStep(100m, 1.0750m) }),
                    new ContributionRateStairCase(6, new[] {"Flex1", "Flex2"}, new [] { new ContributionRateStairCaseStep(40m, 0.7500m), new ContributionRateStairCaseStep(60m, 1.3000m), new ContributionRateStairCaseStep(80m, 1.9000m), new ContributionRateStairCaseStep(100m, 1.1175m) })
                }),
            new CompanyContributionRateStairCases(
                5,
                new[]
                {
                    new ContributionRateStairCase(1, new[] {"FixedRate"}, new [] { new ContributionRateStairCaseStep(40m, 0.4500m), new ContributionRateStairCaseStep(60m, 0.8500m), new ContributionRateStairCaseStep(80m, 1.2000m), new ContributionRateStairCaseStep(100m, 0.7375m) }),
                    new ContributionRateStairCase(2, new[] {"FixedRateInterestOnly"}, new [] { new ContributionRateStairCaseStep(40m, 0.5500m), new ContributionRateStairCaseStep(60m, 1.1500m), new ContributionRateStairCaseStep(80m, 2.0000m), new ContributionRateStairCaseStep(100m, 1.0675m) }),
                    new ContributionRateStairCase(3, new[] {"RateMax", "Flex5", "Flex6", "Flex7", "Flex8", "Flex9", "Flex10"}, new [] { new ContributionRateStairCaseStep(40m, 0.5000m), new ContributionRateStairCaseStep(60m, 1.0500m), new ContributionRateStairCaseStep(80m, 1.4500m), new ContributionRateStairCaseStep(100m, 0.8750m) }),
                    new ContributionRateStairCase(4, new[] {"FShort"}, new [] { new ContributionRateStairCaseStep(40m, 0.5000m), new ContributionRateStairCaseStep(60m, 1.0500m), new ContributionRateStairCaseStep(80m, 1.5500m), new ContributionRateStairCaseStep(100m, 0.9000m) }),
                    new ContributionRateStairCase(5, new[] {"Flex3", "Flex4"}, new [] { new ContributionRateStairCaseStep(40m, 0.7000m), new ContributionRateStairCaseStep(60m, 1.2500m), new ContributionRateStairCaseStep(80m, 1.6500m), new ContributionRateStairCaseStep(100m, 1.0750m) }),
                    new ContributionRateStairCase(6, new[] {"Flex1", "Flex2"}, new [] { new ContributionRateStairCaseStep(40m, 0.7500m), new ContributionRateStairCaseStep(60m, 1.3000m), new ContributionRateStairCaseStep(80m, 1.9000m), new ContributionRateStairCaseStep(100m, 1.1175m) })
                })
        };

        static void Main(string[] args)
        {
            var tasksToWait = new List<Task>();
            using (var httpClient = new HttpClient())
            {
                tasksToWait.Add(httpClient.PutAsync(
                    $"http://configurationbutler-externalapi.azurewebsites.net/v0/loandata/201608221134/producttypes", // Needs to be provided via configuration and via routing metadata
                    new StringContent(JsonConvert.SerializeObject(ProductTypes), Encoding.UTF8, "application/json")));

                tasksToWait.Add(httpClient.PutAsync(
                    $"http://configurationbutler-externalapi.azurewebsites.net/v0/loandata/201608221134/companies", // Needs to be provided via configuration and via routing metadata
                    new StringContent(JsonConvert.SerializeObject(Companies), Encoding.UTF8, "application/json")));

                foreach (var company in CompanyContributionRateStairCases)
                {
                    tasksToWait.Add(httpClient.PutAsync(
                        $"http://configurationbutler-externalapi.azurewebsites.net/v0/loandata/201608221134/contributionratestaircase/{company.Id}", // Needs to be provided via configuration and via routing metadata
                        new StringContent(JsonConvert.SerializeObject(company.ContributionRateStairCases), Encoding.UTF8, "application/json")));
                }

                Task.WaitAll(tasksToWait.ToArray());
            }
        }
    }
}
