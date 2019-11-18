using System;
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
        public async Task<ActionResult<ReturnJson>> GetSearchComments(string video_id, string search, string lang = "en", int count = 25) {
            try {
                var results = await Helpers.SearchComments(video_id: video_id, search: search, lang: lang, count: count);
                results.Cast<ReturnJson>();
                return Ok(results);
            } catch {
                return BadRequest("Please enter a video id");
            }
        }




        [HttpGet]
        [Route("bulk")]
        [Produces("application/json")]

        public async Task<ActionResult<ReturnJson>> GetAsync(string video_id, string search, string nextPageToken) {

            // get all the comments while they are less than the comment restriction i imposed. Use SQL lite for the restriction eventually. 
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

                // Return the comments neatly.
                var allcomments = obj.SelectMany(o => o.comments.Select(c => new {
                    id = c.id,
                    text = c.text,
                    language = c.language,
                    publishedAt = c.publishedAt,
                    likeCount = c.likeCount
                }).ToList());

                int? sum = obj.Sum(s => s.count);
                var meta = obj.Select(s => new { count = sum, search = s.search, video_id = s.video_id }).FirstOrDefault();
                var finalObject = new { documents = allcomments, metadata = meta };

                return Ok(finalObject);
            }
            return BadRequest("Please enter a video id");
        }

    }

}