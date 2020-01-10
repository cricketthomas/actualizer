using System;
using System.Collections.Generic;
using actualizer.Models;
using Microsoft.EntityFrameworkCore;

namespace actualizer.Infastructure.Data.Actualizer.db {
    public class ActualizerModels {
        public ActualizerModels() {
        }

        public class SavedObjects {
            public string VideoId { get; set; }
            public string UserId { get; set; }
            public string Object { get; set; }
            public DateTime Date { get; set; }
            public string Source { get; set; }
            public int JSONLength { get; set; }
        }


        public class RemainingRequests {
            public string Resource { get; set; }
            public int Count { get; set; }
            public DateTime Date { get; set; }

        }

        //This is the create fnction that acually holds the tables or creates the actual tables. 
        public class ActualizerContext : DbContext {
            public DbSet<SavedObjects> SavedObjects { get; set; }
            public DbSet<RemainingRequests> RemainingRequests { get; set; }

        }

        public class ActualizerDbContext : DbContext {

            public DbSet<SavedObjects> SavedObjects { get; set; }
            public DbSet<RemainingRequests> RemainingRequests { get; set; }

        }

        // This is just a class that specifies the input object. 
        public class SaveObject {
            public string VideoId { get; set; }
            public string UserId { get; set; }
            public string Source { get; set; }
            public List<Comments> Comments { get; set; }
        }




    }
}
