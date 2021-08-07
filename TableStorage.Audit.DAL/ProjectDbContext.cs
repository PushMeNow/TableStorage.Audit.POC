using System;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TableStorage.Audit.DAL.Entities;

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
            var audit = new Z.EntityFramework.Plus.Audit();
            audit.PreSaveChanges(this);
            var rowAffecteds = base.SaveChanges();
            audit.PostSaveChanges();

            if (audit.Configuration.AutoSavePreAction == null)
            {
                return rowAffecteds;
            }

            audit.Configuration.AutoSavePreAction(this, audit);
            base.SaveChanges();

            return rowAffecteds;
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            UpdateBaseFields();

            var audit = new Z.EntityFramework.Plus.Audit();
            audit.PreSaveChanges(this);
            var rowAffecteds = await base.SaveChangesAsync(cancellationToken);
            audit.PostSaveChanges();

            if (audit.Configuration.AutoSavePreAction == null)
            {
                return rowAffecteds;
            }

            audit.Configuration.AutoSavePreAction(this, audit);
            await base.SaveChangesAsync(cancellationToken);

            return rowAffecteds;
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