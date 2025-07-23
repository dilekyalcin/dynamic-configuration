using A_ServiceAPI.Context;
using A_ServiceAPI.Entity;
using ConfigurationReaderAPI.Service;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace A_ServiceAPI.Service
{
    public class ProductService
    {
        private readonly ConfigurationReader _configReader;
        private readonly CaseContext _db;

        public ProductService(ConfigurationReader configReader, CaseContext db)
        {
            _configReader = configReader;
            _db = db;
        }

        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            try
            {
                int maxItems = _configReader.GetValue<int>("MaxItemCount");
                bool isEnabled = _configReader.GetValue<bool>("IsBasketEnabled");

                if (!isEnabled)
                    throw new Exception("Basket feature is disabled!");

                Console.WriteLine("max item: " + maxItems);


                var products = await _db.Product
                                        .Take(maxItems)
                                        .ToListAsync();
                return products;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Config error: " + ex.Message);
  
                return await _db.Product
                                .Take(10)
                                .ToListAsync();
            }
        }
    }

}
