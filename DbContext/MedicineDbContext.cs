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
        public DbSet<User> Users {get;set;}
        public DbSet<Privilege> Privileges {get;set;}
        public DbSet<UserPrivilege> UserPrivileges {get;set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<Medicine>().ToTable("Medicine");
            modelBuilder.Entity<Ledger>().ToTable("Ledger");
            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<Privilege>().ToTable("Privilege");
            modelBuilder.Entity<UserPrivilege>().HasKey(up => new {up.PrivilegeId, up.UserId});
        }
    }
}