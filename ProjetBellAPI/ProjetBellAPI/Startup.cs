
using AssetModule.DataService;
using InvoiceModule.DataService;
using InvoiceModule.Service;
using InvoiceModule.Services;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;
using ProjetBellAPI.PrepDB;
using Microsoft.AspNetCore.Hosting.Server;

namespace ProjetBellAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            Console.WriteLine($"ProjectBellApi starting...");
            services.Configure<FormOptions>(fo =>
            {
                fo.ValueLengthLimit = int.MaxValue;
                fo.MultipartBodyLengthLimit = int.MaxValue;
                fo.MemoryBufferThreshold = int.MaxValue;
            });

            services.AddControllers();
            

            if(Environment.GetEnvironmentVariable("DBServer") != null)
            {
                // Use below DB connection info if not local
                var server = Environment.GetEnvironmentVariable("DBServer") ?? "db";
                var port = Environment.GetEnvironmentVariable("DBPort") ?? "1433";
                var user = Environment.GetEnvironmentVariable("DBUser") ?? "SA";
                var password = Environment.GetEnvironmentVariable("DBPassword") ?? "Passw0rd2022";
                var database = Environment.GetEnvironmentVariable("Database") ?? "ProjectBellDB";

                services.AddDbContext<AssetDataContext>(o => o.UseSqlServer($"Server={server},{port};Initial Catalog={database};User ID={user};Password={password}"));
                services.AddDbContext<InvoiceDataContext>(o => o.UseSqlServer($"Server={server},{port};Initial Catalog={database};User ID={user};Password={password}"));


            }
            else
            {
                services.AddScoped<AssetDataContext>();
                services.AddScoped<InvoiceDataContext>();
            }

            services.AddScoped<IinvoiceGenerationService, InvoiceGenerationService>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            app.UseStaticFiles();
            app.UseCors(options =>
            options.WithOrigins("http://localhost:4200")
            .AllowAnyMethod()
            .AllowAnyHeader());


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            PrepareDB.PrepPopulation(app); 

        }
    }
}
