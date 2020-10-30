using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Sqlite;

using api.Models;

namespace api.Contexts {
    public  class MedicineDbContext : DbContext {
        public MedicineDbContext(DbContextOptions<MedicineDbContext> options) : base(options){}
        public DbSet<Medicine> Medicines {get;set;}
        public DbSet<Ledger> Ledgers {get;set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<Medicine>().ToTable("Medicine");
            modelBuilder.Entity<Ledger>().ToTable("Ledger");
        }
    }
}