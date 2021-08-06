using System;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TableStorage.Audit.DAL.Entities;
using Z.EntityFramework.Plus;

namespace TableStorage.Audit.DAL
{
    public class ProjectDbContext : DbContext
    {
        public ProjectDbContext(DbContextOptions options, ITableStorageAccountProvider storageAccountProvider) : base(options)
        {
            StorageAccountProvider = storageAccountProvider;
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Address> Addresses { get; set; }

        public ITableStorageAccountProvider StorageAccountProvider { get; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

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