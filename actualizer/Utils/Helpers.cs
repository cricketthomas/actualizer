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

namespace actualizer.Utils {
    public class Helpers {

        private static readonly HttpClient client = new HttpClient();


        public static async Task<string> CallTextAnalyticsAPI(Docs json, string RequestType, string azure_key) {

            var queryString = HttpUtility.ParseQueryString(string.Empty);
            // Request headers

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





        public static async Task<List<ReturnJson>> GetCommentsNextPageAsync(string video_id, string search, int lastNumOfComments, string youtube_key, string NextPageToken = null) {
            //void means returns nothing
            string u = "https://www.googleapis.com/youtube/v3/commentThreads?part=snippet&maxResults=100";
            string url = $"{u}&pageToken={NextPageToken}&searchTerms={search}&textFormat=plainText&videoId={video_id}&key={youtube_key}";

            var response = await client.GetAsync(url);
            var content = await response.Content.ReadAsStringAsync();
            RootObject rootobject = JsonConvert.DeserializeObject<RootObject>(content);
            List<Comments> commentRows = new List<Comments> { };
            List<ReturnJson> ReturnObject = new List<ReturnJson> { };

            int index = lastNumOfComments;
            foreach (var i in rootobject.items) {
                commentRows.Add(new Comments {
                    id = index,
                    text = i.snippet.topLevelComment.snippet.textDisplay,
                    language = "en",
                    publishedAt = i.snippet.topLevelComment.snippet.publishedAt,
                    likeCount = i.snippet.topLevelComment.snippet.likeCount
                });
                index++;
            }
            string nextPage = rootobject.nextPageToken;
            string json = JsonConvert.SerializeObject(new {
                search,
                //url,
                video_id,
                count = commentRows.Count,
                comments = commentRows,
                nextPage
            });

            ReturnObject.Add(new ReturnJson { search = search, url = url, video_id = video_id, comments = commentRows.ToArray(), count = commentRows.Count, nextPage = nextPage });

            return ReturnObject;

        }


        public static async Task<string> SearchComments(string video_id, string search, string lang, int count, string youtube_key) {
            //void means returns nothing
            //List<string> returnObject = new List<string> { };
            string u = "https://www.googleapis.com/youtube/v3/commentThreads?key=";
            string url = $"{u}{youtube_key}&textFormat=plaintext&part=snippet&videoId={video_id}&maxResults={count}&searchTerms={search}";

            var response = await client.GetAsync(url);
            var content = await response.Content.ReadAsStringAsync();
            RootObject rootobject = JsonConvert.DeserializeObject<RootObject>(content);
            List<Comments> commentRows = new List<Comments> { };
            int index = 0;
            foreach (var i in rootobject.items) {
                commentRows.Add(new Comments { id = index, text = i.snippet.topLevelComment.snippet.textDisplay, language = "en", publishedAt = i.snippet.topLevelComment.snippet.publishedAt });
                index++;
            }
            //IEnumerable<string> obj = rootobject.items.Select(r => r.snippet.topLevelComment.snippet.textDisplay).Where(r => r == "dog");

            string nextPage = rootobject.nextPageToken;
            string json = JsonConvert.SerializeObject(new {
                search,
                video_id,
                count = commentRows.Count,
                comments = commentRows,
                nextPage,

            });
            return json;
        }

    }
}
