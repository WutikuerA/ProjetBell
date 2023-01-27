using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using Assets.Module.Models;

namespace Assets.Module.DataService
{
    public class AssetDataContext : DbContext
    {
        // Note: We can create a mother class which inherits the DbContext, then put this IConfiguration attribute there
        // and inherit that class here instead of DbContext it self
        IConfiguration _configuration;
        public AssetDataContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public DbSet<Assets.Module.Models.Asset> Assets { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_configuration.GetConnectionString("DevConnection"));
        }
    }
}
