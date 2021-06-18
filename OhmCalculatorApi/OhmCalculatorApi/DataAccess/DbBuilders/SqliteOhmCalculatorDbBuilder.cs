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
            });

            modelBuilder.Entity<ColorSelectorConfiguration>().ToTable("ColorSelectorConfigurations");
            modelBuilder.Entity<ColorSelectorConfiguration>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasMany(e => e.Colors);
            });

            modelBuilder.Entity<ResistorDefault>().ToTable("ResistorDefaults");
            modelBuilder.Entity<ResistorDefault>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasOne(e => e.Color);
            });
        }
    }
}
