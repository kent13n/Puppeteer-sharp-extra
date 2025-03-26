using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xunit;
using RestClient = PuppeteerExtraSharp.Plugins.Recaptcha.RestClient.RestClient;

namespace Extra.Tests.Recaptcha.RestClientTest
{
    [Collection("Captcha")]
    public class RestClientTests
    {
        [Fact]
        public async Task ShouldWorkPostWithJson()
        {
            var client = new RestClient("https://postman-echo.com");
            var data = new Dictionary<string, string>
            {
                {
                    "test", "123"
                },
            };

            var result = await client.PostWithJsonAsync<Dictionary<string, object>>("/post", data, CancellationToken.None);

            Assert.NotNull(result);
            Assert.True(result.ContainsKey("json"));

            var jsonData = JsonConvert.DeserializeObject<Dictionary<string, string>>(result["json"].ToString());
            Assert.Equal(data, jsonData);
        }

    }
}
