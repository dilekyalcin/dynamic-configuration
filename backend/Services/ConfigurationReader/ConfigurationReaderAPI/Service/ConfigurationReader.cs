using ConfigurationReaderAPI.Context;
using ConfigurationReaderAPI.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ConfigurationReaderAPI.Service
{
    public class ConfigurationReader : IHostedService, IDisposable
    {
        private readonly string _applicationName;
        private readonly string _connectionString;
        private readonly int _refreshInterval;
       

        private Dictionary<string, ConfigurationItem> _configCache = new();
        private Timer _refreshTimer;
        private readonly object _lock = new();
        private CaseContext _dbContext;

        public ConfigurationReader(string applicationName,
                                  string connectionString,
                                  int refreshTimerIntervalInMs)
        {
            _applicationName = applicationName;
            _connectionString = connectionString;
            _refreshInterval = refreshTimerIntervalInMs;

            var optionsBuilder = new DbContextOptionsBuilder<CaseContext>();
            optionsBuilder.UseSqlServer(_connectionString);
            _dbContext = new CaseContext(optionsBuilder.Options);
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            Console.Write("başladııı");
            await LoadFromStorage();

            _refreshTimer = new Timer(Refresh, null, _refreshInterval, _refreshInterval);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
         
            _refreshTimer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        public T GetValue<T>(string key)
        {
            lock (_lock)
            {
                if (_configCache.TryGetValue(key, out var item))
                {
                    return (T)Convert.ChangeType(item.Value, typeof(T));
                }

                throw new KeyNotFoundException($"Key not found: {key}");
            }
        }

        private async Task LoadFromStorage()
        {
            try
            {

                Console.Write("Veritabanından konfigürasyonlar yükleniyor");
                var configs = await _dbContext.ConfigurationItem
                    .Where(c => c.ApplicationName == _applicationName && c.IsActive)
                    .AsNoTracking()
                    .ToListAsync();
                Console.WriteLine($"{configs.Count} adet konfigürasyon bulundu");
                lock (_lock)
                {
                    _configCache = configs.ToDictionary(c => c.Name, c => c);
                    
                }
            }
            catch (Exception ex)
            {
               
            }
        }

        private void Refresh(object state) => _ = LoadFromStorage();

        public void Dispose()
        {
            _refreshTimer?.Dispose();
            _dbContext?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}