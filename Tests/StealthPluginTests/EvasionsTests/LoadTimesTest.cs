﻿using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using PuppeteerExtraSharp.Plugins.ExtraStealth.Evasions;
using Xunit;

namespace Extra.Tests.StealthPluginTests.EvasionsTests
{
    public class LoadTimesTest: BrowserDefault
    {
        [Fact]
        public async Task ShouldWork()
        {
            var stealthPlugin = new LoadTimes();
            var page = await LaunchAndGetPage(stealthPlugin);

            await page.GoToAsync("https://google.com");

            var loadTimesJsonElement = await page.EvaluateFunctionAsync("() => window.chrome.loadTimes()");
            Assert.NotNull(loadTimesJsonElement);
            var loadTimes = JObject.Parse(loadTimesJsonElement.Value.GetRawText());
            Assert.NotNull(loadTimes["connectionInfo"]);
            Assert.NotNull(loadTimes["npnNegotiatedProtocol"]);
            Assert.NotNull(loadTimes["wasAlternateProtocolAvailable"]);
            Assert.NotNull(loadTimes["wasAlternateProtocolAvailable"]);
            Assert.NotNull(loadTimes["wasFetchedViaSpdy"]);
            Assert.NotNull(loadTimes["wasNpnNegotiated"]);
            Assert.NotNull(loadTimes["firstPaintAfterLoadTime"]);
            Assert.NotNull(loadTimes["requestTime"]);
            Assert.NotNull(loadTimes["startLoadTime"]);
            Assert.NotNull(loadTimes["commitLoadTime"]);
            Assert.NotNull(loadTimes["finishDocumentLoadTime"]);
            Assert.NotNull(loadTimes["firstPaintTime"]);
        }
    }
}
