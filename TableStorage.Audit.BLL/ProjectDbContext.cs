using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TableStorage.Audit.BLL.Entities;
using Z.EntityFramework.Plus;

namespace TableStorage.Audit.BLL
{
    public class ProjectDbContext : DbContext
    {
        public ProjectDbContext(DbContextOptions options, ITableStorageAccountProvider storageAccountProvider) : base(options)
        {
            StorageAccountProvider = storageAccountProvider;
        }

        public ITableStorageAccountProvider StorageAccountProvider { get; }

        public override int SaveChanges()
        {
            UpdateBaseFields();
            return this.SaveChanges(new Z.EntityFramework.Plus.Audit());
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            UpdateBaseFields();
            return this.SaveChangesAsync(new Z.EntityFramework.Plus.Audit(), default);
        }

        private void UpdateBaseFields()
        {
            var entries = ChangeTracker.Entries<BaseFields>()
                                       .Where(q => q.State is EntityState.Added or EntityState.Modified);

            foreach (var entry in entries)
            {
                var entity = entry.Entity;

                entity.LastModified = DateTime.UtcNow;

                if (entry.State is EntityState.Added)
                {
                    entity.CreatedDate = DateTime.UtcNow;
                }
            }
        }
    }
}