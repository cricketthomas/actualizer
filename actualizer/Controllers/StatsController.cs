using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using actualizer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using actualizer.Infastructure.Data.Actualizer.db;
using Microsoft.Extensions.Configuration;
using System.Text.Json;
using System.Text.Json.Serialization;
using static actualizer.Models.ActualizerModels;
using Microsoft.AspNetCore.Routing;

namespace actualizer.Controllers {


    [Route("api/[controller]")]
    public class StatsController : Controller {

        private readonly ActualizerContext _db;

        private readonly IConfiguration _configuration;

        public StatsController(IConfiguration configuration, ActualizerContext db) {
            _configuration = configuration;
            _db = db;
        }


        // GET: api/values
        [HttpGet]
        public ActionResult<SearchResultsMetadata> GetRequest() {
            try {
                var data = _db.SearchResultsMetadata.FirstOrDefault();
                return Ok(data);
            } catch {
                return BadRequest();
            }
        }

    }
}

// GET api/values/5
//[HttpGet("{id}")]
//public IActionResult GetById(string id) {
//    //fix this

//    try {
//        Console.WriteLine("Querying for a JSON Object");
//        var data = _db.SavedObjects
//            //.OrderBy(o => o.VideoId)
//            .First();

//        return Ok(data.Object);
//    } catch {
//        return BadRequest("The ID specified cannot be found");
//    }


//}

//// POST api/values

//[HttpPost]
//public void Post([FromBody] SavedObjects value) {
//    // Create
//    var options = new JsonSerializerOptions {
//        WriteIndented = true,
//    };
//    string commentJson = JsonSerializer.Serialize(value.Documents, options);

//    int JSONLength = value.Documents.Count;
//    var uid = HttpContext.User;
//    Console.WriteLine("Inserting a new post");
//    Console.WriteLine(value);
//    _db.Add(new SavedObjects {
//        UserId = "",
//        Object = commentJson,
//        VideoId = value.metadata.video_id,
//        Date = DateTime.Now,
//        JSONLength = JSONLength,
//        Source = value.metadata.search
//    });
//    _db.SaveChanges();
//    Console.WriteLine("successful.");
//}

//// PUT api/values/5
//[HttpPut("{id}")]
//public void Put(int id, [FromBody]string value) {
//}