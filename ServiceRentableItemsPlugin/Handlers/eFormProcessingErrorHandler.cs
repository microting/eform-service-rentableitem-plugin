using System.Linq;
using System.Threading.Tasks;
using Microting.eFormRentableItemBase.Infrastructure.Data;
using Microting.eFormRentableItemBase.Infrastructure.Data.Entities;
using Rebus.Handlers;
using ServiceRentableItemsPlugin.Infrastructure.Helpers;
using ServiceRentableItemsPlugin.Messages;

namespace ServiceRentableItemsPlugin.Handlers
{
    public class eFormProcessingErrorHandler : IHandleMessages<eFormProcessingError>
    {
        private readonly eFormCore.Core _sdkCore;
        private readonly eFormRentableItemPnDbContext _dbContext;

        public eFormProcessingErrorHandler(eFormCore.Core sdkCore, DbContextHelper dbContextHelper)
        {
            _dbContext = dbContextHelper.GetDbContext();
            _sdkCore = sdkCore;
        }
        public async Task Handle(eFormProcessingError message)
        {
            ContractInspectionItem contractInspectionItem =
                _dbContext.ContractInspectionItem.SingleOrDefault(x => x.SDKCaseId == message.caseId);
            if (contractInspectionItem != null)
            {
                if (contractInspectionItem.Status < 110)
                {
                    contractInspectionItem.Status = 110;
                    await contractInspectionItem.Update(_dbContext);
                }
            }
        }
    }
}