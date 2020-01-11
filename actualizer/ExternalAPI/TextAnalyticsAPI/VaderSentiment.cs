using System;
using VaderSharp;
using System.Net.Http;
using actualizer.Models;
using System.Collections;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ML;
using System.IO;

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

        public static List<AzureSentiment> VaderSentimentAnalytics(Docs json) {
            SentimentIntensityAnalyzer analyzer = new SentimentIntensityAnalyzer();
            //var stopwords_raw = File.ReadAllText("stopwords.json");
            //var stopwords = JsonConvert.DeserializeObject<string[]>(stopwords_raw);

            //var mlContext = new MLContext();
            //var emptySamples = new List<TextData>();
            //var emptyDataView = mlContext.Data.LoadFromEnumerable(emptySamples);


            //var textPipeline = mlContext.Transforms.Text.TokenizeIntoWords("Words",
            // "Text")
            // .Append(mlContext.Transforms.Text.RemoveStopWords(
            // "WordsWithoutStopWords", "Words", stopwords: stopwords));

            //var textTransformer = textPipeline.Fit(emptyDataView);

            //var predictionEngine = mlContext.Model.CreatePredictionEngine<TextData,
            //    TransformedTextData>(textTransformer);





            List<AzureSentiment> azureSentiments = new List<AzureSentiment> { };


            foreach (var item in json.documents) {

                //var data = new TextData() {
                //    Text = item.text
                //};
                //var prediction = predictionEngine.Predict(data);
                //var cleanedText = string.Join(" ", prediction.WordsWithoutStopWords);
                var score = analyzer.PolarityScores(item.text);
                //var score2 = analyzer.PolarityScores(cleanedText);

                Console.WriteLine("Positive score: " + score.Positive);
                Console.WriteLine("Negative score: " + score.Negative);
                Console.WriteLine("Neutral score: " + score.Neutral);
                Console.WriteLine("Compound score: " + score.Compound);
                azureSentiments.Add(new AzureSentiment { id = item.id, score = score.Compound });



            }

            return azureSentiments.ToList();
        }
    }
}
