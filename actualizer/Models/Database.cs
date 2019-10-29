﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using actualizer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;



namespace actualizer.Models
{
    // This class/table stores the values called from the youtube API or returned json from the Azure API
    public class SavedObjects
    {
        [Key]
        public string VideoId { get; set; }
        public string UserId { get; set; }
        public string Object { get; set; }
        public DateTime Date { get; set; }
        public string Source { get; set; }
        public int JSONLength { get; set; }
    }


    public class RemainingRequests
    {
        [Key]
        public string Resource { get; set; }
        public int Count { get; set; }
        public DateTime Date { get; set; }

    }

    //This is the create fnction that acually holds the tables or creates the actual tables. 
    public class ActualizerContext : DbContext
    {

        public DbSet<SavedObjects> SavedObjects { get; set; }

        public DbSet<RemainingRequests> RemainingRequests { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseSqlite("Data Source=Data.db");
    }






    // This is just a class that specifies the input object. 
    public class SaveObject
    {
        public string VideoId { get; set; }
        public string UserId { get; set; }
        public string Source { get; set; }
        public List<Comments> Comments { get; set; }
    }

}