using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using actualizer.Models;
using System.Collections;
using Microsoft.AspNetCore.Authorization;


namespace actualizer.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [Authorize]

    public class SearchController : Controller
    {
        private static readonly HttpClient client = new HttpClient();
        List<string> returnObject = new List<string> { };
        //var client = new HttpClient();

        static async Task<string> GetCommentsNextPageAsync(string video_id, string search, string lang, int count) //void means returns nothing
        {
            string key = "AIzaSyCFDwRa8R7V2g3H-7GzcLkPzedoPIruaVg";
            string u = "https://www.googleapis.com/youtube/v3/commentThreads?key=";
            string url = $"{u}{key}&textFormat=plaintext&part=snippet&videoId={video_id}&maxResults={count}&searchTerms={search}";

            var response = await client.GetAsync(url);
            var content = await response.Content.ReadAsStringAsync();
            RootObject rootobject = JsonConvert.DeserializeObject<RootObject>(content);
            List<Comments> commentRows = new List<Comments> { };
            int index = 0;
            foreach (var i in rootobject.items)
            {
                commentRows.Add(new Comments { id = index, text = i.snippet.topLevelComment.snippet.textDisplay, language = "en", publishedAt = i.snippet.topLevelComment.snippet.publishedAt });
                index++;
            }
            //IEnumerable<string> obj = rootobject.items.Select(r => r.snippet.topLevelComment.snippet.textDisplay).Where(r => r == "dog");

            string nextPage = rootobject.nextPageToken;
            string json = JsonConvert.SerializeObject(new
            {
                search,
                url,
                video_id,
                count = commentRows.Count,
                comments = commentRows,
                nextPage,

            });
            return json;
        }

        [HttpGet]
        public async Task<ActionResult<string>> GetAsync(string video_id, string search, string lang = "en", int count = 20)
        {
            string c = await GetCommentsNextPageAsync(video_id: video_id, search: search, lang: lang, count: count);
            return Ok(c);
        }
    }
}
