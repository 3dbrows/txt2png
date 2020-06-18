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
        // TestUriForPunctuation is the following string URL-encoded: `¬¦\|!"£€$%^&*()_+-=[];'#,./{}:@~<>?
        private const string TestUriForPunctuation = "/txt2png?input=%60%C2%AC%C2%A6%5C%7C%21%22%C2%A3%E2%82%AC%24%25%5E%26%2A%28%29_%2B-%3D%5B%5D%3B%27%23%2C.%2F%7B%7D%3A%40~%3C%3E%3F";
        private const string TextPlain = "text/plain";
        private const string ImagePng = "image/png";

        private const string Base64OutputForTransparentBackground =
            "iVBORw0KGgoAAAANSUhEUgAAAAwAAAAWCAMAAADD5o0oAAAABlBMVEX///8AAABVwtN+AAAAAXRSTlMAQObYZgAAABNJREFUGNNjYBhKgJGREZlDc/sAAzAABnp1FpUAAAAASUVORK5CYII=";

        private const string Base64OutputForWhiteBackground =
            "iVBORw0KGgoAAAANSUhEUgAAAAwAAAAWCAMAAADD5o0oAAAAHlBMVEX////Z2dmamprq6uojIyMAAABTU1NdXV0FBQWMjIyWJksTAAAAG0lEQVQY02NgGOSAkYkZwWFhZUNw2Dk4aW05ABOxAC7xszW6AAAAAElFTkSuQmCC";

        private const string Base64OutputForPunctuation =
            "iVBORw0KGgoAAAANSUhEUgAAAbAAAAAWCAMAAACWojlfAAAABlBMVEX///8AAABVwtN+AAAAAXRSTlMAQObYZgAAAcRJREFUaN7tWOHSwyAIa97/pb/96kQTQLHbdztzt123okCiFHtdBwcHBwc+ULpd8bFl6k/x8I9C9tmEsr0NgNcn6QKNJRIyosjT7Q/cWxvOGmkiiYiREuD+CWHaGqBV775CQzfYXFxH4k9lH7KCwNb4jTlmpEDYKSU7txc2ibskWE871KQqKVjignVcF+yaEyw5L+Jx9mKHYnzJC8GGnSDGDBajrma9PSMY8wAgXupIzovOCWVFbYRVFAVr0++i8WkpCgYY6gWxLLnEDgsFe3ttmFB500Tz6DIVtLg+OFGWIfHsb6/YVqBhveOdDJ0TkLBIzguz0qC2XEBKFvD+yRIwEPiufSpBl+5MRxkY+LbLFTEgrA2+q4JR+cgCyzdnkuhu8eW9IFimJC4mFz0abELwE9p4oiNFLX1YcYr80OJbA57f800HNY7qdqFLrB4sU6mmAx1I6b68kROHwQwzTrzSFbMQrVPYdPgRidpYgteMKze2GoEcp3mtkC9LymV+STB7rPUJET3SVIybD8422Q2TLQysjp/LIXr8XGGlefjVVJRq8+sXBFuLVgn2eWV+GtHLXz7o21EfHHwJgPNC5uDg4GACfydmAf861vsPAAAAAElFTkSuQmCC";

        private readonly HttpClient _httpClient;

        public Tests(CustomWebApplicationFactory<Startup> factory)
        {
            _httpClient = factory.CreateClient();
        }

        [Theory]
        [InlineData(TestUriForTransparentBackground, TextPlain, TextPlain, Base64OutputForTransparentBackground)]
        [InlineData(TestUriForWhiteBackground,       TextPlain, TextPlain, Base64OutputForWhiteBackground)]
        [InlineData(TestUriForPunctuation,           TextPlain, TextPlain, Base64OutputForPunctuation)]
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