using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OhmCalculatorApi.Abstractions;
using OhmCalculatorApi.DataAccess;

namespace OhmCalculatorApiTests
{
    [TestClass]
    public class OhmCalculatorDbContextTests
    {
        private class MockEntity
        {
            public int Id { get; set; }
        }

        [TestMethod]
        public void When_OnConfiguring_Should_Configure_Using_IDbBuilder()
        {
            Mock<IDbBuilder> fakeDbBuilder = new Mock<IDbBuilder>();
            SetupFakeDbBuilder(fakeDbBuilder);

            using (var dbContext = new OhmCalculatorDbContext(fakeDbBuilder.Object))
            {
                DeleteAndCreateDbContext(dbContext);
                fakeDbBuilder.Verify(fake => fake.Configure(It.IsAny<DbContextOptionsBuilder>()));
                Assert.AreEqual("Filename=test.db", dbContext.Database.GetConnectionString());
            }
        }

        [TestMethod]
        public void When_OnModelCreating_Shoud_Create_Using_IDbBuilder()
        {
            Mock<IDbBuilder> fakeDbBuilder = new Mock<IDbBuilder>();
            SetupFakeDbBuilder(fakeDbBuilder);

            fakeDbBuilder.Setup(fake => fake.CreateModel(It.IsAny<ModelBuilder>()))
                .Callback((ModelBuilder modelBuilder) => { modelBuilder.Entity<MockEntity>().ToTable("MockTable"); });

            using (var dbContext = new OhmCalculatorDbContext(fakeDbBuilder.Object))
            {
                DeleteAndCreateDbContext(dbContext);
                fakeDbBuilder.Verify(fake => fake.CreateModel(It.IsAny<ModelBuilder>()));
            }
        }

        private void SetupFakeDbBuilder(Mock<IDbBuilder> fakeDbBuilder, string dbName = "test.db")
        {
            fakeDbBuilder.Setup(fake => fake.Configure(It.IsAny<DbContextOptionsBuilder>()))
                .Callback((DbContextOptionsBuilder optionsBuilder) => {
                    optionsBuilder.EnableServiceProviderCaching(false);
                    optionsBuilder.UseSqlite(connectionString: "Filename=" + dbName);
                });
        }

        private void DeleteAndCreateDbContext(OhmCalculatorDbContext dbContext)
        {
            dbContext.Database.EnsureDeleted();
            dbContext.Database.EnsureCreated();
        }
    }
}
