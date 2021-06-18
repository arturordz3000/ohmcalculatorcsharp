using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OhmCalculatorApi.Abstractions;
using OhmCalculatorApi.DataAccess.DbDataGenerators;
using OhmCalculatorApi.Exceptions;
using OhmCalculatorApi.Models;

namespace OhmCalculatorApiIntegrationTests.DataAccess.DbDataGenerators
{
    [TestClass]
    public class OhmCalculatorDbDataGeneratorTests
    {
        [TestMethod]
        public void Make_Sure_Generates_All_Data()
        {
            Mock<IOhmCalculatorUnitOfWork> fakeUnitOfWork = new Mock<IOhmCalculatorUnitOfWork>();
            Mock<IRepository<Color>> fakeColorsRepository = new Mock<IRepository<Color>>();
            Mock<IRepository<ColorSelectorConfiguration>> fakeColorSelectorsRepository = new Mock<IRepository<ColorSelectorConfiguration>>();
            Mock<IRepository<ResistorDefault>> fakeResistorDefaultsRespository = new Mock<IRepository<ResistorDefault>>();

            fakeUnitOfWork.Setup(fake => fake.ColorsRepository).Returns(fakeColorsRepository.Object);
            fakeUnitOfWork.Setup(fake => fake.ColorSelectorConfigurationsRepository).Returns(fakeColorSelectorsRepository.Object);
            fakeUnitOfWork.Setup(fake => fake.ResistorDefaultsRepository).Returns(fakeResistorDefaultsRespository.Object);

            OhmCalculatorDbDataGenerator generator = new OhmCalculatorDbDataGenerator(fakeUnitOfWork.Object);

            generator.Generate();

            fakeColorsRepository.Verify(fake => fake.Insert(It.IsAny<Color>()), times: Times.Exactly(28));
            fakeColorSelectorsRepository.Verify(fake => fake.Insert(It.IsAny<ColorSelectorConfiguration>()), times: Times.Exactly(4));
            fakeResistorDefaultsRespository.Verify(fake => fake.Insert(It.IsAny<ResistorDefault>()), times: Times.Exactly(4));
        }
    }
}
