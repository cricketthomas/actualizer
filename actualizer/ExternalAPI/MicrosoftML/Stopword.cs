using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ML;
using System.IO;
using System.Text.Json;

namespace actualizer.MicrosoftML.Stopword {
    public class Stopword {
        public Stopword() { }
        private class TextData {
            public string Text { get; set; }
        }

        private class TransformedTextData : TextData {
            public string[] WordsWithoutStopWords { get; set; }
        }


        static string stopwords_raw = File.ReadAllText("stopwords.json");
        static string[] stopwords = JsonSerializer.Deserialize<string[]>(stopwords_raw);
        static MLContext mlContext = new MLContext();
        static List<TextData> emptySamples = new List<TextData>();
        static IDataView emptyDataView = mlContext.Data.LoadFromEnumerable(emptySamples);

        public static string cleaner(string text) {

            var data = new TextData() {
                Text = text
            };

            var textPipeline = mlContext.Transforms.Text.TokenizeIntoWords("Words",
             "Text").Append(mlContext.Transforms.Text.RemoveStopWords(
             "WordsWithoutStopWords", "Words", stopwords: stopwords));

            var textTransformer = textPipeline.Fit(emptyDataView);

            var predictionEngine = mlContext.Model.CreatePredictionEngine<TextData,
                TransformedTextData>(textTransformer);

            var prediction = predictionEngine.Predict(data);


            if (prediction.WordsWithoutStopWords == null) {
                return string.Empty;
            }

            var cleanedText = string.Join(" ", prediction.WordsWithoutStopWords.Where(p => !string.IsNullOrEmpty(p)));

            return cleanedText;
        }
    }
}
