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
    public class PageInfo { 
      public int totalResults { get; set; }
     public int resultsPerPage { get; set; }
        
    }
}
