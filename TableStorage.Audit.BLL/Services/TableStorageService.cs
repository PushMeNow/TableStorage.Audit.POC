using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Azure.Cosmos.Table;
using TableStorage.Audit.BLL.Interfaces;
using TableStorage.Audit.BLL.Responses;
using TableStorage.Audit.DAL;
using Z.EntityFramework.Plus;

namespace TableStorage.Audit.BLL.Services
{
    public class TableStorageService : ITableStorageService
    {
        private readonly IMapper _mapper;
        private readonly CloudStorageAccount _storageAccount;

        public TableStorageService(ITableStorageAccountProvider storageAccountProvider, IMapper mapper)
        {
            _mapper = mapper;
            _storageAccount = storageAccountProvider.GetStorageAccount();
        }

        public async Task<AuditEntryResponse[]> GetAudit()
        {
            var client = _storageAccount.CreateCloudTableClient(new TableClientConfiguration());
            var auditEntryTable = client.GetTableReference(Constants.AuditEntryTableName);
            var auditEntryPropertyTable = client.GetTableReference(Constants.AuditEntryPropertyTableName);

            var queryAuditEntries = new TableQuery<AuditEntry>();
            var queryAuditEntryProperties = new TableQuery<AuditEntryProperty>();

            var auditEntries = auditEntryTable.ExecuteQuerySegmentedAsync(queryAuditEntries, new TableContinuationToken());
            var auditEntryProperties = auditEntryPropertyTable.ExecuteQuerySegmentedAsync(queryAuditEntryProperties, new TableContinuationToken());

            await Task.WhenAll(auditEntries, auditEntryProperties);

            return auditEntries.Result.GroupJoin(auditEntryProperties.Result,
                                                 entry => entry.AuditEntryID,
                                                 property => property.AuditEntryID,
                                                 (entry, properties) =>
                                                 {
                                                     var mappedEntry = _mapper.Map<AuditEntryResponse>(entry);
                                                     mappedEntry.Properties = _mapper.Map<AuditEntryPropertyResponse[]>(properties);

                                                     return mappedEntry;
                                                 })
                               .ToArray();
        }
    }
}