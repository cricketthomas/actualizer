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
using actualizer.Utils;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace actualizer.Controllers {
    [Route("api/[controller]")]

    public class CommentsController : Controller {




        [Produces("application/json")]
        [Route("search")]
        [HttpGet]
        public async Task<ActionResult<string>> GetSearchComments(string video_id, string search, string lang = "en", int count = 25) {
            var results = await Helpers.SearchComments(video_id: video_id, search: search, lang: lang, count: count);

            return Ok(results);
        }


        [HttpGet]
        [Route("bulk")]
        [Produces("application/json")]

        public async Task<ActionResult<string>> GetAsync(string video_id, string search, string nextPageToken) {

            if (!string.IsNullOrWhiteSpace(video_id)) {
                List<ReturnJson> obj = new List<ReturnJson> { };

                string nextPageIdFromQuery = null;
                int allCommentCount = 0;
                int index = 0;
                do {
                    var result = await Helpers.GetCommentsNextPageAsync(
                        video_id: video_id, search: search,
                        NextPageToken: nextPageIdFromQuery, lastNumOfComments: allCommentCount);

                    var results = result.Cast<ReturnJson>();

                    nextPageIdFromQuery = results.Select(p => p.nextPage).First();

                    obj.Add(new ReturnJson {
                        search = results.Select(x => x.search).First(),
                        count = results.Select(x => x.count).First(),
                        //url = results.Select(x => x.url).First(),
                        video_id = results.Select(x => x.video_id).First(),
                        comments = results.Select(x => x.comments).First(),
                        nextPage = results.Select(x => x.nextPage).First(),
                    });
                    allCommentCount += results.Select(c => c.count).Last();

                } while (!string.IsNullOrEmpty(nextPageIdFromQuery) && index < 20);

                var allcomments = obj.SelectMany(o => o.comments.Select(c =>
                new { id = c.id, text = c.text, language = c.language, publishedAt = c.publishedAt, likeCount = c.likeCount }).ToList());

                int? sum = obj.Sum(s => s.count);
                var meta = obj.Select(s => new { count = sum, search = s.search, video_id = s.video_id }).FirstOrDefault();
                var finalObject = new { documents = allcomments, metadata = meta };

                return Ok(finalObject);
            }
            return "video id field cannot be null or empty";
        }

    }




    //[HttpGet]
    //[Route("whole")]
    //public async Task<ActionResult<string>> GetWholeAsync(string v, string s, string n, int pageReqCount = 1) {
    //    List<ReturnJson> obj = new List<ReturnJson> { };

    //    var result = await Helpers.GetCommentsNextPageAsync(video_id: v, search: s, NextPageToken: n, lastNumOfComments: 0);
    //    var results = result.Cast<ReturnJson>();
    //    var nextPageIdFromQuery = results.Select(p => p.nextPage).First();
    //    Console.WriteLine(nextPageIdFromQuery);
    //    int allCommentCount = -100; //HACK. Its this value so the index starts a 0, im sure i couldve done better but this works. 

    //    if (pageReqCount == 1 || string.IsNullOrEmpty(nextPageIdFromQuery)) {
    //        return Ok(result);
    //    } else {
    //        for (var index = 0; index < pageReqCount; index++) {
    //            Console.WriteLine(nextPageIdFromQuery);
    //            if (!string.IsNullOrEmpty(nextPageIdFromQuery)) {

    //                int lastNumOfCommentInt = results.Select(c => c.count).Last();
    //                allCommentCount += lastNumOfCommentInt;
    //                result = await Helpers.GetCommentsNextPageAsync(video_id: v, search: s, NextPageToken: nextPageIdFromQuery, lastNumOfComments: allCommentCount);
    //                Console.WriteLine(nextPageIdFromQuery);
    //                results = result.Cast<ReturnJson>();
    //                nextPageIdFromQuery = results.Select(p => p.nextPage).First();
    //                obj.Add(new ReturnJson {
    //                    search = results.Select(x => x.search).First(),
    //                    count = results.Select(x => x.count).First(),
    //                    url = results.Select(x => x.url).First(),
    //                    video_id = results.Select(x => x.video_id).First(),
    //                    comments = results.Select(x => x.comments).First(),
    //                    nextPage = results.Select(x => x.nextPage).First(),
    //                });
    //            }
    //        }
    //    }


    //    var allcomments = obj.SelectMany(o => o.comments.Select(c => new { id = c.id, text = c.text, language = c.language }).ToList());

    //    var a = allcomments.SelectMany(s => s.text).ToArray();
    //    string concatComments = String.Join("", a.ToArray());

    //    var whole = new { id = 0, text = concatComments, language = allcomments.Select(s => s.language).FirstOrDefault() };
    //    var meta = obj.Select(s => new { count = s.count, search = s.search, video_id = s.video_id });
    //    //var alldata = obj.Select(o => o.comments.Select(c => new { id = c.id, text = c.text, language = c.language }).ToList());
    //    var finalObject = new { documents = whole, metadata = meta };
    //    return Ok(finalObject);

    //}


}
