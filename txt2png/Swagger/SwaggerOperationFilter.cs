using System.Collections.Generic;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace txt2png.Swagger
{
    public class SwaggerOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            operation.Responses.Clear();
            operation.Responses.Add("200", new OpenApiResponse
            {
                Description = "The image in the desired encoding (text/plain or image/png).",
                Content = new Dictionary<string, OpenApiMediaType>
                {
                    {
                        "text/plain", new OpenApiMediaType
                        {
                            Examples = new Dictionary<string, OpenApiExample>
                            {
                                {
                                    "Usage in HTML", new OpenApiExample
                                    {
                                        Description =
                                            "This is how to embed an example Base64-encoded image in HTML. This is not the image you generated above.",
                                        Value = new OpenApiString(
                                            $"<img alt=\"{Constants.AltText}\" src=\"data:image/png;base64,{Constants.Base64OutputForWhiteBackground}\">")
                                    }
                                },
                                {
                                    "Usage in Markdown", new OpenApiExample
                                    {
                                        Description =
                                            "This is how to embed an example Base64-encoded image in Markdown. This is not the image you generated above.",
                                        Value = new OpenApiString(
                                            $"![{Constants.AltText}](data:image/png;base64,{Constants.Base64OutputForWhiteBackground})")
                                    }
                                }
                            }
                        }
                    },
                    {
                        "image/png", new OpenApiMediaType
                        {
                            Examples = new Dictionary<string, OpenApiExample>
                            {
                                {
                                    "Usage in HTML", new OpenApiExample
                                    {
                                        Description =
                                            "This is how to load an example binary image in HTML. This is not the image you generated above.",
                                        Value = new OpenApiString(
                                            $"<img alt=\"{Constants.AltText}\" src=\"{Constants.Uri}\">")
                                    }
                                },
                                {
                                    "Usage in Markdown", new OpenApiExample
                                    {
                                        Description =
                                            "This is how to load an example binary image in Markdown. This is not the image you generated above.",
                                        Value = new OpenApiString(
                                            $"![{Constants.AltText}]({Constants.Uri})")
                                    }
                                }
                            }
                        }
                    }
                }
            });
            operation.Responses.Add("400", new OpenApiResponse {Description = "The input was invalid."});
        }
    }
}