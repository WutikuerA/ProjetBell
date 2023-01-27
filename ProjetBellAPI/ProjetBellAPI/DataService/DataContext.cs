using Microsoft.EntityFrameworkCore;
using ProjetBellAPI.Models;

namespace ProjetBellAPI.DataService
{
    public class DataContext : DbContext
    {
        
        IConfiguration _configuration;
        public DataContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public DbSet<Asset> Assets { get; set; } = null!;
        public DbSet<Invoice> Invoice { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_configuration.GetConnectionString("DevConnection"));
        }
    }
}
