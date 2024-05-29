using Core.Entities;
using Core.Enums;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace Infrastructure
{
    public class ApplicationDBContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {
        }

        public DbSet<City> Cities { get; set; }
        public DbSet<Governorate> Governorates { get; set; }
        public DbSet<Owner> Owners { get; set; }
        public DbSet<Customer> Customers { get; set; }

        public DbSet<Hall> Halls { get; set; }
        public DbSet<BeautyCenter> BeautyCenters { get; set; }
        public DbSet<Photography> Photographies { get; set; }
        public DbSet<Dresses> Dresses { get; set; }




        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure ApplicationUser
            modelBuilder.Entity<ApplicationUser>()
                .ToTable("ApplicationUsers"); // This is the base table for ApplicationUser

            // Configure Owner
            modelBuilder.Entity<Owner>()
                .ToTable("Owners") // Table for Owner entity
                .HasBaseType<ApplicationUser>();

            // Configure Customer
            modelBuilder.Entity<Customer>()
                .ToTable("Customers") // Table for Customer entity
                .HasBaseType<ApplicationUser>();

        }

    }
}
