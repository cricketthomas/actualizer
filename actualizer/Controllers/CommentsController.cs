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
using actualizer.ExternalAPI.YoutubeAPI;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Okta.Sdk;
using Okta.Sdk.Configuration;
using Microsoft.Extensions.Configuration;
using actualizer.Infastructure.Data.Actualizer.db;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.AspNetCore.DataProtection.KeyManagement.Internal;

namespace actualizer.Controllers {

    [Route("api/[controller]")]

    public class CommentsController : Controller {

        private readonly ActualizerContext _db;

        private readonly IConfiguration _configuration;
        private readonly IMemoryCache _cache;

        public CommentsController(IConfiguration configuration, ActualizerContext db, IMemoryCache cache) {
            _configuration = configuration;
            _db = db;
            _cache = cache;
        }


        [HttpGet]
        [Route("search")]
        [Produces("application/json")]
        [Authorize(Policy = "CanMakeAnalyticsRequests")]
        public async Task<ActionResult<ReturnJson>> GetSearchComments(string video_id, string search, string lang = "en", int count = 25) {
            //var uid = User.Claims.FirstOrDefault(c => c.Type == "uid")?.Value;
            //var role = await OktaClientHelper.GetProfileDetails(uid: uid).Result;
            //Console.WriteLine(userClaims.ToList());
            var data = _db.SavedObjects.OrderBy(o => o.VideoId).First();

            try {
                var results = await YoutubeAPI.SearchComments(video_id: video_id, search: search, lang: lang, count: count, youtube_key: _configuration["youtube_key"]);
                results.Cast<ReturnJson>();
                return Ok(results);
            } catch {
                return BadRequest("Please enter a video id");
            }

        }




        [HttpGet]
        [Route("bulk")]
        [Produces("application/json")]
        //TODO: Add to the database, the maximum call amount, such as 10 pages. I think 5,000 commnets is a good start. 
        public async Task<ActionResult<ReturnJson>> GetAsync(string video_id, string search, string nextPageToken) {

            if (!string.IsNullOrWhiteSpace(video_id)) {

                var cacheKey = string.Join(",", search, video_id);
                dynamic cached_object;

                if (_cache.TryGetValue(cacheKey, out cached_object)) {
                    return Ok(cached_object);
                }

                // get all the comments while they are less than the comment restriction i imposed. Use SQL lite for the restriction eventually. Certainly could be done per user. 
                List<ReturnJson> obj = new List<ReturnJson> { };

                string nextPageIdFromQuery = null;
                int allCommentCount = 0;
                int index = 0;

                do {
                    var result = await YoutubeAPI.GetCommentsNextPageAsync(
                        video_id: video_id, search: search,
                        NextPageToken: nextPageIdFromQuery, lastNumOfComments: allCommentCount,
                        youtube_key: _configuration["youtube_key"]);

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
                    index++;
                } while (!string.IsNullOrEmpty(nextPageIdFromQuery) && index < 5);

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

                cached_object = finalObject;

                var cacheEntryOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(500));
                _cache.Set(cacheKey, finalObject, cacheEntryOptions);

                return Ok(finalObject);


            }
            return BadRequest("Please enter a video id");
        }

    }

}
