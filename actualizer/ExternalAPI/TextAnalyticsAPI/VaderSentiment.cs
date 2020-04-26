using VaderSharp;
using actualizer.Models;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using actualizer.MicrosoftML.Stopword;

namespace actualizer.ExternalAPI.TextAnalyticsAPI.Vader {
    public class VaderSentiment {
        public VaderSentiment() {
        }
        private class TextData {
            public string Text { get; set; }
        }

        private class TransformedTextData : TextData {
            public string[] WordsWithoutStopWords { get; set; }
        }

        public static async Task<List<AzureSentiment>> VaderSentimentAnalytics(Docs json, string score_type, bool stopword) {



            SentimentIntensityAnalyzer analyzer = new SentimentIntensityAnalyzer();



            List<AzureSentiment> azureSentiments = new List<AzureSentiment> { };


            foreach (var item in json.documents) {

                string text = item.text;

                if (stopword) {
                    //if stops words, clean the text.
                    text = Stopword.cleaner(item.text);
                }

                var score = analyzer.PolarityScores(text);
                var type = new Hashtable {
                        { "compound", score.Compound },
                        { "neutral", score.Neutral },
                        { "positive", score.Positive },
                        { "negative", score.Negative }

                };

                azureSentiments.Add(new AzureSentiment { id = item.id, score = (double)type[score_type] });

            }

            return azureSentiments.ToList();
        }
    }
}
