using Microsoft.EntityFrameworkCore;
using PruebaTecnicaTekus.Dtos;
using PruebaTecnicaTekus.Models;
using System.Diagnostics.Metrics;

namespace PruebaTecnicaTekus.Data
{
    public class TekusContext : DbContext
    {
        public TekusContext(DbContextOptions<TekusContext> options)
          : base(options) {
        }
        public DbSet<Provider> Providers { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<ProviderService> ProviderServices { get; set; }
        public DbSet<ServiceCountry> ServiceCountries { get; set; }
        public DbSet<CustomProviderField> CustomProviderFields { get; set; }
        public DbSet<ServicesByCountryDto> ServicesByCountryDtos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ServicesByCountryDto>().HasNoKey();
        }

    }
}
