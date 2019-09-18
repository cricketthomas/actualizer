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


namespace actualizer.Controllers
{
    [Route("api/[controller]")]
    public class KeyPhraseController : Controller
    {
        static async Task<string> CallTextAnalyticsAPI() {
            var client = new HttpClient();
            var queryString = HttpUtility.ParseQueryString(string.Empty);
            // Request headers
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "075e9f44d55d49ffad994b55b979434e");
            var uri = "https://eastus.api.cognitive.microsoft.com/text/analytics/v2.1/keyPhrases" + queryString;
            HttpResponseMessage response;

            Comments data = new Comments { id = 0, text = "I love you, money", language = "en" };
            Console.WriteLine("**************************************************************");

            Console.WriteLine(new { id = data.id, text = data.text, language = data.language });
            Console.WriteLine("**************************************************************");
            var d = $@"
                   {{
                  ""documents"": [
                    {{
                      ""language"": ""en"",
                      ""id"": ""1"",
                      ""text"": ""Hello world. This is some input text that I love.""
                    }},
                    {{
                      ""language"": ""fr"",
                      ""id"": ""2"",
                      ""text"": ""Bonjour tout le monde""
                    }},
                    {{
                      ""language"": ""es"",
                      ""id"": ""3"",
                      ""text"": ""La carretera estaba atascada. Había mucho tráfico el día de ayer.""
                    }}
                  ]
                }}
                ";
            byte[] byteData = Encoding.UTF8.GetBytes(d);
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
                    Console.WriteLine(res);
                    return res;
                }
                

            }

           

        }

    


     
        [HttpGet]
        public async Task<ActionResult<string>> GetAsync()
        {
            string test = await CallTextAnalyticsAPI();
            Console.WriteLine(test);
            return test;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(string id)
        {

            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
      

        }


    }
}
