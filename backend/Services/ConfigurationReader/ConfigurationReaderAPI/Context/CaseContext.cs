using System.Collections.Generic;
using System.Data;
using ConfigurationReaderAPI.Entity;
using Microsoft.EntityFrameworkCore;

namespace ConfigurationReaderAPI.Context
{
    public class CaseContext : DbContext
    {
        public CaseContext(DbContextOptions<CaseContext> options)
           : base(options)
        {
        }

        public DbSet<ConfigurationItem> ConfigurationItem { get; set; }
    }
}
