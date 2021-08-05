namespace TableStorage.Audit.BLL
{
    public class TableStorageService
    {
        private readonly ITableStorageAccountProvider _storageAccountProvider;

        public TableStorageService(ITableStorageAccountProvider storageAccountProvider)
        {
            _storageAccountProvider = storageAccountProvider;
        }
    }
}