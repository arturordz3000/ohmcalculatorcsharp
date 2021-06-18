using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OhmCalculatorApi.DataAccess;
using OhmCalculatorApi.DataAccess.DbBuilders;
using OhmCalculatorApi.DataAccess.Repositories;
using OhmCalculatorApi.Models;

namespace OhmCalculatorApiIntegrationTests.DataAccess.Repositories
{
    [TestClass]
    public class OhmCalculatorRepositoryTests
    {
        [TestMethod]
        public void Can_Insert_And_Get()
        {
            using (var dbContext = new OhmCalculatorDbContext(new SqliteOhmCalculatorDbBuilder("integration.db")))
            {
                dbContext.Database.EnsureDeleted();
                dbContext.Database.EnsureCreated();

                var colorsRespository = new OhmCalculatorRepository<Color>(dbContext);
                colorsRespository.Insert(new Color { Id = 1, Rgb = "rgb(0, 0, 0)", ValueDescription = "0", ValueNumber = 0 });

                dbContext.SaveChanges();

                var color = colorsRespository.GetByID(1);

                Assert.IsNotNull(color);
            }
        }

        [TestMethod]
        public void Can_Update()
        {
            using (var dbContext = new OhmCalculatorDbContext(new SqliteOhmCalculatorDbBuilder("integration.db")))
            {
                dbContext.Database.EnsureDeleted();
                dbContext.Database.EnsureCreated();

                var colorsRespository = new OhmCalculatorRepository<Color>(dbContext);

                var color = new Color { Id = 1, Rgb = "rgb(0, 0, 0)", ValueDescription = "0", ValueNumber = 0 };
                colorsRespository.Insert(color);
                dbContext.SaveChanges();

                color.Rgb = "rgb(1,1,1)";
                colorsRespository.Update(color);
                dbContext.SaveChanges();

                var updatedColor = colorsRespository.GetByID(1);

                Assert.IsNotNull(updatedColor);
                Assert.AreEqual("rgb(1,1,1)", updatedColor.Rgb);
            }
        }

        [TestMethod]
        public void Can_Delete()
        {
            using (var dbContext = new OhmCalculatorDbContext(new SqliteOhmCalculatorDbBuilder("integration.db")))
            {
                dbContext.Database.EnsureDeleted();
                dbContext.Database.EnsureCreated();

                var colorsRespository = new OhmCalculatorRepository<Color>(dbContext);

                var color = new Color { Id = 1, Rgb = "rgb(0, 0, 0)", ValueDescription = "0", ValueNumber = 0 };
                colorsRespository.Insert(color);
                dbContext.SaveChanges();

                colorsRespository.Delete(color);
                dbContext.SaveChanges();

                var deletedColor = colorsRespository.GetByID(1);

                Assert.IsNull(deletedColor);
            }
        }
    }
}
