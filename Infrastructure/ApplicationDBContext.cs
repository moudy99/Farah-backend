using Core.Entities;
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationUser>()
                .HasDiscriminator<string>("Discriminator")
                .HasValue<Customer>("Customer")
                .HasValue<Owner>("Owner");

            modelBuilder.Entity<Owner>(entity =>
            {
                entity.Property(o => o.IDFrontImage).IsRequired();
                entity.Property(o => o.IDBackImage).IsRequired();
                entity.Property(o => o.UserType).IsRequired();
                entity.Property(o => o.IsApproved).HasDefaultValue(false);
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                // configuration for Customer can go here
            });

        }

    }
}
