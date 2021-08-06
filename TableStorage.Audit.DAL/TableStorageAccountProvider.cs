using Microsoft.Azure.Cosmos.Table;

namespace TableStorage.Audit.DAL
{
    public class TableStorageAccountProvider : ITableStorageAccountProvider
    {
        private readonly CloudStorageAccount _storageAccount;

        public TableStorageAccountProvider(string connectionString)
        {
            _storageAccount = CloudStorageAccount.Parse(connectionString);
        }

        public CloudStorageAccount GetStorageAccount()
        {
            return _storageAccount;
        }
    }
}