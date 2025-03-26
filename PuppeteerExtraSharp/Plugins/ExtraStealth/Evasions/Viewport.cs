using System.Threading.Tasks;
using PuppeteerSharp;
namespace PuppeteerExtraSharp.Plugins.ExtraStealth.Evasions
{
    public class Viewport : PuppeteerExtraPlugin
    {

        public Viewport() : base("viewport")
        {
        }

        public override async Task OnPageCreated(IPage page)
        {
            // Change default Puppeteer values
            await page.SetViewportAsync(new ViewPortOptions
            {
                Width = 1920,
                Height = 1080,
            });
        }
    }
}
