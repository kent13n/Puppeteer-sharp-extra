using System.Threading.Tasks;
using PuppeteerExtraSharp.Plugins.ExtraStealth.Evasions;
using PuppeteerSharp;
using Xunit;
namespace Extra.Tests.StealthPluginTests.EvasionsTests
{
    public class ViewportTest : BrowserDefault
    {
        [Fact]
        public async Task ShouldWork()
        {
            var plugin = new Viewport();
            var page = await LaunchAndGetPage(plugin);
            await page.GoToAsync("https://google.com");
            Assert.Equal(new ViewPortOptions { Width = 1920, Height = 1080 }, page.Viewport);
        }
    }
}
