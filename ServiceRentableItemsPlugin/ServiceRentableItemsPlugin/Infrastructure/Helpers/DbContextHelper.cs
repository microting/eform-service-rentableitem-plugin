using Microting.eFormRentableItemBase.Infrastructure.Data;
using Microting.eFormRentableItemBase.Infrastructure.Data.Factories;

namespace ServiceRentableItemsPlugin.Infrastructure.Helpers
{
    public class DbContextHelper
    {
        private string ConnecitonString { get; }

        public DbContextHelper(string connecitonString)
        {
            ConnecitonString = connecitonString;
        }

        public eFormRentableItemPnDbContext GetDbContext()
        {
            eFormRentableItemPnDbContextFactory contextFactory = new eFormRentableItemPnDbContextFactory();

            return contextFactory.CreateDbContext(new[] {ConnecitonString});
        }
    }
}