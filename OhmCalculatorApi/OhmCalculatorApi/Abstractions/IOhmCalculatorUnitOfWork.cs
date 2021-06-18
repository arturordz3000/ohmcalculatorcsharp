using System;
using OhmCalculatorApi.Models;

namespace OhmCalculatorApi.Abstractions
{
    public interface IOhmCalculatorUnitOfWork
    {
        IRepository<Color> ColorsRepository { get; }
        IRepository<ColorSelectorConfiguration> ColorSelectorConfigurationsRepository { get; }
        IRepository<ResistorDefault> ResistorDefaultsRepository { get; }
        void Save();
    }
}
