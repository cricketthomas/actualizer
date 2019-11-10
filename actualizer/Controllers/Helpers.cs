using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http;
using actualizer.Models;
using System.Web;
using System.Text;
using System.Net.Http.Headers;

namespace actualizer.Controllers

{
    public class Helpers
    {
        public Helpers()
        {

            static async Task<string> CallTextAnalyticsAPI(Docs json)
            {
                var client = new HttpClient();
                var queryString = HttpUtility.ParseQueryString(string.Empty);
                // Request headers
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "de4105f3c9f24d739e7915a6ea764d2f");
                var uri = "https://actualizer.cognitiveservices.azure.com/text/analytics/v2.1/keyPhrases" + queryString;

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
                        return res;
                    }

                }

            }







        }
    }
}
