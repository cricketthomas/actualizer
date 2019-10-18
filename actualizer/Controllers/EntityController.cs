using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using actualizer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace actualizer.Controllers
{


    public class ActualizerContext : DbContext
    {

        public DbSet<SavedObjects> SavedObjects { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseSqlite("Data Source=Data.db");

        // protected override void OnConfiguring(DbContextOptionsBuilder options)
        //=> options.UseSqlite("Data Source=comments.db");
        //}

    }


    public class SavedObjects
    {
        [Key]
        public string VideoId { get; set; }
        public string UserId { get; set; }
        public string Object { get; set; }
        public DateTime Date { get; set; }
    }

    public class SaveObject
    {
        public string VideoId { get; set; }
        public string UserId { get; set; }
        public List<Comments> Comments { get; set; }
    }




    [Route("api/[controller]")]
    public class EntityController : Controller
    {
        // GET: api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public IActionResult GetById(string id)
        {
            using (var db = new ActualizerContext())
            {


                try
                {
                    Console.WriteLine("Querying for a JSON Object");
                    var data = db.SavedObjects
                        .OrderBy(o => o.VideoId)
                        .First();

                    return Ok(data.Object);
                }
                catch
                {
                    return BadRequest("The ID specified cannot be found");
                }
            }

        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] SaveObject value)
        {

            using (var db = new ActualizerContext())
            {
                // Create
                string commentJson = JsonConvert.SerializeObject(value.Comments, Formatting.Indented);
                Console.WriteLine("Inserting a new post");
                Console.WriteLine(value);
                db.Add(new SavedObjects { UserId = value.UserId, Object = commentJson, VideoId = value.VideoId, Date = DateTime.Now });
                db.SaveChanges();
                Console.WriteLine("successful.");


            }
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}