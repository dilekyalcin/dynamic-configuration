using A_ServiceAPI.Entity;
using ConfigurationReaderAPI.Entity;
using Microsoft.EntityFrameworkCore;

namespace A_ServiceAPI.Context
{
    public class CaseContext : DbContext
    {
        public CaseContext(DbContextOptions<CaseContext> options)
          : base(options)
        {
        }

        public DbSet<Product> Product { get; set; }
    }
}
