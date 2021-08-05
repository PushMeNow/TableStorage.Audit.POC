using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Azure.Cosmos.Table;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TableStorage.Audit.BLL;
using Z.EntityFramework.Plus;

namespace TableStorage.Audit.POC
{
    public class Startup
    {
        private readonly string _tableStorageConnString;
        private readonly string _databaseConnString;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            _tableStorageConnString = Configuration.GetConnectionString("TableStorage");
            _databaseConnString = Configuration.GetConnectionString("Database");
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            services.AddSingleton<ITableStorageAccountProvider, TableStorageAccountProvider>(_ => new TableStorageAccountProvider(_tableStorageConnString));
            services.AddDbContext<ProjectDbContext>(q => q.UseSqlServer(_databaseConnString, w => w.MigrationsAssembly("TableStorage.Audit.BLL")));

            ConfigureAudit();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
                             {
                                 endpoints.MapDefaultControllerRoute();
                             });
        }

        private static void ConfigureAudit()
        {
            AuditManager.DefaultConfiguration.UseUtcDateTime = true;
            AuditManager.DefaultConfiguration.AuditEntryFactory = _ => new AuditEntry
                                                                       {
                                                                           AuditEntryID = Guid.NewGuid()
                                                                       };
            AuditManager.DefaultConfiguration.AuditEntryPropertyFactory = q => new AuditEntryProperty
                                                                               {
                                                                                   AuditEntryPropertyID = Guid.NewGuid(),
                                                                                   AuditEntryID = q.AuditEntry.AuditEntryID
                                                                               };
            AuditManager.DefaultConfiguration.AutoSavePreAction = (context, audit) =>
                                                                  {
                                                                      const string auditEntryTableName = "AuditEntries";
                                                                      const string auditEntryPropertyTableName = "AuditEntryProperties";

                                                                      var castedContext = (ProjectDbContext)context;
                                                                      var client = castedContext.StorageAccountProvider
                                                                                                .GetStorageAccount()
                                                                                                .CreateCloudTableClient(new TableClientConfiguration());
                                                                      var auditEntryTable = client.GetTableReference(auditEntryTableName);
                                                                      var auditEntryPropertyTable = client.GetTableReference(auditEntryPropertyTableName);

                                                                      auditEntryTable.CreateIfNotExists();
                                                                      auditEntryPropertyTable.CreateIfNotExists();

                                                                      var entryBatch = new TableBatchOperation();
                                                                      var entryPropertyBatch = new TableBatchOperation();
                                                                      foreach (var auditEntry in audit.Entries)
                                                                      {
                                                                          auditEntry.PartitionKey = nameof(auditEntry);
                                                                          auditEntry.RowKey = Guid.NewGuid()
                                                                                                  .ToString();
                                                                          entryBatch.Insert(auditEntry);

                                                                          foreach (var auditEntryProperty in auditEntry.Properties)
                                                                          {
                                                                              auditEntryProperty.PartitionKey = nameof(auditEntryProperty);
                                                                              auditEntryProperty.RowKey = Guid.NewGuid()
                                                                                  .ToString();
                                                                              entryPropertyBatch.Insert(auditEntryProperty);
                                                                          }
                                                                      }

                                                                      auditEntryTable.ExecuteBatch(entryBatch);
                                                                      auditEntryPropertyTable.ExecuteBatch(entryPropertyBatch);
                                                                  };
        }
    }
}