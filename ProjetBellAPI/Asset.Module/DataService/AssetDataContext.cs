

using AssetModule.Models;
using Microsoft.EntityFrameworkCore;

namespace AssetModule.DataService
{
    public class AssetDataContext : DbContext
    {
        
        IConfiguration _configuration;
        public AssetDataContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public DbSet<Asset> Asset { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_configuration.GetConnectionString("DevConnection"));
        }
    }
}
