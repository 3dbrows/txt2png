using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using txt2png.Filters;
using txt2png.Formatters;

namespace txt2png
{
    public class Startup
    {
        private const string InputConfigurationSectionKey = "Input";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<InputOptions>(Configuration.GetSection(InputConfigurationSectionKey));
            services.AddRazorPages();
            services.AddScoped<InputFilterAttribute>();
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    builder =>
                    {
                        builder.AllowAnyOrigin();
                    });
            });
            services.AddControllers(options =>
            {
                options.OutputFormatters.Insert(0, new PngOutputFormatter());
                options.OutputFormatters.Insert(1, new Base64OutputFormatter());
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();
            app.UseCors();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapRazorPages();
            });
        }
    }
}