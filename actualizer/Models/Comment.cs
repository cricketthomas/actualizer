using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net.Http;
using System.Linq;
using System.Collections.Generic;

namespace actualizer.Models {
    public class Comments {
        public int id { get; set; }
        public string text { get; set; }
        public string language { get; set; }
        public DateTime publishedAt { get; set; }

    }


    public class ReturnJson {
        public string search { get; set; }
        public string url { get; set; }
        public string video_id { get; set; }
        public int count { get; set; }
        public Comments[] comments { get; set; }
        public string nextPage { get; set; }
    }

    // classes for sending the json to the 
    public class Document {
        public string language { get; set; }
        public int id { get; set; }
        public string text { get; set; }

    }

    public class Docs {
        public List<Document> documents { get; set; }
    }

    public class DocumentWithTime {
        public string language { get; set; }
        public int id { get; set; }
        public string text { get; set; }
        public DateTime publishedAt { get; set; }

    }

    public class DocsWithTime {
        public List<DocumentWithTime> documents { get; set; }
    }




    // Classes for the return object from Azure Text analytics
    public class AzureTextAnalytics {
        public int id { get; set; }
        public List<string> keyPhrases { get; set; }
    }

    public class TextAnalytics {
        public List<AzureTextAnalytics> documents { get; set; }
    }



    // Classes for the return object from Azure for sentment
    public class AzureSentiment {
        public int id { get; set; }
        public double score { get; set; }
    }

    public class Sentiment {
        public List<AzureSentiment> documents { get; set; }
    }










    //TODO: sentiment overtime.

}
