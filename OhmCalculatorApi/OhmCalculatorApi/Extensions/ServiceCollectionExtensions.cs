using System;
using Microsoft.Extensions.DependencyInjection;
using OhmCalculatorApi.Abstractions;
using OhmCalculatorApi.DataAccess;
using OhmCalculatorApi.DataAccess.DbBuilders;
using OhmCalculatorApi.DataAccess.DbDataGenerators;

namespace OhmCalculatorApi.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddDataAccessLayer(this IServiceCollection services)
        {
            services.AddScoped<IDbBuilder>((serviceProvider) => new SqliteOhmCalculatorDbBuilder("ohmcalculator.db"));
            services.AddScoped<IOhmCalculatorDbContext, OhmCalculatorDbContext>();
            services.AddScoped<IOhmCalculatorUnitOfWork, OhmCalculatorUnitOfWork>();
            services.AddScoped<IDbDataGenerator, OhmCalculatorDbDataGenerator>();
        }
    }
}
