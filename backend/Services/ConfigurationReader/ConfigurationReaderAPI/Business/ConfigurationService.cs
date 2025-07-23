using ConfigurationReaderAPI.Context;
using ConfigurationReaderAPI.Entity;
using Microsoft.EntityFrameworkCore;

namespace ConfigurationReaderAPI.Business
{
    public class ConfigurationService : IConfigurationService
    {
        private readonly CaseContext _context;

        public ConfigurationService(CaseContext context)
        {
            _context = context;
        }
        public async Task<List<ConfigurationItem>> GetAllConfigurationsAsync()
        {
            return await _context.ConfigurationItem.ToListAsync();
        }
        public async Task<List<ConfigurationItem>> GetConfigurationsAsync(string applicationName)
        {
            return await _context.ConfigurationItem
                .Where(c => c.ApplicationName == applicationName)
                .ToListAsync();
        }

        public async Task AddConfigurationAsync(ConfigurationItem item)
        {
            _context.ConfigurationItem.Add(item);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateConfigurationAsync(ConfigurationItem item)
        {
            _context.ConfigurationItem.Update(item);
            await _context.SaveChangesAsync();
        }
    }
}
