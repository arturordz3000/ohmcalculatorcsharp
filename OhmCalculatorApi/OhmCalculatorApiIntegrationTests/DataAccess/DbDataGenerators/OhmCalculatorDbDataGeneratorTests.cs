using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OhmCalculatorApi.DataAccess;
using OhmCalculatorApi.DataAccess.DbBuilders;
using OhmCalculatorApi.DataAccess.DbDataGenerators;
using OhmCalculatorApi.Models;

namespace OhmCalculatorApiIntegrationTests.DataAccess.DbDataGenerators
{
    [TestClass]
    public class OhmCalculatorDbDataGeneratorTests
    {
        [TestMethod]
        public void Make_Sure_Generates_All_Data()
        {
            SqliteOhmCalculatorDbBuilder dbBuilder = new SqliteOhmCalculatorDbBuilder("integration.db");
            OhmCalculatorDbContext dbContext = new OhmCalculatorDbContext(dbBuilder);

            using (var unitOfWork = new OhmCalculatorUnitOfWork(dbContext))
            {
                dbContext.Database.EnsureDeleted();
                dbContext.Database.EnsureCreated();

                OhmCalculatorDbDataGenerator generator = new OhmCalculatorDbDataGenerator(unitOfWork);
                generator.Generate();

                var colors = unitOfWork.ColorsRepository.Get();
                var colorSelectorConfigurations = unitOfWork.ColorSelectorConfigurationsRepository.Get();
                var resistorDefaults = unitOfWork.ResistorDefaultsRepository.Get();

                Assert.AreEqual(28, colors.Count);
                Assert.AreEqual(4, colorSelectorConfigurations.Count);
                Assert.AreEqual(4, resistorDefaults.Count);
                AssertColorColorSelectorConfigurations(colorSelectorConfigurations);
            }
        }

        private void AssertColorColorSelectorConfigurations(IList<ColorSelectorConfiguration> colorSelectorConfigurations)
        {
            Assert.AreEqual(10, colorSelectorConfigurations.First(cofiguration => cofiguration.Name == "First Band").Colors.Count);
            Assert.AreEqual(10, colorSelectorConfigurations.First(cofiguration => cofiguration.Name == "Second Band").Colors.Count);
            Assert.AreEqual(10, colorSelectorConfigurations.First(cofiguration => cofiguration.Name == "Multiplier").Colors.Count);
            Assert.AreEqual(8, colorSelectorConfigurations.First(cofiguration => cofiguration.Name == "Tolerance").Colors.Count);
        }
    }
}
