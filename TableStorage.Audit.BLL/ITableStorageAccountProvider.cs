using Microsoft.Azure.Cosmos.Table;

namespace TableStorage.Audit.BLL
{
    public interface ITableStorageAccountProvider
    {
        CloudStorageAccount GetStorageAccount();
    }
}