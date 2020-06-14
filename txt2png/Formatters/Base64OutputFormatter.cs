using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;

namespace txt2png.Formatters
{
    public class Base64OutputFormatter : OutputFormatter
    {
        public Base64OutputFormatter()
        {
            SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("text/plain"));
        }

        public override async Task WriteResponseBodyAsync(OutputFormatterWriteContext context)
        {
            var response = Convert.ToBase64String((byte[]) context.Object);
            context.HttpContext.Response.Headers.Add("Content-Length", response.Length.ToString());
            await context.HttpContext.Response.WriteAsync(response);
        }

        protected override bool CanWriteType(Type type)
        {
            return type == typeof(byte[]);
        }
    }
}