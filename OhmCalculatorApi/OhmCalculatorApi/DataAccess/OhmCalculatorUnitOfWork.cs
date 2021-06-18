using System;
using OhmCalculatorApi.Abstractions;
using OhmCalculatorApi.DataAccess.Repositories;
using OhmCalculatorApi.Models;

namespace OhmCalculatorApi.DataAccess
{
    public class OhmCalculatorUnitOfWork : IOhmCalculatorUnitOfWork
    {
        private readonly IOhmCalculatorDbContext dbContext;
        private IRepository<Color> colorsRepository;
        private IRepository<ColorSelectorConfiguration> colorSelectorConfigurationsRepository;
        private IRepository<ResistorDefault> resistorDefaultsRepository;

        public OhmCalculatorUnitOfWork(IOhmCalculatorDbContext dbContext)
        {
            this.dbContext = dbContext;

            if (dbContext.Database != null)
            {
                dbContext.Database.EnsureCreated();
            }
        }

        public IRepository<Color> ColorsRepository
        {
            get
            {
                if (colorsRepository is null)
                {
                    colorsRepository = new OhmCalculatorRepository<Color>(dbContext);
                }

                return colorsRepository;
            }
        }

        public IRepository<ColorSelectorConfiguration> ColorSelectorConfigurationsRepository
        {
            get
            {
                if (colorSelectorConfigurationsRepository is null)
                {
                    colorSelectorConfigurationsRepository = new OhmCalculatorRepository<ColorSelectorConfiguration>(dbContext);
                }

                return colorSelectorConfigurationsRepository;
            }
        }

        public IRepository<ResistorDefault> ResistorDefaultsRepository
        {
            get
            {
                if (resistorDefaultsRepository is null)
                {
                    resistorDefaultsRepository = new OhmCalculatorRepository<ResistorDefault>(dbContext);
                }

                return resistorDefaultsRepository;
            }
        }

        public void Save()
        {
            dbContext.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    dbContext.Dispose();
                }
            }

            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
