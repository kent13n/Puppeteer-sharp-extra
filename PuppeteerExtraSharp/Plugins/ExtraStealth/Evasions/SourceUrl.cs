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
            await page.EvaluateFunctionOnNewDocumentAsync(@"
                () => {
                    const originalPrepareStackTrace = Error.prepareStackTrace;
                    Error.prepareStackTrace = (err, structuredStackTrace) => {
                        const stack = structuredStackTrace
                            .map(callSite => callSite.toString().replace('puppeteer_evaluation_script', ''))
                            .join('\n');
                        return originalPrepareStackTrace
                            ? originalPrepareStackTrace(err, structuredStackTrace)
                            : stack;
                    };
                }
            ");
        }
    }
}
