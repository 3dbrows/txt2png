using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;

namespace txt2png.Formatters
{
    public class PngOutputFormatter : OutputFormatter
    {
        public PngOutputFormatter()
        {
            SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("image/png"));
        }

        public override async Task WriteResponseBodyAsync(OutputFormatterWriteContext context)
        {
            var response = (byte[]) context.Object;
            context.HttpContext.Response.Headers.Add("Content-Length", response.Length.ToString());
            await context.HttpContext.Response.Body.WriteAsync(response);
        }

        protected override bool CanWriteType(Type type)
        {
            return type == typeof(byte[]);
        }
    }
}