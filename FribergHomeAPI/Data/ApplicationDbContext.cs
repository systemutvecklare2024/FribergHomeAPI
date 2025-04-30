using FribergHomeAPI.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FribergHomeAPI.Data
{
    // Author: Christoffer
    public class ApplicationDbContext : IdentityDbContext<ApiUser>
    {
        public DbSet<Property> Properties { get; set; }
        public DbSet<RealEstateAgency> Agencies {get;set;}
        public DbSet<RealEstateAgent> Agents { get; set; }
        public DbSet<Muncipality> Muncipalities { get; set; }
        public DbSet<PropertyImage> PropertyImages { get; set; }

        public DbSet<Application> Applications { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) {}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Property
            modelBuilder.Entity<Property>()
                .HasOne(p => p.Address)
                .WithMany(a => a.Properties)
                .HasForeignKey(p => p.AddressId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Property>()
                .HasOne(p => p.Muncipality)
                .WithMany(m => m.Properties)
                .HasForeignKey(p => p.MuncipalityId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Property>()
                .HasOne(p => p.RealEstateAgent)
                .WithMany(a => a.Properties)
                .HasForeignKey(p => p.RealEstateAgentId)
                .OnDelete(DeleteBehavior.Restrict);

			//Co-Author: Glate
			modelBuilder.Entity<Property>()
                .HasMany(p => p.Images)
                .WithOne(i => i.Property)
                .HasForeignKey(i => i.PropertyId)
                .OnDelete(DeleteBehavior.Restrict);

            // RealEstateAgent
            modelBuilder.Entity<RealEstateAgent>()
                .HasOne(a => a.Agency)
                .WithMany(agency => agency.Agents)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<RealEstateAgency>()
                .HasMany(r => r.Applications)
                .WithOne(a => a.Agency)
                .HasForeignKey(a => a.AgencyId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
