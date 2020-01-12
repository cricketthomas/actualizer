using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using actualizer.Models;
using Newtonsoft.Json;
using System.Linq;
using System.Collections;
using actualizer.ExternalAPI.TextAnalytics;
using actualizer.ExternalAPI.TextAnalyticsAPI.Vader;
using System.Diagnostics.Eventing.Reader;
using Microsoft.Extensions.Configuration;
using System.Globalization;
using Microsoft.Extensions.Caching.Memory;

namespace actualizer.Controllers {
    [Route("api/[controller]")]
    public class TextAnalyticsController : Controller {

        private readonly IConfiguration _configuration;
        private readonly IMemoryCache _cache;

        public TextAnalyticsController(IConfiguration configuration, IMemoryCache cache) {
            _configuration = configuration;
            _cache = cache;
        }





        // POST TextAnalytics/keyphrase
        [Route("keyphrases")]
        [HttpPost]
        public async Task<ActionResult<IList>> PostKeyPhraseAsync([FromBody] DocsWithTime json) {

            Docs jsonDoc = JsonConvert.DeserializeObject<Docs>(JsonConvert.SerializeObject(json));

            //string result = await CallTextAnalyticsAPI(jsonDoc);

            string result = await TextAnalticsAPI.CallTextAnalyticsAPI(json: jsonDoc, RequestType: "keyphrases", azure_key: _configuration["azure_key"]);


            TextAnalytics textanalyticsresponse = JsonConvert.DeserializeObject<TextAnalytics>(result);

            var p = textanalyticsresponse.documents; //.GroupBy(i => i.keyPhrases);

            var allphrases = p.SelectMany(s => s.keyPhrases).ToList();

            var allPhrasesCount = allphrases.GroupBy(x => x)
                .Where(g => g.Count() > 1)
                .Select(y => new { word = y.Key, count = y.Count() })
                .ToList();

            return allPhrasesCount.ToList();
        }


        // POST TextAnalytics/sentiment
        [Route("sentiment")]
        [HttpPost]
        public async Task<ActionResult<IList>> PostSentimentAsync([FromBody] DocsWithTime json) {

            Docs jsonDoc = JsonConvert.DeserializeObject<Docs>(JsonConvert.SerializeObject(json));
            string result = await TextAnalticsAPI.CallTextAnalyticsAPI(json: jsonDoc, RequestType: "sentiment", azure_key: _configuration["azure_key"]);
            //string result = await CallTextAnalyticsAPI(jsonDoc);
            Sentiment sentimentresponse = JsonConvert.DeserializeObject<Sentiment>(result);
            var sentimentscores = sentimentresponse.documents;
            var originaldocument = json.documents;

            var query = sentimentscores.Join(originaldocument,
                                    s => s.id,
                                    o => o.id,
                                    (s, o) => new {
                                        id = s.id,
                                        text = o.text,
                                        score = s.score,
                                        likeCount = o.likeCount,
                                        date = o.publishedAt,
                                        dayName = o.publishedAt.DayOfWeek.ToString(),
                                        month = o.publishedAt.Month.ToString()
                                    });

            var DayAggregate = query.GroupBy(d => d.dayName).Select(g => new {
                Day = g.Key,
                Scores = g.Select(s => s.score),
                Avg = (g.Sum(g => g.score) / g.Select(s => s.score).Count())
            });

            var MonthAggregate = query.GroupBy(d => d.month).Select(g => new {
                Month = g.Key,
                SentimentScores = g.Select(s => s.score),
                Avg = (g.Sum(g => g.score) / g.Select(s => s.score).Count())
            });

            var LikeAggregate = query.GroupBy(l => l.likeCount).Select(s => new {
                LikeCount = s.Key,
                SentimentScore = s.Select(s => s.score)
            });//does this make sense?

            return Ok(new { textanalyticsbase = query.ToList(), MonthAggregate = MonthAggregate.ToList(), DayAggregate = DayAggregate.ToList(), LikeAggregate = LikeAggregate.ToList() });
        }


        // POST TextAnalytics/entities
        [Route("entities")]
        [HttpPost]
        public async Task<ActionResult<IList>> GetEntities([FromBody] DocsWithTime json) {


            Docs jsonDoc = JsonConvert.DeserializeObject<Docs>(JsonConvert.SerializeObject(json));
            string result = await TextAnalticsAPI.CallTextAnalyticsAPI(json: jsonDoc, RequestType: "entities", azure_key: _configuration["azure_key"]);

            Entities entityresponse = JsonConvert.DeserializeObject<Entities>(result);

            var entitiesmatches = entityresponse.documents;
            var originaldocument = json.documents;

            var query = entitiesmatches.Join(originaldocument,
                            e => e.id,
                            o => o.id,
                            (e, o) => new {
                                id = e.id,
                                text = o.text,
                                likeCount = o.likeCount,
                                date = o.publishedAt,
                                entities = e.entities
                            });

            return Ok(query.ToList());
        }




        // POST TextAnalytics/sentiment
        [Route("vader/{score_type}/{stopword}")]
        [HttpPost]
        public async Task<ActionResult<IList>> PostVaderAsync([FromBody] DocsWithTime json, string score_type = "compound", bool stopword = false) {

            Docs jsonDoc = JsonConvert.DeserializeObject<Docs>(JsonConvert.SerializeObject(json));

            var vaders = await VaderSentiment.VaderSentimentAnalytics(json: jsonDoc, score_type: score_type, stopword: stopword);

            var originaldocument = json.documents;

            var query = vaders.Join(originaldocument,
                                    s => s.id,
                                    o => o.id,
                                    (s, o) => new {
                                        id = s.id,
                                        text = o.text,
                                        score = s.score,
                                        likeCount = o.likeCount,
                                        date = o.publishedAt,
                                        dayName = o.publishedAt.DayOfWeek.ToString(),
                                        month = o.publishedAt.Month.ToString()
                                    });

            var DayAggregate = query.GroupBy(d => d.dayName).Select(g => new {
                Day = g.Key,
                Scores = g.Select(s => s.score),
                Avg = (g.Sum(g => g.score) / g.Select(s => s.score).Count())
            });

            var MonthAggregate = query.GroupBy(d => d.month).Select(g => new {
                Month = g.Key,
                SentimentScores = g.Select(s => s.score),
                Avg = (g.Sum(g => g.score) / g.Select(s => s.score).Count())
            });

            var LikeAggregate = query.GroupBy(l => l.likeCount).Select(s => new {
                LikeCount = s.Key,
                SentimentScore = s.Select(s => s.score),
                Avg = (s.Sum(s => s.score) / s.Select(s => s.score).Count())
            });//does this make sense?

            return Ok(new { textanalyticsbase = query.ToList(), MonthAggregate = MonthAggregate.ToList(), DayAggregate = DayAggregate.ToList(), LikeAggregate = LikeAggregate.ToList() });
        }
    }
}
