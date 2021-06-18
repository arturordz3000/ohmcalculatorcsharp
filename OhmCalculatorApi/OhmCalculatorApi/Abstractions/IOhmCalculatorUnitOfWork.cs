using System;
using OhmCalculatorApi.Models;

namespace OhmCalculatorApi.Abstractions
{
    public interface IOhmCalculatorUnitOfWork : IDisposable
    {
        IRepository<Color> ColorsRepository { get; }
        IRepository<ColorSelectorConfiguration> ColorSelectorConfigurationsRepository { get; }
        IRepository<ResistorDefault> ResistorDefaultsRepository { get; }
        void Save();
    }
}
