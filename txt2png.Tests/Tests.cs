using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace txt2png.Tests
{
    public class Tests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private const string TestUriForTransparentBackground = "/txt2png/v1.0?input=.";
        private const string TestUriForWhiteBackground = "/txt2png/v1.0?input=.&background=wHiTe";
        private const string TestUriForBlackBackground = "/txt2png/v1.0?input=.&background=BlAcK";
        // TestUriForPunctuation is the following string URL-encoded: `¬¦\|!"£€$%^&*()_+-=[];'#,./{}:@~<>?
        private const string TestUriForPunctuation = "/txt2png/v1.0?input=%60%C2%AC%C2%A6%5C%7C%21%22%C2%A3%E2%82%AC%24%25%5E%26%2A%28%29_%2B-%3D%5B%5D%3B%27%23%2C.%2F%7B%7D%3A%40~%3C%3E%3F";
        private const string TestUriForRiverCrab = "/txt2png/v1.0?input=河蟹";
        private const string TextPlain = "text/plain";
        private const string ImagePng = "image/png";

        private readonly HttpClient _httpClient;

        public Tests(CustomWebApplicationFactory<Startup> factory)
        {
            _httpClient = factory.CreateClient();
        }

        [WindowsTheory]
        [InlineData(TestUriForTransparentBackground, TextPlain, TextPlain, WindowsTestConstants.Base64OutputForTransparentBackground)]
        [InlineData(TestUriForWhiteBackground,       TextPlain, TextPlain, WindowsTestConstants.Base64OutputForWhiteBackground)]
        [InlineData(TestUriForBlackBackground,       TextPlain, TextPlain, WindowsTestConstants.Base64OutputForBlackBackground)]
        [InlineData(TestUriForPunctuation,           TextPlain, TextPlain, WindowsTestConstants.Base64OutputForPunctuation)]
        [InlineData(TestUriForRiverCrab,             TextPlain, TextPlain, WindowsTestConstants.Base64OutputForRiverCrab)]
        [InlineData(TestUriForTransparentBackground, ImagePng,  ImagePng,  WindowsTestConstants.Base64OutputForTransparentBackground)]
        [InlineData(TestUriForWhiteBackground,       ImagePng,  ImagePng,  WindowsTestConstants.Base64OutputForWhiteBackground)]
        [InlineData(TestUriForBlackBackground,       ImagePng,  ImagePng,  WindowsTestConstants.Base64OutputForBlackBackground)]
        [InlineData(TestUriForRiverCrab,             ImagePng,  ImagePng,  WindowsTestConstants.Base64OutputForRiverCrab)]
        [InlineData(TestUriForTransparentBackground, "*/*",     ImagePng,  WindowsTestConstants.Base64OutputForTransparentBackground)]
        [InlineData(TestUriForWhiteBackground,       "foo/bar", ImagePng,  WindowsTestConstants.Base64OutputForWhiteBackground)]
        internal async void Windows_Get_ReturnsExpectedContentType_ForAcceptHeader(string url, string acceptHeader,
            string expectedContentType, string expectedB64)
        {
            await Test(url, acceptHeader, expectedContentType, expectedB64);
        }

        [LinuxTheory]
        [InlineData(TestUriForTransparentBackground, TextPlain, TextPlain, LinuxTestConstants.Base64OutputForTransparentBackground)]
        [InlineData(TestUriForWhiteBackground,       TextPlain, TextPlain, LinuxTestConstants.Base64OutputForWhiteBackground)]
        [InlineData(TestUriForBlackBackground,       TextPlain, TextPlain, LinuxTestConstants.Base64OutputForBlackBackground)]
        [InlineData(TestUriForPunctuation,           TextPlain, TextPlain, LinuxTestConstants.Base64OutputForPunctuation)]
        [InlineData(TestUriForRiverCrab,             TextPlain, TextPlain, LinuxTestConstants.Base64OutputForRiverCrab)]
        [InlineData(TestUriForTransparentBackground, ImagePng,  ImagePng,  LinuxTestConstants.Base64OutputForTransparentBackground)]
        [InlineData(TestUriForWhiteBackground,       ImagePng,  ImagePng,  LinuxTestConstants.Base64OutputForWhiteBackground)]
        [InlineData(TestUriForBlackBackground,       ImagePng,  ImagePng,  LinuxTestConstants.Base64OutputForBlackBackground)]
        [InlineData(TestUriForRiverCrab,             ImagePng,  ImagePng,  LinuxTestConstants.Base64OutputForRiverCrab)]
        [InlineData(TestUriForTransparentBackground, "*/*",     ImagePng,  LinuxTestConstants.Base64OutputForTransparentBackground)]
        [InlineData(TestUriForWhiteBackground,       "foo/bar", ImagePng,  LinuxTestConstants.Base64OutputForWhiteBackground)]
        internal async void Linux_Get_ReturnsExpectedContentType_ForAcceptHeader(string url, string acceptHeader,
            string expectedContentType, string expectedB64)
        {
            await Test(url, acceptHeader, expectedContentType, expectedB64);
        }

        private async Task Test(string url, string acceptHeader,
            string expectedContentType, string expectedB64)
        {
            var expectedBytes = expectedContentType == TextPlain
                ? Encoding.UTF8.GetBytes(expectedB64)
                : Convert.FromBase64String(expectedB64);

            var response = await _httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Get, url)
                { Headers = { Accept = { new MediaTypeWithQualityHeaderValue(acceptHeader) } } });
            Assert.True(response.IsSuccessStatusCode);
            Assert.Equal(expectedContentType, response.Content.Headers.ContentType.ToString());
            Assert.Equal(expectedBytes.Length, response.Content.Headers.ContentLength);
            Assert.Equal(expectedBytes, await response.Content.ReadAsByteArrayAsync());
        }

        [Theory]
        [InlineData("/txt2png/v1.0")]
        [InlineData("/txt2png/v1.0?input=")]
        [InlineData("/txt2png/v1.0?input= ")]
        [InlineData("/txt2png/v1.0?input=A long string that violates the test maximum input limit of 70 characters")]
        public async void Get_InvalidInput_Returns_BadRequest(string url)
        {
            var response = await _httpClient.GetAsync(url);
            Assert.False(response.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}