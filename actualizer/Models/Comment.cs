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

        //public static int count = 0;

        /*public Comments(int id,string text, string language) {

            Console.WriteLine($"comments called{id},{text},{language}");
            count++;
        }*/

        
    

    }
}
