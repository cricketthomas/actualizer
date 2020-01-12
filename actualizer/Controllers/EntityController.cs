using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using actualizer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Security.Claims;
using actualizer.Infastructure.Data.Actualizer.db;
using Microsoft.Extensions.Configuration;

namespace actualizer.Controllers {


    [Route("api/[controller]")]
    public class EntityController : Controller {

        private readonly ActualizerContext _db;

        private readonly IConfiguration _configuration;

        public EntityController(IConfiguration configuration, ActualizerContext db) {
            _configuration = configuration;
            _db = db;
        }




        // GET: api/values
        [HttpGet]
        public IEnumerable<string> Get() {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public IActionResult GetById(string id) {


            try {
                Console.WriteLine("Querying for a JSON Object");
                var data = _db.SavedObjects
                    .OrderBy(o => o.VideoId)
                    .First();

                return Ok(data.Object);
            } catch {
                return BadRequest("The ID specified cannot be found");
            }


        }

        // POST api/values

        [HttpPost]
        public void Post([FromBody] SaveObject value) {

            // Create
            string commentJson = JsonConvert.SerializeObject(value.Documents, Formatting.Indented);
            int JSONLength = value.Documents.Count;
            var uid = HttpContext.User;
            Console.WriteLine("Inserting a new post");
            Console.WriteLine(value);
            _db.Add(new SavedObjects {
                UserId = "",
                Object = commentJson,
                VideoId = value.metadata.video_id,
                Date = DateTime.Now,
                JSONLength = JSONLength,
                Source = value.metadata.search
            });
            _db.SaveChanges();
            Console.WriteLine("successful.");



        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value) {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id) {
        }
    }
}