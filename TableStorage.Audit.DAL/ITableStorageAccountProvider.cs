using Microsoft.Azure.Cosmos.Table;

namespace TableStorage.Audit.DAL
{
    public interface ITableStorageAccountProvider
    {
        CloudStorageAccount GetStorageAccount();
    }
}