using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using actualizer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace actualizer.Controllers
{


    public class ActualizerContext : DbContext
    {

        public DbSet<SaveComment> AllSavedComments { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseSqlite("Data Source=Data.db");

        // protected override void OnConfiguring(DbContextOptionsBuilder options)
        //=> options.UseSqlite("Data Source=comments.db");
        //}

    }


    public class SaveComment
    {

        [Key]
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
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] SaveComment value)
        {

            using (var db = new ActualizerContext())
            {
                // Create
                Console.WriteLine("Inserting a new post");
                Console.WriteLine(value);
                db.Add(new SaveComment { UserId = value.UserId, Comments = value.Comments, VideoId = value.VideoId });
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