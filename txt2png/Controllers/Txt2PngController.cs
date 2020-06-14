using System;
using System.ComponentModel.DataAnnotations;
using System.Text;
using ImageMagick;
using Microsoft.AspNetCore.Mvc;
using txt2png.Filters;

namespace txt2png.Controllers
{
    [ApiController]
    [ServiceFilter(typeof(InputFilterAttribute))]
    public class Txt2PngController : Controller
    {
        [Route("/txt2png")]
        public byte[] Get([FromQuery] [Required] string input, [FromQuery] string background)
        {
            var settings = new MagickReadSettings
            {
                FontFamily = "Courier New",
                FontPointsize = 18,
                TextGravity = Gravity.Center,
                BackgroundColor = "white".Equals(background, StringComparison.InvariantCultureIgnoreCase)
                    ? MagickColors.White
                    : MagickColors.Transparent,
                Width = input.Length * 12,
                UseMonochrome = true,
                TextEncoding = Encoding.UTF8
            };
            using var caption = new MagickImage($"caption:{input}", settings) {Format = MagickFormat.Png8};
            caption.Strip();
            return caption.ToByteArray();
        }
    }
}