using System.Text.Json;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using PuppeteerExtraSharp.Plugins.ExtraStealth.Evasions;
using PuppeteerSharp;
using Xunit;

namespace Extra.Tests.StealthPluginTests.EvasionsTests
{
    public class ChromeAppTest : BrowserDefault
    {
        [Fact]
        public async Task ShouldWork()
        {
            var plugin = new ChromeApp();

            var page = await LaunchAndGetPage(plugin);
            await page.GoToAsync("https://google.com");

            var chrome = await page.EvaluateExpressionAsync("window.chrome");
            Assert.NotNull(chrome);

            var app = await page.EvaluateExpressionAsync("chrome.app");
            Assert.NotNull(app);

            var getIsInstalled = await page.EvaluateExpressionAsync<bool>("chrome.app.getIsInstalled()");
            Assert.False(getIsInstalled);

            var installStateJsonElement = await page.EvaluateExpressionAsync("chrome.app.InstallState");
            Assert.NotNull(installStateJsonElement);
            var installState = JObject.Parse(installStateJsonElement.Value.GetRawText());
            Assert.Equal("disabled", installState["DISABLED"]);
            Assert.Equal("installed", installState["INSTALLED"]);
            Assert.Equal("not_installed", installState["NOT_INSTALLED"]);

            var runningStateJsonElement = await page.EvaluateExpressionAsync("chrome.app.RunningState");
            Assert.NotNull(runningStateJsonElement);
            var runningState = JObject.Parse(runningStateJsonElement.Value.GetRawText());
            Assert.Equal("cannot_run", runningState["CANNOT_RUN"]);
            Assert.Equal("ready_to_run", runningState["READY_TO_RUN"]);
            Assert.Equal("running", runningState["RUNNING"]);

            var details = await page.EvaluateExpressionAsync<object>("chrome.app.getDetails()");
            Assert.Null(details);

            var runningStateFunc = await page.EvaluateExpressionAsync<string>("chrome.app.runningState()");
            Assert.Equal("cannot_run", runningStateFunc);


            await Assert.ThrowsAsync<EvaluationFailedException>(async () => await page.EvaluateExpressionAsync("chrome.app.getDetails('foo')"));
        }
    }
}
