using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OhmCalculatorApi.Abstractions;
using OhmCalculatorApi.DataAccess;

namespace OhmCalculatorApiTests.DataAccess
{
    [TestClass]
    public class OhmCalculatorUnitOfWorkTests
    {
        [TestMethod]
        public void Can_Get_Colors_Repository()
        {
            Mock<IOhmCalculatorDbContext> fakeDbContext = new Mock<IOhmCalculatorDbContext>();

            var unitOfWork = new OhmCalculatorUnitOfWork(fakeDbContext.Object);

            var repository = unitOfWork.ColorsRepository;

            Assert.IsNotNull(repository);
        }

        [TestMethod]
        public void Can_Get_ColorSelectorConfigurations_Repository()
        {
            Mock<IOhmCalculatorDbContext> fakeDbContext = new Mock<IOhmCalculatorDbContext>();

            var unitOfWork = new OhmCalculatorUnitOfWork(fakeDbContext.Object);

            var repository = unitOfWork.ColorSelectorConfigurationsRepository;

            Assert.IsNotNull(repository);
        }

        [TestMethod]
        public void Can_Get_ResistorDefaults_Repository()
        {
            Mock<IOhmCalculatorDbContext> fakeDbContext = new Mock<IOhmCalculatorDbContext>();

            var unitOfWork = new OhmCalculatorUnitOfWork(fakeDbContext.Object);

            var repository = unitOfWork.ResistorDefaultsRepository;

            Assert.IsNotNull(repository);
        }

        [TestMethod]
        public void Can_Save_Changes()
        {
            Mock<IOhmCalculatorDbContext> fakeDbContext = new Mock<IOhmCalculatorDbContext>();

            var unitOfWork = new OhmCalculatorUnitOfWork(fakeDbContext.Object);
            unitOfWork.Save();

            fakeDbContext.Verify(fake => fake.SaveChanges());
        }
    }
}
