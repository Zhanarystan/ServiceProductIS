using API.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class DataContext : IdentityDbContext<AppUser>
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<City> Cities { get; set; }
        public DbSet<Establishment> Establishments { get; set; }
        public DbSet<Manufacturer> Manufacturers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<EstablishmentProduct> EstablishmentProduct { get; set; }
        public DbSet<EstablishmentService> EstablishmentService { get; set; }
        public DbSet<Metric> Metrics { get; set; }
        public DbSet<Estimate> Estimates { get; set; }
        public DbSet<EstimateProduct> EstimateProduct { get; set; }
        public DbSet<EstimateService> EstimateService { get; set; }
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Product>()
                .HasOne(p => p.Metric)
                .WithMany(m => m.Products)
                .OnDelete(DeleteBehavior.ClientSetNull);
            
            builder.Entity<Service>()
                .HasOne(p => p.Metric)
                .WithMany(m => m.Services)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.Entity<Product>()
                .HasOne(p => p.Manufacturer)
                .WithMany(m => m.Products)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.Entity<Establishment>()
                .HasOne(e => e.City)
                .WithMany(c => c.Establishments)
                .OnDelete(DeleteBehavior.Cascade)
                .HasForeignKey(e => e.CityId);

            builder.Entity<EstablishmentProduct>(x => x.HasKey(ep => new {ep.EstablishmentId, ep.ProductId}));
            builder.Entity<EstablishmentService>(x => x.HasKey(es => new {es.EstablishmentId, es.ServiceId}));

            builder.Entity<EstablishmentProduct>()
                .HasOne(u => u.Establishment)
                .WithMany(a => a.Products)
                .HasForeignKey(ep => ep.EstablishmentId);

            builder.Entity<EstablishmentProduct>()
                .HasOne(u => u.Product)
                .WithMany(a => a.Establishments)
                .HasForeignKey(ep => ep.ProductId);
            
            builder.Entity<EstablishmentService>()
                .HasOne(u => u.Establishment)
                .WithMany(a => a.Services)
                .HasForeignKey(ep => ep.EstablishmentId);

            builder.Entity<EstablishmentService>()
                .HasOne(u => u.Service)
                .WithMany(a => a.Establishments)
                .HasForeignKey(ep => ep.ServiceId);
            
            builder.Entity<EstimateProduct>()
                .HasOne(u => u.Estimate)
                .WithMany(a => a.Products)
                .HasForeignKey(ep => ep.EstimateId);

            builder.Entity<EstimateService>()
                .HasOne(u => u.Estimate)
                .WithMany(a => a.Services)
                .HasForeignKey(ep => ep.EstimateId);
            
            builder.Entity<Estimate>() 
                .HasOne(u => u.Establishment)
                .WithMany(a => a.Estimates)
                .HasForeignKey(e => e.EstablishmentId);

            builder.Entity<Estimate>() 
                .HasOne(u => u.CreatedBy)
                .WithMany(a => a.Estimates)
                .HasForeignKey(e => e.CreatedById);
        }
    }
}