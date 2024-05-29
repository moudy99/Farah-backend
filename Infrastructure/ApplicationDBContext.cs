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

        public DbSet<Hall> Halls { get; set; }
        public DbSet<BeautyCenter> BeautyCenters { get; set; }
        public DbSet<ServiceForBeautyCenter> Services { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Photography> Photographies { get; set; }
        public DbSet<Dresses> Dresses { get; set; }




        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure ApplicationUser
            modelBuilder.Entity<ApplicationUser>()
                .HasDiscriminator<string>("Discriminator")
                .HasValue<Customer>("Customer")
                .HasValue<Owner>("Owner");

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
           .HasMany(b => b.Services)
           .WithOne(s => s.BeautyCenter)
           .HasForeignKey(s => s.BeautyCenterId);

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
                .HasMany(b => b.Appointments)
                .WithOne(a => a.BeautyCenter)
                .HasForeignKey(a => a.BeautyCenterId);
        }

    }

}

