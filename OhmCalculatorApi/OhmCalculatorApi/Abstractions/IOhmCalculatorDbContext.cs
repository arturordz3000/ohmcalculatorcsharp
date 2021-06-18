using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using OhmCalculatorApi.Models;

namespace OhmCalculatorApi.Abstractions
{
    public interface IOhmCalculatorDbContext : IDisposable
    {
        public DbSet<Color> Colors { get; set; }
        public DbSet<ColorSelectorConfiguration> ColorSelectorConfigurations { get; set; }
        public DbSet<ResistorDefault> ResistorDefaults { get; set; }

        public DatabaseFacade Database { get; }
        public DbSet<TEntity> Set<TEntity>() where TEntity : class;
        public EntityEntry<TEntity> Entry<TEntity>([NotNull] TEntity entity) where TEntity : class;
        public int SaveChanges();
    }
}
