using System;
using Microsoft.EntityFrameworkCore;
using OhmCalculatorApi.Abstractions;
using OhmCalculatorApi.Models;

namespace OhmCalculatorApi.DataAccess
{
    public class OhmCalculatorDbContext : DbContext, IOhmCalculatorDbContext
    {
        private readonly IDbBuilder dbBuilder;

        public OhmCalculatorDbContext(IDbBuilder dbBuilder)
        {
            this.dbBuilder = dbBuilder;
        }

        public DbSet<Color> Colors { get; set; }
        public DbSet<ColorSelectorConfiguration> ColorSelectorConfigurations { get; set; }
        public DbSet<ResistorDefault> ResistorDefaults { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            dbBuilder.Configure(optionsBuilder);
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            dbBuilder.CreateModel(modelBuilder);
            base.OnModelCreating(modelBuilder);
        }
    }
}
