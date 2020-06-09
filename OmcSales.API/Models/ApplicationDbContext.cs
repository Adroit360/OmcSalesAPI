using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OilBackend.Models;

namespace OmcSales.API.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions options):base(options)
        {
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductPrice> ProductPrices { get; set; }
        public DbSet<FillingStation> FillingStation { get; set; }
        public DbSet<NozzleValue> NozzleValue { get; set; }
        public DbSet<Nozzle> Nozzle { get; set; }
        public DbSet<Tank> Tank { get; set; }
        public DbSet<Pump> Pump { get; set; }
        public DbSet<ProductBank> ProductBank { get; set; }
    }
}
