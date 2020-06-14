using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Xunit;

namespace txt2png.Tests
{
    public class Tests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private const string TestUriForTransparentBackground = "/txt2png?input=.";
        private const string TestUriForWhiteBackground = "/txt2png?input=.&background=wHiTe";
        private const string TextPlain = "text/plain";
        private const string ImagePng = "image/png";

        private const string Base64OutputForTransparentBackground =
            "iVBORw0KGgoAAAANSUhEUgAAAAwAAAAWCAMAAADD5o0oAAAABlBMVEX///8AAABVwtN+AAAAAXRSTlMAQObYZgAAABNJREFUGNNjYBhKgJGREZlDc/sAAzAABnp1FpUAAAAASUVORK5CYII=";

        private const string Base64OutputForWhiteBackground =
            "iVBORw0KGgoAAAANSUhEUgAAAAwAAAAWCAMAAADD5o0oAAAAHlBMVEX////Z2dmamprq6uojIyMAAABTU1NdXV0FBQWMjIyWJksTAAAAG0lEQVQY02NgGOSAkYkZwWFhZUNw2Dk4aW05ABOxAC7xszW6AAAAAElFTkSuQmCC";

        private readonly HttpClient _httpClient;

        public Tests(CustomWebApplicationFactory<Startup> factory)
        {
            _httpClient = factory.CreateClient();
        }

        [Theory]
        [InlineData(TestUriForTransparentBackground, TextPlain, TextPlain, Base64OutputForTransparentBackground)]
        [InlineData(TestUriForWhiteBackground,       TextPlain, TextPlain, Base64OutputForWhiteBackground)]
        [InlineData(TestUriForTransparentBackground, ImagePng,  ImagePng,  Base64OutputForTransparentBackground)]
        [InlineData(TestUriForWhiteBackground,       ImagePng,  ImagePng,  Base64OutputForWhiteBackground)]
        [InlineData(TestUriForTransparentBackground, "*/*",     ImagePng,  Base64OutputForTransparentBackground)]
        [InlineData(TestUriForWhiteBackground,       "foo/bar", ImagePng,  Base64OutputForWhiteBackground)]
        internal async void Get_ReturnsExpectedContentType_ForAcceptHeader(string url, string acceptHeader,
            string expectedContentType, string expectedB64)
        {
            var expectedBytes = expectedContentType == TextPlain
                ? Encoding.UTF8.GetBytes(expectedB64)
                : Convert.FromBase64String(expectedB64);

            var response = await _httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Get, url)
                {Headers = {Accept = {new MediaTypeWithQualityHeaderValue(acceptHeader)}}});
            Assert.True(response.IsSuccessStatusCode);
            Assert.Equal(expectedContentType, response.Content.Headers.ContentType.ToString());
            Assert.Equal(expectedBytes.Length, response.Content.Headers.ContentLength);
            Assert.Equal(expectedBytes, await response.Content.ReadAsByteArrayAsync());
        }

        [Theory]
        [InlineData("/txt2png")]
        [InlineData("/txt2png?input=")]
        [InlineData("/txt2png?input= ")]
        [InlineData("/txt2png?input=A long string that violates the test maximum input limit of 70 characters")]
        public async void Get_InvalidInput_Returns_BadRequest(string url)
        {
            var response = await _httpClient.GetAsync(url);
            Assert.False(response.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}