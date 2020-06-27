using System;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;
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
            services.AddApiVersioning(options => { options.ReportApiVersions = true; });
            services.AddScoped<InputFilterAttribute>();
            services.AddCors(options => options.AddDefaultPolicy(builder => { builder.AllowAnyOrigin(); }));
            services.AddControllers(options =>
            {
                options.OutputFormatters.Insert(0, new PngOutputFormatter());
                options.OutputFormatters.Insert(1, new Base64OutputFormatter());
            });
            services.AddVersionedApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVVV";
                options.SubstituteApiVersionInUrl = true;
            });
            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
            services.AddSwaggerGen();
        }

        public void Configure(IApplicationBuilder app, IApiVersionDescriptionProvider provider)
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                foreach (var description in provider.ApiVersionDescriptions)
                    options.SwaggerEndpoint(
                        $"/swagger/{description.GroupName}/swagger.json",
                        description.GroupName);
                options.RoutePrefix = string.Empty;
                options.DocExpansion(DocExpansion.Full);
            });
            app.UseRouting();
            app.UseCors();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }

    internal class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
    {
        private readonly IApiVersionDescriptionProvider _provider;

        public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider)
        {
            _provider = provider;
        }

        public void Configure(SwaggerGenOptions options)
        {
            foreach (var description in _provider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(
                    description.GroupName,
                    new OpenApiInfo
                    {
                        Title = $"Txt2Png API v{description.ApiVersion}",
                        Version = description.ApiVersion.ToString(),
                        Description =
                            "A utility which accepts a string via HTTP endpoint, and returns a PNG graphic of that text.",
                        Contact = new OpenApiContact
                        {
                            Name = "3dbrows",
                            Email = string.Empty,
                            Url = new Uri("https://github.com/3dbrows/txt2png")
                        },
                        License = new OpenApiLicense
                        {
                            Name = "BSD 2-Clause License",
                            Url = new Uri("https://github.com/3dbrows/txt2png/blob/master/LICENSE")
                        }
                    });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);
                options.OperationFilter<SwaggerOperationFilter>();
            }
        }
    }
}