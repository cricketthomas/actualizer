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
using actualizer.Utils;
namespace actualizer.Controllers {
    [Route("api/[controller]")]
    public class TextAnalyticsController : Controller {


        // POST TextAnalytics/keyphrase
        [Route("keyphrase")]
        [HttpPost]
        public async Task<IList> PostKeyPhraseAsync([FromBody] DocsWithTime json) {

            Docs jsonDoc = JsonConvert.DeserializeObject<Docs>(JsonConvert.SerializeObject(json));

            //string result = await CallTextAnalyticsAPI(jsonDoc);

            string result = await Helpers.CallTextAnalyticsAPI(json: jsonDoc, RequestType: "keyphrases");


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
            string result = await Helpers.CallTextAnalyticsAPI(json: jsonDoc, RequestType: "sentiment");
            //string result = await CallTextAnalyticsAPI(jsonDoc);
            Sentiment sentimentresponse = JsonConvert.DeserializeObject<Sentiment>(result);
            var sentimentscores = sentimentresponse.documents;
            var originaldocument = json.documents;

            var query = sentimentscores.Join(originaldocument,
                                    s => s.id,
                                    o => o.id,
                                    (s, o) => new { id = s.id, text = o.text, score = s.score, date = o.publishedAt });

            return query.ToList();
            //TODO sentiment works and joins well, now I need to aggregate it or something for sentiment overitme?
        }

    }
}
