using System;
using Microsoft.EntityFrameworkCore;
using OhmCalculatorApi.Abstractions;
using OhmCalculatorApi.Models;

namespace OhmCalculatorApi.DataAccess.DbBuilders
{
    public class SqliteOhmCalculatorDbBuilder : IDbBuilder
    {
        private readonly string databaseFilename;

        public SqliteOhmCalculatorDbBuilder(string databaseFilename)
        {
            this.databaseFilename = databaseFilename;
        }

        public void Configure(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(connectionString: "Filename=" + databaseFilename);
        }

        public void CreateModel(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Color>().ToTable("Colors");
            modelBuilder.Entity<Color>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Rgb).IsRequired(true);
                entity.Property(e => e.ValueDescription).IsRequired(true);
                entity.Property(e => e.ValueNumber).IsRequired(true);
                entity.Property(e => e.ColorType).IsRequired(true);
            });

            modelBuilder.Entity<ColorSelectorConfiguration>().ToTable("ColorSelectorConfigurations");
            modelBuilder.Entity<ColorSelectorConfiguration>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired(true);
                entity.HasMany(e => e.Colors)
                    .WithMany(e => e.ColorSelectorConfigurations);
            });

            modelBuilder.Entity<ResistorDefault>().ToTable("ResistorDefaults");
            modelBuilder.Entity<ResistorDefault>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Position).IsRequired(true);
                entity.HasOne(e => e.Color);
            });
        }
    }
}
