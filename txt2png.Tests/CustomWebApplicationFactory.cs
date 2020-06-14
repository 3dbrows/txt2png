using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;

namespace txt2png.Tests
{
    public class CustomWebApplicationFactory<TStartup>
        : WebApplicationFactory<TStartup> where TStartup : class
    {
        private const string InputMaxLengthConfigKey = "Input__MaxLength";
        private const string InputMaxLengthTestValue = "70";

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            Environment.SetEnvironmentVariable(InputMaxLengthConfigKey, InputMaxLengthTestValue);
            builder.ConfigureAppConfiguration(c => c.AddEnvironmentVariables());
        }
    }
}