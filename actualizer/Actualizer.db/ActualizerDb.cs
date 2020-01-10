using System;
using actualizer.Models;
using Microsoft.EntityFrameworkCore;

namespace actualizer.ActualizerDb {
    public class ActualizerDbContext : DbContext {
        public ActualizerDbContext(DbContextOptions<ActualizerDbContext> options)
       : base(options) { }

        public DbSet<RemainingRequests> RemainingRequests { get; set; }
    }
}
