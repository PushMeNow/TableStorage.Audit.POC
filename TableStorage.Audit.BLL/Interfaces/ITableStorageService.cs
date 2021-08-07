using System.Collections.Generic;
using System.Threading.Tasks;
using TableStorage.Audit.BLL.Responses;

namespace TableStorage.Audit.BLL.Interfaces
{
    public interface ITableStorageService
    {
        Task<AuditEntryResponse[]> GetAudit();
    }
}