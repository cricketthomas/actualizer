﻿using System;
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
    public class SearchController : Controller
    {
        private static readonly HttpClient client = new HttpClient();
        List<string> returnObject = new List<string> { };
        //var client = new HttpClient();

        static async Task<string> GetCommentsNextPageAsync(string video_id, string search, string lang) //void means returns nothing
        {
            string key = "AIzaSyCFDwRa8R7V2g3H-7GzcLkPzedoPIruaVg";
            string u = "https://www.googleapis.com/youtube/v3/commentThreads?key=";
            string url = $"{u}{key}&textFormat=plaintext&part=snippet&videoId={video_id}&maxResults=100&searchTerms={search}";

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

        [HttpGet]
        public async Task<ActionResult<string>> GetAsync( string v, string s, string l = "en")
        {
            string c = await GetCommentsNextPageAsync(video_id: v, search: s, lang: l);
            return Ok(c);
        }
    }
}
