using System;
using Microsoft.EntityFrameworkCore.Infrastructure;
using OhmCalculatorApi.Models;

namespace OhmCalculatorApi.Abstractions
{
    public interface IOhmCalculatorUnitOfWork : IDisposable
    {
        IRepository<Color> ColorsRepository { get; }
        IRepository<ColorSelectorConfiguration> ColorSelectorConfigurationsRepository { get; }
        IRepository<ResistorDefault> ResistorDefaultsRepository { get; }
        DatabaseFacade Database { get; }
        void Save();
    }
}
