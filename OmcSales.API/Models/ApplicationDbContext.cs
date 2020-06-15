using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace OmcSales.API.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductPrice> ProductPrices { get; set; }
        public DbSet<FillingStation> FillingStations { get; set; }
        public DbSet<NozzleValue> NozzleValues { get; set; }
        public DbSet<Nozzle> Nozzles { get; set; }
        public DbSet<Tank> Tanks { get; set; }
        public DbSet<TankValue> TankValues{get;set;}
        public DbSet<Pump> Pumps { get; set; }
        public DbSet<ProductBank> ProductBanks { get; set; }
        public DbSet<ProductStation> ProductStations { get; set; }
    }
}
