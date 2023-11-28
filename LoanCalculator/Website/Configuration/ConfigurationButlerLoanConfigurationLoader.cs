using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using Website.ApplicationInsights;
using Website.Configuration.LoanModels;

namespace Website.Configuration
{
    public class ConfigurationButlerLoanConfigurationLoader : ILoanConfigurationLoader
    {
        public ConfigurationData Load()
        {
            using (var configurationButlerClient = new HttpClient())
            {
                return Telemetry.Create().TrackDependency(
                    "Configuration Butler",
                    "Load Loan Data",
                    () =>
                    {
                        configurationButlerClient.BaseAddress =
                            new Uri(@"http://configurationbutler-externalapi.azurewebsites.net/v0/loandata/201608221134");
                        configurationButlerClient.DefaultRequestHeaders.Accept.Clear();
                        configurationButlerClient.DefaultRequestHeaders.Accept.Add(
                            new MediaTypeWithQualityHeaderValue("application/json"));

                        var result = configurationButlerClient.GetStringAsync("").Result;
                        var configuration = JsonConvert.DeserializeObject<ConfigurationData>(result);

                        return configuration;
                    });
            }
        }
    }
}