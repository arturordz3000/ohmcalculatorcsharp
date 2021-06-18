using System;
using Microsoft.EntityFrameworkCore;
using OhmCalculatorApi.Models;

namespace OhmCalculatorApi.Abstractions
{
    public interface IOhmCalculatorDbContext
    {
        public DbSet<Color> Colors { get; set; }
        public DbSet<ColorSelectorConfiguration> ColorSelectorConfigurations { get; set; }
        public DbSet<ResistorDefault> ResistorDefaults { get; set; }
    }
}
