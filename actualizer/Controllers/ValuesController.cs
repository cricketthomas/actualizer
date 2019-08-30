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
        static async Task<string> GetCommentsAsync(string video_id, string lang = "en") //void means returns nothing
        {
            string key = "AIzaSyCFDwRa8R7V2g3H-7GzcLkPzedoPIruaVg";
            string u = "https://www.googleapis.com/youtube/v3/commentThreads?key=";
            string url = $"{u}{key}&textFormat=plaintext&part=snippet&videoId={video_id}&maxResults=100";
            var client = new HttpClient();
            var response = await client.GetAsync(url);
            var content = await response.Content.ReadAsStringAsync();

            RootObject rootobject = JsonConvert.DeserializeObject<RootObject>(content);

            List<Comments> commentRows = new List<Comments> { };

            int index = 0;
            foreach (var i in rootobject.items){
                commentRows.Add(new Comments { id = index, text = i.snippet.topLevelComment.snippet.textDisplay, language = "en" });
                index++; 
            }
            string json = JsonConvert.SerializeObject(new { results = commentRows });
            Console.WriteLine(json);

            return json;
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }



        // GET api/values/ --- youtube video id 
        [HttpGet("{video_id}")]
        public async Task<ActionResult<string>> GetAsync(string video_id)
        {
            string c = await GetCommentsAsync(video_id);
            Console.WriteLine(c);
            return c;
        }
    


        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
            Console.WriteLine(value);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
