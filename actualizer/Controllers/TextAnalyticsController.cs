using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using actualizer.ExternalAPI.TextAnalytics;
using actualizer.ExternalAPI.TextAnalyticsAPI.Vader;
using actualizer.Infastructure.Data.Actualizer.db;
using actualizer.MicrosoftML.Stopword;
using actualizer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.ML.Transforms.Text;

namespace actualizer.Controllers {
    [Route("api/[controller]")]
    public class TextAnalyticsController : Controller {

        private readonly IConfiguration _configuration;
        private readonly IMemoryCache _cache;
        private readonly ActualizerContext _db;

        public TextAnalyticsController(IConfiguration configuration, IMemoryCache cache, ActualizerContext db) {
            _configuration = configuration;
            _cache = cache;
            _db = db;
        }


        // POST TextAnalytics/keyphrase
        [Route("keyphrases")]
        [HttpPost]
        [Authorize(Policy = "CanMakeAnalyticsRequests")]
        public async Task<ActionResult<IList>> PostKeyPhraseAsync([FromBody] DocsWithTime json) {

            Docs jsonDoc = JsonSerializer.Deserialize<Docs>(JsonSerializer.Serialize(json));

            //string result = await CallTextAnalyticsAPI(jsonDoc);

            string result = await TextAnalticsAPI.CallTextAnalyticsAPI(json: jsonDoc, RequestType: "keyphrases", azure_key: _configuration["azure_key"]);


            TextAnalytics textanalyticsresponse = JsonSerializer.Deserialize<TextAnalytics>(result);

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
        [Authorize(Policy = "CanMakeAnalyticsRequests")]
        public async Task<ActionResult<IList>> PostSentimentAsync([FromBody] DocsWithTime json) {

            Docs jsonDoc = JsonSerializer.Deserialize<Docs>(JsonSerializer.Serialize(json));
            string result = await TextAnalticsAPI.CallTextAnalyticsAPI(json: jsonDoc, RequestType: "sentiment", azure_key: _configuration["azure_key"]);
            //string result = await CallTextAnalyticsAPI(jsonDoc);
            Sentiment sentimentresponse = JsonSerializer.Deserialize<Sentiment>(result);
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
            }).OrderBy(o => o.Day);

            var MonthAggregate = query.GroupBy(d => d.month).Select(g => new {
                Month = Int32.Parse(g.Key),
                Scores = g.Select(s => s.score),
                Avg = (g.Sum(g => g.score) / g.Select(s => s.score).Count())
            }).OrderByDescending(o => o.Month);

            var LikeAggregate = query.GroupBy(l => l.likeCount).Select(s => new {
                LikeCount = s.Key,
                Scores = s.Select(s => s.score)
            }).OrderBy(o => o.LikeCount);//does this make sense?

            return Ok(new { textanalyticsbase = query.ToList(), MonthAggregate = MonthAggregate.ToList(), DayAggregate = DayAggregate.ToList(), LikeAggregate = LikeAggregate.ToList() });
        }


        // POST TextAnalytics/entities

        [Route("entities")]
        [HttpPost]
        [Authorize(Policy = "CanMakeAnalyticsRequests")]
        public async Task<ActionResult<IList>> GetEntities([FromBody] DocsWithTime json) {


            Docs jsonDoc = JsonSerializer.Deserialize<Docs>(JsonSerializer.Serialize(json));
            string result = await TextAnalticsAPI.CallTextAnalyticsAPI(json: jsonDoc, RequestType: "entities", azure_key: _configuration["azure_key"]);

            Entities entityresponse = JsonSerializer.Deserialize<Entities>(result);

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
        [Route("actualizer/{score_type}/{stopword}")]
        [HttpPost]
        public async Task<ActionResult<IList>> PostActualizerSentimentAsync([FromBody] DocsWithTime json, string score_type = "compound", bool stopword = false) {
            if (null != json) {


                Docs jsonDoc = JsonSerializer.Deserialize<Docs>(JsonSerializer.Serialize(json));

                var vaders = await VaderSentiment.VaderSentimentAnalytics(json: jsonDoc, score_type: score_type, stopword: stopword);

                var srm = await _db.SearchResultsMetadata.FirstAsync();
                srm.sentimentAPIRequests = srm.sentimentAPIRequests + jsonDoc.documents.Count;
                await _db.SaveChangesAsync();

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
                    //SentimentScore = g.Select(s => s.score),
                    Avg = (g.Sum(g => g.score) / g.Select(s => s.score).Count())
                });

                var MonthAggregate = query.GroupBy(d => d.month).Select(g => new {
                    Month = g.Key,
                    // SentimentScores = g.Select(s => s.score),
                    Avg = (g.Sum(g => g.score) / g.Select(s => s.score).Count())
                });

                var LikeAggregate = query.Where(l => l.likeCount > 200)
                    .GroupBy(l => l.likeCount)
                    .Select(s => new {
                        LikeCount = s.Key,
                        Avg = (s.Sum(s => s.score) / s.Select(s => s.score).Count())
                    }).OrderBy(l => l.LikeCount);

                //buckets group by for scatter plot


                //time series chart for comments per day

                var hourAggregate = query.GroupBy(d => d.date.Hour).Select(g => new {
                    Hour = g.Key,
                    Avg = (g.Sum(g => g.score) / g.Select(s => s.score).Count()),
                    Count = g.Select(s => s.id).Count()
                }).OrderBy(h => h.Hour);


                return Ok(
                    new {
                        textanalyticsbase = query.OrderBy(q => q.date).ToList(),
                        MonthAggregate,
                        DayAggregate,
                        LikeAggregate,
                        hourAggregate
                    });
            }
            return BadRequest("json document is null");
        }


        [Route("actualizer/keyphrases")]
        [HttpPost]
        public ActionResult<IList> PostActualizerKeyPrhases([FromBody] DocsWithTime json, int wordthreshold = 1, bool stopword = false) {

            Docs jsonDoc = JsonSerializer.Deserialize<Docs>(JsonSerializer.Serialize(json));

            IList<string> keywordsList;



            if (stopword) {
                keywordsList = jsonDoc.documents.Select(s => Stopword.cleaner(s.text)).ToList();
            } else {
                keywordsList = jsonDoc.documents.Select(s => s.text).ToList();
            }


            List<string> keywordsLake = new List<string> { };

            var keywordArray = String.Join(",", keywordsList).ToLowerInvariant().Split(' ');

            foreach (var sentence in keywordArray) {
                var splitSentence = sentence.Split(",").ToArray();
                keywordsLake.AddRange(splitSentence);
            }


            var keywords = keywordsLake.GroupBy(x => x)
                .Where(g => g.Count() >= wordthreshold)
                .Select(y => new { word = y.Key, count = y.Count() }).OrderBy(o => o.count).Where(w => !string.IsNullOrWhiteSpace(w.word)).ToList();

            var srm = _db.SearchResultsMetadata.First();
            srm.keywordsExtracted = srm.keywordsExtracted + keywords.Count;
            _db.SaveChangesAsync();

            return keywords;
        }
    }
}
