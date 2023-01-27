using Assets.Module.DataService;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.FileProviders;
using Microsoft.OpenApi.Models;
using Swashbuckle.Swagger;
using System.Reflection;

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
            services.AddScoped<AssetDataContext>();
            services.AddCors( option =>
            {
                option.AddPolicy(name: "MyAllowSpecifications",
                    builder =>
                    {
                        builder.WithOrigins(
                                "http://localhost:4200",
                                "http://localhost:4201",
                                "http://localhost:5011"
                            );
                    });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            app.UseStaticFiles();
            app.UseSwaggerUI(c =>
            {
                c.RoutePrefix = "";
                c.SwaggerEndpoint("v1/swagger.json", "My service");
            });
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
