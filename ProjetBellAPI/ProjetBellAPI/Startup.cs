
using Microsoft.AspNetCore.Http.Features;
using ProjetBellAPI.DataService;
using ProjetBellAPI.Interface;
using ProjetBellAPI.Services;

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
            services.Configure<FormOptions>(fo =>
            {
                fo.ValueLengthLimit = int.MaxValue;
                fo.MultipartBodyLengthLimit = int.MaxValue;
                fo.MemoryBufferThreshold = int.MaxValue;
            });

            services.AddControllers();
            services.AddScoped<DataContext>();
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

        }
    }
}
