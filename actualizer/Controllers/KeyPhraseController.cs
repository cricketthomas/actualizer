using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.CognitiveServices.Language.TextAnalytics;
using Microsoft.Azure.CognitiveServices.Language.TextAnalytics.Models;
using Microsoft.Rest;
using actualizer.Models;
using Newtonsoft.Json;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Linq;
using System.Collections;

namespace actualizer.Controllers
{




    [Route("api/[controller]")]
    public class KeyPhraseController : Controller
    {
        static async Task<string> CallTextAnalyticsAPI(Docs json)
        {
            var client = new HttpClient();
            var queryString = HttpUtility.ParseQueryString(string.Empty);
            // Request headers
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "075e9f44d55d49ffad994b55b979434e");
            var uri = "https://eastus.api.cognitive.microsoft.com/text/analytics/v2.1/keyPhrases" + queryString;
            HttpResponseMessage response;

            var xxxxx = Encoding.UTF8.GetBytes(String.Concat(json));

            string output = JsonConvert.SerializeObject(json);
            Console.WriteLine(output);

            byte[] byteData = Encoding.UTF8.GetBytes(output);
            Console.WriteLine("byte data");
            Console.WriteLine(byteData);

            using (var content = new ByteArrayContent(byteData))
            {
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                response = await client.PostAsync(uri, content);

                string res = "";
                using (HttpContent x = response.Content)
                {
                    // ... Read the string.
                    Task<string> result = x.ReadAsStringAsync();
                    res = result.Result;
                    Console.WriteLine("xxxxxxxxxxxxxxxxxxxxxxx");
                    Console.WriteLine(res);

                    return res;
                }


            }



        }





        [HttpGet]
        public async Task<ActionResult<string>> GetAsync()
        {
            //string j = await CallTextAnalyticsAPI(Documents Document);
            return "hello";
        }


        // POST api/values
        [HttpPost]
        public async Task<IList> PostAsync([FromBody] Docs json)
        {
            string result = await CallTextAnalyticsAPI(json);
            TextAnalytics textanalyticsresponse = JsonConvert.DeserializeObject<TextAnalytics>(result);

            var p = textanalyticsresponse.documents; //.GroupBy(i => i.keyPhrases);

            var allphrases = p.SelectMany(s => s.keyPhrases).ToList();




            var allPhrasesCount = allphrases.GroupBy(x => x)
                .Where(g => g.Count() > 1)
                .Select(y => new { word = y.Key, count = y.Count() })
                .ToList();

            return allPhrasesCount;

        }


    }
}
