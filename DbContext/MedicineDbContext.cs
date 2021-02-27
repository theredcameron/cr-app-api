using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Sqlite;

using api.Models;
using api.Utilities;

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

            modelBuilder.Entity<User>().HasData(new User{
                UserId = 1,
                UserName = "admin",
                Password = Crypto.Encrypt("12345"),
                FirstName = "admin",
                LastName = "user",
                CreatedDate = DateTime.Now
            });

            modelBuilder.Entity<Privilege>().HasData(new Privilege{
                Id = 1,
                Name = "Admin",
                Description = "Administrator"
            });

            modelBuilder.Entity<Privilege>().HasData(new Privilege{
                Id = 2,
                Name = "View",
                Description = "View Medicines and Ledgers"
            });

            modelBuilder.Entity<Privilege>().HasData(new Privilege{
                Id = 3,
                Name = "Edit",
                Description = "Create/Edit Medicines and Ledgers"
            });

            modelBuilder.Entity<UserPrivilege>(e => {
                e.HasKey(l => new {l.PrivilegeId, l.UserId});
                e.HasData(new UserPrivilege{
                    UserId = 1,
                    PrivilegeId = 2
                });
            });
        }
    }
}