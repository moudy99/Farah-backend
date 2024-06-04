using Application.Helpers;
using Core.Entities;
using Core.Enums;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

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
        public DbSet<Service> Services { get; set; }
        public DbSet<Hall> Halls { get; set; }
        public DbSet<BeautyCenter> BeautyCenters { get; set; }
        public DbSet<ServiceForBeautyCenter> servicesForBeautyCenter { get; set; }
        public DbSet<Review> Reviews { get; set; }

        public DbSet<Photography> Photographies { get; set; }
        public DbSet<ShopDresses> ShopDresses { get; set; }
        public DbSet<Dress> Dresses { get; set; }




        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<Owner>(entity =>
            {
                entity.Property(o => o.IDFrontImage).IsRequired();
                entity.Property(o => o.IDBackImage).IsRequired();
                entity.Property(o => o.UserType).IsRequired();
                entity.Property(o => o.AccountStatus).HasDefaultValue(OwnerAccountStatus.Pending);
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                // configuration for Customer can go here
            });




            modelBuilder.Entity<BeautyCenter>()
           .HasMany(b => b.servicesForBeautyCenter)
           .WithOne(s => s.BeautyCenter)
           .HasForeignKey(s => s.BeautyCenterId);

            modelBuilder.Entity<Hall>()
                .ToTable("Halls")
                .HasBaseType<Service>();

            modelBuilder.Entity<Photography>()
                        .ToTable("Photograph")
                        .HasBaseType<Service>();

            modelBuilder.Entity<Car>()
                        .ToTable("Cars")
                        .HasBaseType<Service>();

            modelBuilder.Entity<BeautyCenter>()
                        .ToTable("BeautyCenters")
                        .HasBaseType<Service>();

            modelBuilder.Entity<ShopDresses>()
                        .ToTable("ShopDresses")
                        .HasBaseType<Service>();

            // Configure Owner
            modelBuilder.Entity<Owner>()
                .ToTable("Owners") // Table for Owner entity
                .HasBaseType<ApplicationUser>();

            // Configure Customer
            modelBuilder.Entity<Customer>()
                .ToTable("Customers") // Table for Customer entity
                .HasBaseType<ApplicationUser>();

            modelBuilder.Entity<BeautyCenter>()
                .HasMany(b => b.Reviews)
                .WithOne(r => r.BeautyCenter)
                .HasForeignKey(r => r.BeautyCenterId);

            modelBuilder.Entity<BeautyCenter>()
          .HasMany(b => b.servicesForBeautyCenter)
          .WithOne(s => s.BeautyCenter)
          .HasForeignKey(s => s.BeautyCenterId);



            modelBuilder.Entity<Service>()
                        .HasOne(s => s.Owner)
                        .WithMany(o => o.Services)
                        .HasForeignKey(s => s.OwnerID)
                        .OnDelete(DeleteBehavior.Restrict);
        }

    }

}

