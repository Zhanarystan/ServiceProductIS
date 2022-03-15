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

        DbSet<City> Cities { get; set; }
        DbSet<Establishment> Establishments { get; set; }
        DbSet<Manufacturer> Manufacturers { get; set; }
        DbSet<Product> Products { get; set; }
        DbSet<Service> Services { get; set; }
        DbSet<EstablishmentProduct> EstablishmentProduct { get; set; }
        DbSet<EstablishmentService> EstablishmentService { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Establishment>()
                .HasOne(e => e.City)
                .WithMany(c => c.Establishments)
                .OnDelete(DeleteBehavior.Cascade)
                .HasForeignKey(e => e.CityId);
        }
    }
}