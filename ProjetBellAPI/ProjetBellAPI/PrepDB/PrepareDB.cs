

using AssetModule.DataService;
using AssetModule.Models;
using InvoiceModule.DataService;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetBellAPI.PrepDB
{
    public static class PrepareDB
    {
        public static void PrepPopulation(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                SeedData(serviceScope.ServiceProvider.GetService<AssetDataContext>(),
                    serviceScope.ServiceProvider.GetService<InvoiceDataContext>());
            }
        }

        public static void SeedData(AssetDataContext assetDBContext, InvoiceDataContext invoiceDBContext)
        {
            Console.WriteLine("Appling database migration...");
            assetDBContext.Database.Migrate();
            invoiceDBContext.Database.Migrate();

            if(!assetDBContext.Asset.Any())
            {
                Console.WriteLine("Creating data samples...");

                Asset assetSample1 = new Asset()
                {
                    Name = "Service1",
                    Price = 10,
                    ValidFrom = DateTime.Now.AddDays(-10),
                    ValidTo = DateTime.Now.AddDays(10),
                };

                Asset assetSample2 = new Asset()
                {
                    Name = "Service2",
                    Price = 10,
                    ValidFrom = DateTime.Now.AddDays(-10),
                };

                Asset assetSample3 = new Asset()
                {
                    Name = "Service3",
                    Price = 15
                };

                assetDBContext.Asset.AddRange(assetSample1, assetSample2, assetSample3);
                assetDBContext.SaveChanges();
            }

        }
        

    }
}
