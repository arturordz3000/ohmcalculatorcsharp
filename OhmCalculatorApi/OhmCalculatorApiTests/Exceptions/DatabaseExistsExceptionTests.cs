using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OhmCalculatorApi.Exceptions;

namespace OhmCalculatorApiTests.Exceptions
{
    [TestClass]
    public class DatabaseExistsExceptionTests
    {
        [TestMethod]
        public void Initializes_With_Message()
        {
            var exception = new DatabaseExistsException();
            Assert.AreEqual("Database already exists.", exception.Message);
        }
    }
}
