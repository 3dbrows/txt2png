using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;
using System.Text;
using ImageMagick;
using Microsoft.AspNetCore.Mvc;
using txt2png.Filters;

namespace txt2png.Controllers
{
    [ApiVersion("1.0")]
    [ApiController]
    [ServiceFilter(typeof(InputFilterAttribute))]
    [Route("[controller]/v{version:apiVersion}")]
    public class Txt2PngController : Controller
    {
        public static string Font => RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
            ? "Microsoft YaHei & Microsoft YaHei UI"
            : "WenQuanYi Zen Hei Mono";

        /// <summary>
        ///     Creates a PNG image from the given input text.
        /// </summary>
        /// <remarks>
        ///     Send an Accept header of "text/plain" to get Base64 output. Any other Accept header (including none) returns
        ///     an "image/png" response.
        /// </remarks>
        /// <param name="input" example="foo@example.com">
        ///     The text to render. The default max length is 280 characters. Must
        ///     contain non-whitespace characters.
        /// </param>
        /// <param name="background" example="white">
        ///     Valid values: 'white' for black text on white background; 'black' for white
        ///     text on black background; any other value gives black text on a transparent background.
        /// </param>
        [HttpGet]
        [Produces("text/plain", "image/png")]
        public byte[] Get([FromQuery] [Required] string input, [FromQuery] string background)
        {
            GetColours(background, out var backgroundColor, out var textColor);

            var settings = new MagickReadSettings
            {
                FontFamily = Font,
                FontPointsize = 18,
                TextGravity = Gravity.West,
                BackgroundColor = backgroundColor,
                FillColor = textColor,
                Width = input.Length * 24,
                UseMonochrome = true,
                TextEncoding = Encoding.UTF8
            };
            input = input.Replace("%", "%%");
            input = input.Replace("\\", "\\\\");
            using var caption = new MagickImage($"caption:{input}", settings) {Format = MagickFormat.Png8};
            caption.Trim();
            caption.Strip();
            return caption.ToByteArray();
        }

        private static void GetColours(string background, out MagickColor backgroundColor, out MagickColor textColor)
        {
            backgroundColor = MagickColors.Transparent;
            textColor = MagickColors.Black;
            if ("white".Equals(background, StringComparison.InvariantCultureIgnoreCase))
            {
                backgroundColor = MagickColors.White;
            }
            else if ("black".Equals(background, StringComparison.InvariantCultureIgnoreCase))
            {
                backgroundColor = MagickColors.Black;
                textColor = MagickColors.White;
            }
        }
    }
}