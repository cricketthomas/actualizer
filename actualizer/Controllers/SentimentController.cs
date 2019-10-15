using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Rest;
using actualizer.Models;
using Newtonsoft.Json;
using System.Linq;
using System.Collections;

namespace actualizer.Controllers
{


    [Route("api/[controller]")]
    public class SentimentController : Controller
    {
        static async Task<string> CallTextAnalyticsAPI(Docs json)
        {

            var client = new HttpClient();
            var queryString = HttpUtility.ParseQueryString(string.Empty);
            // Request headers
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "de4105f3c9f24d739e7915a6ea764d2f");
            var uri = "https://actualizer.cognitiveservices.azure.com/text/analytics/v2.1/sentiment" + queryString;
            //

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


        // POST api/values
        [HttpPost]
        [Route("general")]
        public async Task<IList> PostAsync([FromBody] DocsWithTime json)
        {

            Docs jsonDoc = JsonConvert.DeserializeObject<Docs>(JsonConvert.SerializeObject(json));

            string result = await CallTextAnalyticsAPI(jsonDoc);

            Sentiment sentimentresponse = JsonConvert.DeserializeObject<Sentiment>(result);

            var sentimentscores = sentimentresponse.documents;

            var originaldocument = json.documents;


            var query = sentimentscores.Join(originaldocument,
                                    s => s.id,
                                    o => o.id,
                                    (s, o) => new { id = s.id, text = o.text, score = s.score, date = o.publishedAt });

            return query.ToList();
            //TODO sentiment works and joins well, now I need to aggregate it or something for sentiment overitme?
        }



        //[HttpPost]
        //[Route("whole")]
        //public async Task<IList> WholePostAsync([FromBody] DocsWithTime json)
        //{

        //    Docs jsonDoc = JsonConvert.DeserializeObject<Docs>(JsonConvert.SerializeObject(json));

        //    string result = await CallTextAnalyticsAPI(jsonDoc);

        //    Sentiment sentimentresponse = JsonConvert.DeserializeObject<Sentiment>(result);

        //    var sentimentscores = sentimentresponse.documents.SelectMany(s => s.t);




        //    return "test";
        //    //TODO sentiment works and joins well, now I need to aggregate it or something for sentiment overitme?
        //}
    }
}
