using System;
using Microsoft.EntityFrameworkCore;

namespace OhmCalculatorApi.Abstractions
{
    public interface IDbBuilder
    {
        void Configure(DbContextOptionsBuilder optionsBuilder);
        void CreateModel(ModelBuilder modelBuilder);
    }
}
