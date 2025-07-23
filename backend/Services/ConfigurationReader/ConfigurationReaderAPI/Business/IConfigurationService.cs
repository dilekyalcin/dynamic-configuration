using ConfigurationReaderAPI.Entity;

namespace ConfigurationReaderAPI.Business
{
    public interface IConfigurationService
    {
        Task<List<ConfigurationItem>> GetAllConfigurationsAsync();
        Task<List<ConfigurationItem>> GetConfigurationsAsync(string applicationName);
        Task AddConfigurationAsync(ConfigurationItem item);
        Task UpdateConfigurationAsync(ConfigurationItem item);
    }
}
