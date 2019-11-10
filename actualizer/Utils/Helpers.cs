using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http;
using actualizer.Models;
using System.Web;
using System.Text;
using System.Net.Http.Headers;

namespace actualizer.Utils {
    public static class Helpers {




        public static async Task<string> CallTextAnalyticsAPI(Docs json, string RequestType) {
            var client = new HttpClient();
            var queryString = HttpUtility.ParseQueryString(string.Empty);
            // Request headers
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "1d70014e2d4247689f609fc795143f99");
            var uri = $"https://actualizer.cognitiveservices.azure.com/text/analytics/v2.1/" + RequestType + queryString;
            Console.WriteLine(uri);
            HttpResponseMessage response;

            string output = JsonConvert.SerializeObject(json);
            Console.WriteLine(output);

            byte[] byteData = Encoding.UTF8.GetBytes(output);
            Console.WriteLine("byte data");
            Console.WriteLine(byteData);

            using (var content = new ByteArrayContent(byteData)) {
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                response = await client.PostAsync(uri, content);

                string res = "";
                using (HttpContent x = response.Content) {
                    // ... Read the string.
                    Task<string> result = x.ReadAsStringAsync();
                    res = result.Result;
                    Console.WriteLine(res);

                    return res;
                }

            }
        }



    }
}
