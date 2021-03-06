﻿using System;
using Microsoft.EntityFrameworkCore;
using actualizer.Models;
using static actualizer.Models.ActualizerModels;

namespace actualizer.Infastructure.Data.Actualizer.db {


    public class ActualizerContext : DbContext {
        public ActualizerContext(DbContextOptions<ActualizerContext> options)
       : base(options) { }

        public DbSet<SearchResultsMetadata> SearchResultsMetadata { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
           => optionsBuilder.UseSqlite("Data Source=Data.db");
    }

}
