using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Sqlite.Infrastructure.Internal;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OhmCalculatorApi.DataAccess.DbBuilders;

namespace OhmCalculatorApiTests
{
    [TestClass]
    public class SqliteOhmCalculatorDbBuilderTests
    {
        [TestMethod]
        public void Set_Database_Filename_Passed_In_Constructor_When_Configuring_Database()
        {
            var builder = new SqliteOhmCalculatorDbBuilder("test.db");
            var fakeOptionsBuilder = new DbContextOptionsBuilder();

            builder.Configure(fakeOptionsBuilder);

#pragma warning disable EF1001 // Internal EF Core API usage.
            SqliteOptionsExtension sqliteExtension = fakeOptionsBuilder.Options.FindExtension<SqliteOptionsExtension>();
#pragma warning restore EF1001 // Internal EF Core API usage.

            Assert.AreEqual("Filename=test.db", sqliteExtension.ConnectionString);
        }

        [TestMethod]
        public void Make_Sure_Correct_Initialization_When_Creating_Model()
        {
            var builder = new SqliteOhmCalculatorDbBuilder("test.db");
            var fakeModelBuilder = new ModelBuilder();

            builder.CreateModel(fakeModelBuilder);
            fakeModelBuilder.FinalizeModel();

            int tablesCount = GetTablesCount(fakeModelBuilder.Model.GetEntityTypes());

            Assert.AreEqual(3, tablesCount);
        }

        private int GetTablesCount(IEnumerable<IMutableEntityType> tables)
        {
            int count = 0;

            foreach (var table in tables)
            {
                count++;
            }

            return count;
        }
    }
}
