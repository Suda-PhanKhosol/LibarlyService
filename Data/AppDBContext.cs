using Microsoft.EntityFrameworkCore;
using NetCoreAPI_Template_v3_with_auth.Models;
using System;
using System.Collections.Generic;

namespace NetCoreAPI_Template_v3_with_auth.Data
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserRole>().HasKey(x => new { x.UserId, x.RoleId });

            SeedData(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>()
                .HasData(new List<Role>()
               {
                    new Role(){ Id = Guid.NewGuid(), Name = "user"},
                    new Role(){ Id = Guid.NewGuid(), Name = "Manager"},
                    new Role(){ Id = Guid.NewGuid(), Name = "Admin"},
                    new Role(){ Id = Guid.NewGuid(), Name = "Developer"}
               });
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<UserRole> UserRoles { get; set; }

        public DbSet<Admin> Admins { get; set; }
        public DbSet<Book> Books { get; set; }

        public DbSet<BorrowBook> BorrowBooks { get; set; }

        public DbSet<CategoryBook> CategoryBooks { get; set; }

        public DbSet<Customer> Customers { get; set; }


    }
}