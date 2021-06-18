using Microsoft.VisualStudio.TestTools.UnitTesting;
using OhmCalculatorApi.DataAccess;
using OhmCalculatorApi.DataAccess.DbBuilders;

namespace OhmCalculatorApiIntegrationTests
{
    [TestClass]
    public class OhmCalculatorDbContextTests
    {
        [TestMethod]
        public void Sqlite_Database_Is_Created_Successfully()
        {
            using (var dbContext = new OhmCalculatorDbContext(new SqliteOhmCalculatorDbBuilder("integration.db")))
            {
                dbContext.Database.EnsureDeleted();
                dbContext.Database.EnsureCreated();
            }
        }
    }
}
