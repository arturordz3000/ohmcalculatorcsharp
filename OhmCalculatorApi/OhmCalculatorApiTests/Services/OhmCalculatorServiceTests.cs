using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OhmCalculatorApi.Abstractions;
using OhmCalculatorApi.Models;
using OhmCalculatorApi.Services;

namespace OhmCalculatorApiTests.Services
{
    [TestClass]
    public class OhmCalculatorServiceTests
    {
        [TestMethod]
        public void Calculate_Returns_Result_In_Ohms()
        {
            Mock<IOhmCalculatorUnitOfWork> fakeUnitOfWork = new Mock<IOhmCalculatorUnitOfWork>();
            Mock<IRepository<Color>> fakeColorsRepository = new Mock<IRepository<Color>>();
            List<Color> fakeColors = new List<Color>(new Color[]
            {
                new Color { Id = 1, ColorType = ColorType.Value, ValueNumber = 1 },
                new Color { Id = 2, ColorType = ColorType.Value, ValueNumber = 2 },
                new Color { Id = 3, ColorType = ColorType.Multiplier, ValueNumber = 10 },
                new Color { Id = 4, ColorType = ColorType.Tolerance, ValueNumber = 1 },
            });

            fakeColorsRepository.Setup(fake => fake.Get(null, null, "")).Returns(fakeColors);
            fakeUnitOfWork.Setup(fake => fake.ColorsRepository).Returns(fakeColorsRepository.Object);

            OhmCalculatorService service = new OhmCalculatorService(fakeUnitOfWork.Object);

            string result = service.Calculate(2, 1, 3, 4);

            Assert.AreEqual("210 Ohms ±1%", result);
        }

        [TestMethod]
        public void Calculate_Returns_Result_In_Kilo_Ohms()
        {
            Mock<IOhmCalculatorUnitOfWork> fakeUnitOfWork = new Mock<IOhmCalculatorUnitOfWork>();
            Mock<IRepository<Color>> fakeColorsRepository = new Mock<IRepository<Color>>();
            List<Color> fakeColors = new List<Color>(new Color[]
            {
                new Color { Id = 1, ColorType = ColorType.Value, ValueNumber = 1 },
                new Color { Id = 2, ColorType = ColorType.Value, ValueNumber = 2 },
                new Color { Id = 3, ColorType = ColorType.Multiplier, ValueNumber = 100 },
                new Color { Id = 4, ColorType = ColorType.Tolerance, ValueNumber = 5 },
            });

            fakeColorsRepository.Setup(fake => fake.Get(null, null, "")).Returns(fakeColors);
            fakeUnitOfWork.Setup(fake => fake.ColorsRepository).Returns(fakeColorsRepository.Object);

            OhmCalculatorService service = new OhmCalculatorService(fakeUnitOfWork.Object);

            string result = service.Calculate(2, 1, 3, 4);

            Assert.AreEqual("2.1K Ohms ±5%", result);
        }

        [TestMethod]
        public void Calculate_Returns_Result_In_Mega_Ohms()
        {
            Mock<IOhmCalculatorUnitOfWork> fakeUnitOfWork = new Mock<IOhmCalculatorUnitOfWork>();
            Mock<IRepository<Color>> fakeColorsRepository = new Mock<IRepository<Color>>();
            List<Color> fakeColors = new List<Color>(new Color[]
            {
                new Color { Id = 1, ColorType = ColorType.Value, ValueNumber = 1 },
                new Color { Id = 2, ColorType = ColorType.Value, ValueNumber = 2 },
                new Color { Id = 3, ColorType = ColorType.Multiplier, ValueNumber = 1e6 },
                new Color { Id = 4, ColorType = ColorType.Tolerance, ValueNumber = 3 },
            });

            fakeColorsRepository.Setup(fake => fake.Get(null, null, "")).Returns(fakeColors);
            fakeUnitOfWork.Setup(fake => fake.ColorsRepository).Returns(fakeColorsRepository.Object);

            OhmCalculatorService service = new OhmCalculatorService(fakeUnitOfWork.Object);

            string result = service.Calculate(2, 1, 3, 4);

            Assert.AreEqual("21M Ohms ±3%", result);
        }

