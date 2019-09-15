using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using actualizer.Models;
using System.Collections;

namespace actualizer.Controllers
{
    [Route("api/[controller]")]
    public class PageController : Controller
    {
        private static readonly HttpClient client = new HttpClient();
        List<string> returnObject = new List<string> { };
       

        static async Task<string> GetCommentsNextPageAsync(string video_id, string NextPageToken, string search) //void means returns nothing
        {


            string key = "AIzaSyCFDwRa8R7V2g3H-7GzcLkPzedoPIruaVg";
            string u = "https://www.googleapis.com/youtube/v3/commentThreads?part=snippet&maxResults=100";
            string url = $"{u}&pageToken={NextPageToken}&searchTerms={search}&textFormat=plainText&videoId={video_id}&key={key}";            

            var response = await client.GetAsync(url);
            var content = await response.Content.ReadAsStringAsync();
            RootObject rootobject = JsonConvert.DeserializeObject<RootObject>(content);
            List<Comments> commentRows = new List<Comments> { };
            int index = 0;
            foreach (var i in rootobject.items)
            {
                commentRows.Add(new Comments { id = index, text = i.snippet.topLevelComment.snippet.textDisplay, language = "en" });
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
        /*
        static async Task<string> GetCommentsAsync(string video_id1, string lang1 = "en")
        {
            string key1 = "AIzaSyCFDwRa8R7V2g3H-7GzcLkPzedoPIruaVg";
            string u1 = "https://www.googleapis.com/youtube/v3/commentThreads?key=";
            string url1 = $"{u1}{key1}&textFormat=plaintext&part=snippet&videoId={video_id1}&maxResults=100";
            var response1 = await client.GetAsync(url1);
            var content1 = await response1.Content.ReadAsStringAsync();

            RootObject rootobject1 = JsonConvert.DeserializeObject<RootObject>(content1);

            List<Comments> commentRows1 = new List<Comments> { };

            int index1 = 0;
            foreach (var i1 in rootobject1.items)
            {
                commentRows1.Add(new Comments { id = index1, text = i1.snippet.topLevelComment.snippet.textDisplay, language = lang1 });
                index1++;
            }


            string nextPage1 = rootobject1.nextPageToken;
            string json1 = JsonConvert.SerializeObject(new
            {
                video_id1,
                count = commentRows1.Count,
                comments = commentRows1,
                nextPage1,

            });
            return json1;
        }
        */
        [HttpGet]
        public async Task<ActionResult<string>> GetAsync(string v, string s, string p)  {

            //List<ReturnJson> Ncomments = new List<ReturnJson> { };
            //string c = await GetCommentsAsync(video_id1: v);
            string c = await GetCommentsNextPageAsync(video_id: v, search: s, NextPageToken: p);
            //ReturnJson cj = JsonConvert.DeserializeObject<ReturnJson>(c);

            return Ok(c);
            /*

            Console.WriteLine(cj);
            Console.WriteLine("**********************************************************************************");
            string np = cj.nextPage;
            Console.WriteLine(np);

            Ncomments.Add(cj);

            for (int x = 0; x < n; x++) {
                string c1 = await GetCommentsNextPageAsync(video_id: v, search: s, NextPageToken: np);
                ReturnJson cj1 = JsonConvert.DeserializeObject<ReturnJson>(c1);
                Ncomments.Add(cj1);
                np = cj1.nextPage;
            }
           
            */
        }



    }
}
