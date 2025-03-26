using System.Reflection;
using System.Text.Json;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using PuppeteerExtraSharp.Utils;
using PuppeteerSharp;

namespace Extra.Tests.Utils
{
    public class FingerPrint
    {
        /// <summary>
        /// https://antoinevastel.com/bots/
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public async Task<JObject> GetFingerPrint(IPage page)
        {
            var script = ResourcesReader.ReadFile("Extra.Tests.StealthPluginTests.Script.fpCollect.js", Assembly.GetExecutingAssembly());
            await page.EvaluateExpressionAsync(script);

            var jsonElement =
                await page.EvaluateFunctionAsync<JsonElement>("async () => await fpCollect().generateFingerprint()");

            return JObject.Parse(jsonElement.GetRawText());
        }
    }
}
