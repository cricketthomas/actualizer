using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http;
using actualizer.Models;
using System.Web;
using System.Text;
using System.Net.Http.Headers;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Okta.Sdk;
using Microsoft.Extensions.Configuration;
using System.Globalization;

namespace actualizer.ExternalAPI.TextAnalytics {
    public class TextAnalticsAPI {

        private static readonly HttpClient client = new HttpClient();


        public static async Task<string> CallTextAnalyticsAPI(Docs json, string RequestType, string azure_key) {

            var queryString = HttpUtility.ParseQueryString(string.Empty);

            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", azure_key);
            var uri = $"https://actualizer.cognitiveservices.azure.com/text/analytics/v2.1/" + RequestType + queryString;
            Console.WriteLine(uri);
            HttpResponseMessage response;


            string output = JsonConvert.SerializeObject(json);
            byte[] byteData = Encoding.UTF8.GetBytes(output);

            using (var content = new ByteArrayContent(byteData)) {
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                response = await client.PostAsync(uri, content);

                string res = "";
                using (HttpContent x = response.Content) {
                    // ... Read the string.
                    Task<string> result = x.ReadAsStringAsync();
                    res = result.Result;

                    return res;
                }

            }
        }
    }

}
