namespace PrecisionDealApi.Models
{
    public class ConfigurationSettings : IConfigurationSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }

    public interface IConfigurationSettings
    {
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}