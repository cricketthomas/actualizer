﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using actualizer.Models;
using System.Collections;
using System.Security.Cryptography.X509Certificates;
using System.Reflection.Metadata;

namespace actualizer.Controllers
{
    [Route("api/[controller]")]
    public class CountController : Controller
    {
        private static readonly HttpClient client = new HttpClient();



        static async Task<IList> GetCommentsNextPageAsync(string video_id, string search, int lastNumOfComments, string NextPageToken = null)  //void means returns nothing
        {

            string key = "AIzaSyCFDwRa8R7V2g3H-7GzcLkPzedoPIruaVg";
            string u = "https://www.googleapis.com/youtube/v3/commentThreads?part=snippet&maxResults=100";
            string url = $"{u}&pageToken={NextPageToken}&searchTerms={search}&textFormat=plainText&videoId={video_id}&key={key}";

            var response = await client.GetAsync(url);
            var content = await response.Content.ReadAsStringAsync();
            RootObject rootobject = JsonConvert.DeserializeObject<RootObject>(content);
            List<Comments> commentRows = new List<Comments> { };
            List<ReturnJson> ReturnObject = new List<ReturnJson> { };




            int index = lastNumOfComments;
            foreach (var i in rootobject.items)
            {
                commentRows.Add(new Comments
                {
                    id = index,
                    text = i.snippet.topLevelComment.snippet.textDisplay,
                    language = "en",
                    publishedAt = i.snippet.topLevelComment.snippet.publishedAt
                });
                index++;
            }
            string nextPage = rootobject.nextPageToken;
            string json = JsonConvert.SerializeObject(new
            {
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



        [HttpGet]
        [Route("individual")]
        public async Task<ActionResult<string>> GetAsync(string v, string s, string n, int pageReqCount = 1)
        {
            List<ReturnJson> obj = new List<ReturnJson> { };

            var result = await GetCommentsNextPageAsync(video_id: v, search: s, NextPageToken: n, lastNumOfComments: 0);
            var results = result.Cast<ReturnJson>();
            var nextPageIdFromQuery = results.Select(p => p.nextPage).First();
            Console.WriteLine(nextPageIdFromQuery);
            int allCommentCount = -100; //HACK. Its this value so the index starts a 0, im sure i couldve done better but this works. 

            if (pageReqCount == 1 || string.IsNullOrEmpty(nextPageIdFromQuery))
            {
                var singleMeta = results.Select(sm => new { count = sm.count, video_id = sm.video_id, search = sm.search });
                var singleObject = new { documents = results.Select(s => s.comments.ToList()), metadata = singleMeta };

                return Ok(singleObject);
            }
            else
            {
                for (var index = 0; index < pageReqCount; index++)
                {
                    Console.WriteLine(nextPageIdFromQuery);
                    if (!string.IsNullOrEmpty(nextPageIdFromQuery))
                    {

                        int lastNumOfCommentInt = results.Select(c => c.count).Last();
                        allCommentCount += lastNumOfCommentInt;
                        result = await GetCommentsNextPageAsync(video_id: v, search: s, NextPageToken: nextPageIdFromQuery, lastNumOfComments: allCommentCount);
                        Console.WriteLine(nextPageIdFromQuery);
                        results = result.Cast<ReturnJson>();
                        nextPageIdFromQuery = results.Select(p => p.nextPage).First();
                        obj.Add(new ReturnJson
                        {
                            search = results.Select(x => x.search).First(),
                            count = results.Select(x => x.count).First(),
                            //url = results.Select(x => x.url).First(),
                            video_id = results.Select(x => x.video_id).First(),
                            comments = results.Select(x => x.comments).First(),
                            nextPage = results.Select(x => x.nextPage).First(),
                        });
                    }
                }
            }


            var allcomments = obj.SelectMany(o => o.comments.Select(c => new { id = c.id, text = c.text, language = c.language, publishedAt = c.publishedAt }).ToList());
            var meta = obj.Select(s => new { count = s.count, search = s.search, video_id = s.video_id });
            //var alldata = obj.Select(o => o.comments.Select(c => new { id = c.id, text = c.text, language = c.language }).ToList());
            var finalObject = new { documents = allcomments, metadata = meta };
            return Ok(finalObject);

        }




        [HttpGet]
        [Route("whole")]
        public async Task<ActionResult<string>> GetWholeAsync(string v, string s, string n, int pageReqCount = 1)
        {
            List<ReturnJson> obj = new List<ReturnJson> { };

            var result = await GetCommentsNextPageAsync(video_id: v, search: s, NextPageToken: n, lastNumOfComments: 0);
            var results = result.Cast<ReturnJson>();
            var nextPageIdFromQuery = results.Select(p => p.nextPage).First();
            Console.WriteLine(nextPageIdFromQuery);
            int allCommentCount = -100; //HACK. Its this value so the index starts a 0, im sure i couldve done better but this works. 

            if (pageReqCount == 1 || string.IsNullOrEmpty(nextPageIdFromQuery))
            {
                return Ok(result);
            }
            else
            {
                for (var index = 0; index < pageReqCount; index++)
                {
                    Console.WriteLine(nextPageIdFromQuery);
                    if (!string.IsNullOrEmpty(nextPageIdFromQuery))
                    {

                        int lastNumOfCommentInt = results.Select(c => c.count).Last();
                        allCommentCount += lastNumOfCommentInt;
                        result = await GetCommentsNextPageAsync(video_id: v, search: s, NextPageToken: nextPageIdFromQuery, lastNumOfComments: allCommentCount);
                        Console.WriteLine(nextPageIdFromQuery);
                        results = result.Cast<ReturnJson>();
                        nextPageIdFromQuery = results.Select(p => p.nextPage).First();
                        obj.Add(new ReturnJson
                        {
                            search = results.Select(x => x.search).First(),
                            count = results.Select(x => x.count).First(),
                            url = results.Select(x => x.url).First(),
                            video_id = results.Select(x => x.video_id).First(),
                            comments = results.Select(x => x.comments).First(),
                            nextPage = results.Select(x => x.nextPage).First(),
                        });
                    }
                }
            }


            var allcomments = obj.SelectMany(o => o.comments.Select(c => new { id = c.id, text = c.text, language = c.language }).ToList());

            var a = allcomments.SelectMany(s => s.text).ToArray();
            string concatComments = String.Join("", a.ToArray());

            var whole = new { id = 0, text = concatComments, language = allcomments.Select(s => s.language).FirstOrDefault() };
            var meta = obj.Select(s => new { count = s.count, search = s.search, video_id = s.video_id });
            //var alldata = obj.Select(o => o.comments.Select(c => new { id = c.id, text = c.text, language = c.language }).ToList());
            var finalObject = new { documents = whole, metadata = meta };
            return Ok(finalObject);

        }

    }
}