        [TestMethod]
        public void Calculate_Returns_Result_In_Giga_Ohms()
        {
            Mock<IOhmCalculatorUnitOfWork> fakeUnitOfWork = new Mock<IOhmCalculatorUnitOfWork>();
            Mock<IRepository<Color>> fakeColorsRepository = new Mock<IRepository<Color>>();
            List<Color> fakeColors = new List<Color>(new Color[]
            {
                new Color { Id = 1, ColorType = ColorType.Value, ValueNumber = 1 },
                new Color { Id = 2, ColorType = ColorType.Value, ValueNumber = 2 },
                new Color { Id = 3, ColorType = ColorType.Multiplier, ValueNumber = 1e9 },
                new Color { Id = 4, ColorType = ColorType.Tolerance, ValueNumber = 1 },
            });

            fakeColorsRepository.Setup(fake => fake.Get(null, null, "")).Returns(fakeColors);
            fakeUnitOfWork.Setup(fake => fake.ColorsRepository).Returns(fakeColorsRepository.Object);

            OhmCalculatorService service = new OhmCalculatorService(fakeUnitOfWork.Object);

            string result = service.Calculate(2, 1, 3, 4);

            Assert.AreEqual("21G Ohms ±1%", result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Throw_Exception_When_FirstValueColorId_Does_Not_Exist()
        {
            Mock<IOhmCalculatorUnitOfWork> fakeUnitOfWork = new Mock<IOhmCalculatorUnitOfWork>();
            Mock<IRepository<Color>> fakeColorsRepository = new Mock<IRepository<Color>>();
            List<Color> fakeColors = new List<Color>(new Color[]
            {
                new Color { Id = 1, ColorType = ColorType.Value },
                new Color { Id = 2, ColorType = ColorType.Value },
                new Color { Id = 3, ColorType = ColorType.Multiplier },
                new Color { Id = 4, ColorType = ColorType.Tolerance },
            });

            fakeColorsRepository.Setup(fake => fake.Get(null, null, "")).Returns(fakeColors);
            fakeUnitOfWork.Setup(fake => fake.ColorsRepository).Returns(fakeColorsRepository.Object);

            OhmCalculatorService service = new OhmCalculatorService(fakeUnitOfWork.Object);

            // The first value does not exist in the colors list
            service.Calculate(5, 2, 3, 4);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Throw_Exception_When_FirstValueColorId_Exists_But_Is_Not_Value_ColorType()
        {
            Mock<IOhmCalculatorUnitOfWork> fakeUnitOfWork = new Mock<IOhmCalculatorUnitOfWork>();
            Mock<IRepository<Color>> fakeColorsRepository = new Mock<IRepository<Color>>();
            List<Color> fakeColors = new List<Color>(new Color[]
            {
                new Color { Id = 1, ColorType = ColorType.Value },
                new Color { Id = 2, ColorType = ColorType.Value },
                new Color { Id = 3, ColorType = ColorType.Multiplier },
                new Color { Id = 4, ColorType = ColorType.Tolerance },
            });

            fakeColorsRepository.Setup(fake => fake.Get(null, null, "")).Returns(fakeColors);
            fakeUnitOfWork.Setup(fake => fake.ColorsRepository).Returns(fakeColorsRepository.Object);

            OhmCalculatorService service = new OhmCalculatorService(fakeUnitOfWork.Object);

            // The first value exists in the colors list, but is not a value color type
            service.Calculate(3, 2, 3, 4);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Throw_Exception_When_SecondValueColorId_Does_Not_Exist()
        {
            Mock<IOhmCalculatorUnitOfWork> fakeUnitOfWork = new Mock<IOhmCalculatorUnitOfWork>();
            Mock<IRepository<Color>> fakeColorsRepository = new Mock<IRepository<Color>>();
            List<Color> fakeColors = new List<Color>(new Color[]
            {
                new Color { Id = 1, ColorType = ColorType.Value },
                new Color { Id = 2, ColorType = ColorType.Value },
                new Color { Id = 3, ColorType = ColorType.Multiplier },
                new Color { Id = 4, ColorType = ColorType.Tolerance },
            });

            fakeColorsRepository.Setup(fake => fake.Get(null, null, "")).Returns(fakeColors);
            fakeUnitOfWork.Setup(fake => fake.ColorsRepository).Returns(fakeColorsRepository.Object);

            OhmCalculatorService service = new OhmCalculatorService(fakeUnitOfWork.Object);

            // The second value does not exist in the colors list
            service.Calculate(1, 5, 3, 4);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Throw_Exception_When_SecondValueColorId_Exists_But_Is_Not_Value_ColorType()
        {
            Mock<IOhmCalculatorUnitOfWork> fakeUnitOfWork = new Mock<IOhmCalculatorUnitOfWork>();
            Mock<IRepository<Color>> fakeColorsRepository = new Mock<IRepository<Color>>();
            List<Color> fakeColors = new List<Color>(new Color[]
            {
                new Color { Id = 1, ColorType = ColorType.Value },
                new Color { Id = 2, ColorType = ColorType.Value },
                new Color { Id = 3, ColorType = ColorType.Multiplier },
                new Color { Id = 4, ColorType = ColorType.Tolerance },
            });

            fakeColorsRepository.Setup(fake => fake.Get(null, null, "")).Returns(fakeColors);
            fakeUnitOfWork.Setup(fake => fake.ColorsRepository).Returns(fakeColorsRepository.Object);

            OhmCalculatorService service = new OhmCalculatorService(fakeUnitOfWork.Object);

            // The second value exists in the colors list, but is not a value color type
            service.Calculate(1, 3, 3, 4);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Throw_Exception_When_MultiplerValueColorId_Does_Not_Exist()
        {
            Mock<IOhmCalculatorUnitOfWork> fakeUnitOfWork = new Mock<IOhmCalculatorUnitOfWork>();
            Mock<IRepository<Color>> fakeColorsRepository = new Mock<IRepository<Color>>();
            List<Color> fakeColors = new List<Color>(new Color[]
            {
                new Color { Id = 1, ColorType = ColorType.Value },
                new Color { Id = 2, ColorType = ColorType.Value },
                new Color { Id = 3, ColorType = ColorType.Multiplier },
                new Color { Id = 4, ColorType = ColorType.Tolerance },
            });

            fakeColorsRepository.Setup(fake => fake.Get(null, null, "")).Returns(fakeColors);
            fakeUnitOfWork.Setup(fake => fake.ColorsRepository).Returns(fakeColorsRepository.Object);

            OhmCalculatorService service = new OhmCalculatorService(fakeUnitOfWork.Object);

            // The third value does not exist in the colors list
            service.Calculate(1, 2, 5, 4);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Throw_Exception_When_MultiplierValueColorId_Exists_But_Is_Not_Multiplier_ColorType()
        {
            Mock<IOhmCalculatorUnitOfWork> fakeUnitOfWork = new Mock<IOhmCalculatorUnitOfWork>();
            Mock<IRepository<Color>> fakeColorsRepository = new Mock<IRepository<Color>>();
            List<Color> fakeColors = new List<Color>(new Color[]
            {
                new Color { Id = 1, ColorType = ColorType.Value },
                new Color { Id = 2, ColorType = ColorType.Value },
                new Color { Id = 3, ColorType = ColorType.Multiplier },
                new Color { Id = 4, ColorType = ColorType.Tolerance },
            });

            fakeColorsRepository.Setup(fake => fake.Get(null, null, "")).Returns(fakeColors);
            fakeUnitOfWork.Setup(fake => fake.ColorsRepository).Returns(fakeColorsRepository.Object);

            OhmCalculatorService service = new OhmCalculatorService(fakeUnitOfWork.Object);

            // The third value exists in the colors list, but is not a multiplier color type
            service.Calculate(1, 2, 2, 4);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Throw_Exception_When_ToleranceValueColorId_Does_Not_Exist()
        {
            Mock<IOhmCalculatorUnitOfWork> fakeUnitOfWork = new Mock<IOhmCalculatorUnitOfWork>();
            Mock<IRepository<Color>> fakeColorsRepository = new Mock<IRepository<Color>>();
            List<Color> fakeColors = new List<Color>(new Color[]
            {
                new Color { Id = 1, ColorType = ColorType.Value },
                new Color { Id = 2, ColorType = ColorType.Value },
                new Color { Id = 3, ColorType = ColorType.Multiplier },
                new Color { Id = 4, ColorType = ColorType.Tolerance },
            });

            fakeColorsRepository.Setup(fake => fake.Get(null, null, "")).Returns(fakeColors);
            fakeUnitOfWork.Setup(fake => fake.ColorsRepository).Returns(fakeColorsRepository.Object);

            OhmCalculatorService service = new OhmCalculatorService(fakeUnitOfWork.Object);

            // The fourth value does not exist in the colors list
            service.Calculate(1, 2, 3, 5);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Throw_Exception_When_ToleranceValueColorId_Exists_But_Is_Not_Tolerance_ColorType()
        {
            Mock<IOhmCalculatorUnitOfWork> fakeUnitOfWork = new Mock<IOhmCalculatorUnitOfWork>();
            Mock<IRepository<Color>> fakeColorsRepository = new Mock<IRepository<Color>>();
            List<Color> fakeColors = new List<Color>(new Color[]
            {
                new Color { Id = 1, ColorType = ColorType.Value },
                new Color { Id = 2, ColorType = ColorType.Value },
                new Color { Id = 3, ColorType = ColorType.Multiplier },
                new Color { Id = 4, ColorType = ColorType.Tolerance },
            });

            fakeColorsRepository.Setup(fake => fake.Get(null, null, "")).Returns(fakeColors);
            fakeUnitOfWork.Setup(fake => fake.ColorsRepository).Returns(fakeColorsRepository.Object);

            OhmCalculatorService service = new OhmCalculatorService(fakeUnitOfWork.Object);

            // The fourth value exists in the colors list, but is not a tolerance color type
            service.Calculate(1, 2, 3, 2);
        }
    }
}
