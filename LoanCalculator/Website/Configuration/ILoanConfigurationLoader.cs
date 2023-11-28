using Website.Configuration.LoanModels;

namespace Website.Configuration
{
    public interface ILoanConfigurationLoader
    {
        ConfigurationData Load();
    }
}