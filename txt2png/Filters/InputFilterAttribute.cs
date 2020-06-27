using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;

namespace txt2png.Filters
{
    public class InputFilterAttribute : Attribute, IResourceFilter
    {
        private const string InputQueryParamKey = "input";
        private readonly InputOptions _settings;

        public InputFilterAttribute(IOptions<InputOptions> options)
        {
            _settings = options.Value;
        }

        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            if (context.HttpContext.Request.Query.TryGetValue(InputQueryParamKey, out var input)
                && IsAcceptableInput(input))
                return;
            context.Result = new BadRequestResult();
        }

        public void OnResourceExecuted(ResourceExecutedContext context)
        {
        }

        internal bool IsAcceptableInput(string input)
        {
            return !string.IsNullOrWhiteSpace(input) && input.Length <= _settings.MaxLength;
        }
    }
}