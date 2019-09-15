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
    [ApiController]
    public class ValuesController : ControllerBase
    {

        private static readonly HttpClient client = new HttpClient();
        List<string> returnObject = new List<string> { };
        //var client = new HttpClient();

        static async Task<string> GetCommentsAsync(string video_id, string lang = "en") {
            string key = "AIzaSyCFDwRa8R7V2g3H-7GzcLkPzedoPIruaVg";
            string u = "https://www.googleapis.com/youtube/v3/commentThreads?key=";
            string url = $"{u}{key}&textFormat=plaintext&part=snippet&videoId={video_id}&maxResults=100";
            var response = await client.GetAsync(url);
            var content = await response.Content.ReadAsStringAsync();

            RootObject rootobject = JsonConvert.DeserializeObject<RootObject>(content);

            List<Comments> commentRows = new List<Comments> { };

            int index = 0;
            foreach (var i in rootobject.items) {
                commentRows.Add(new Comments { id = index, text = i.snippet.topLevelComment.snippet.textDisplay, language = lang });
                index++;
            }


            string nextPage = rootobject.nextPageToken;
            string json = JsonConvert.SerializeObject(new {
                video_id,
                count = commentRows.Count,
                comments = commentRows,
                nextPage,

            });
            return json;
        }


        // GET api/values/ --- youtube video id 
        [HttpGet]
        public async Task<ActionResult<string>> GetAsync(string video_id, string lang)
        {
            string c = await GetCommentsAsync(video_id: video_id, lang:lang);
            Console.WriteLine(c);
            return c;
        }
    


        // POST api/values/youtubevideo id
        [HttpPost]
        [Produces("application/json")]
        public async Task<ActionResult<string>> PostItemAsync([FromBody] string value)
        {
         
            try {
                var c = await GetCommentsAsync(value);
                string videoId = value;
                //return Ok( new Value{ video_id = videoId,  c });
                return Ok(c);
            }
            catch {
                return BadRequest();
            }
        }

        //Comment search feature. 
      // [HttpGet]
        //public ActionResult<IEnumerable<string>> SearchComments(string q)
       // {
         //   return Ok();
        //}


    }
}
