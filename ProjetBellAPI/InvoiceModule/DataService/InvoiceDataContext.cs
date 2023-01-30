

using InvoiceModule.Models;
using Microsoft.EntityFrameworkCore;

namespace InvoiceModule.DataService
{
    public class InvoiceDataContext : DbContext
    {
        
        IConfiguration _configuration;
        public InvoiceDataContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public DbSet<Invoice> Invoice { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (Environment.GetEnvironmentVariable("DBServer") != null)
            {
                optionsBuilder.UseSqlServer(_configuration.GetConnectionString("ContainerConnection"));
            }
            else
            {
                optionsBuilder.UseSqlServer(_configuration.GetConnectionString("DevConnection"));
            }
        }
    }
}
