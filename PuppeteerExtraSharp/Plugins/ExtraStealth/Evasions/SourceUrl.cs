using System.Reflection;
using System.Threading.Tasks;
using PuppeteerSharp;

namespace PuppeteerExtraSharp.Plugins.ExtraStealth.Evasions
{
    public class SourceUrl : PuppeteerExtraPlugin
    {
        public SourceUrl() : base("SourceUrl")
        {
        }

        public override async Task OnPageCreated(IPage page)
        {
            page.Load += async (_, _) =>
            {
                await page.EvaluateFunctionAsync(@"
                    () => {
                        Error.prepareStackTrace = (_, structuredStackTrace) => {
                            return structuredStackTrace
                                .map(callSite => callSite.toString().replace('puppeteer_evaluation_script', ''))
                                .join('\n');
                        };
                    }
                ");
            };
        }
    }
}