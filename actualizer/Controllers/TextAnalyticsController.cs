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
using System.Diagnostics.Eventing.Reader;
using Microsoft.Extensions.Configuration;

namespace actualizer.Controllers {
    [Route("api/[controller]")]
    public class TextAnalyticsController : Controller {

        private readonly IConfiguration _configuration;

        public TextAnalyticsController(IConfiguration configuration) {
            _configuration = configuration;
        }





        // POST TextAnalytics/keyphrase
        [Route("keyphrases")]
        [HttpPost]
        public async Task<IList> PostKeyPhraseAsync([FromBody] DocsWithTime json) {

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
        public async Task<IList> PostSentimentAsync([FromBody] DocsWithTime json) {

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
                                        dayname = o.publishedAt.DayOfWeek.ToString(),
                                        month = o.publishedAt.Month.ToString()
                                    });


            return query.ToList();
        }


        // POST TextAnalytics/entities
        [Route("entities")]
        [HttpPost]
        public async Task<IList> GetEntities([FromBody] DocsWithTime json) {


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

            return query.ToList();
        }

    }
}
