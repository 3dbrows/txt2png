using System;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace txt2png.Swagger
{
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