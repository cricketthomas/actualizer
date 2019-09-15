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
namespace actualizer.Models
{
    public class Comments
    {

        public int id { get; set; }
        public string text { get; set; }
        public string language { get; set; }
    }


    public class ReturnJson
    {
        public string search { get; set; }
        public string url { get; set; }
        public string video_id { get; set; }
        public int count { get; set; }
        public Comments[] comments { get; set; }
        public string nextPage { get; set; } 
    }

    
}
