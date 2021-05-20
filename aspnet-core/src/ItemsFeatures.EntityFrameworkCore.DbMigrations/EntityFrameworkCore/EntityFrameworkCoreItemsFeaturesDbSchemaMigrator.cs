using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ItemsFeatures.Data;
using Volo.Abp.DependencyInjection;

namespace ItemsFeatures.EntityFrameworkCore
{
    public class EntityFrameworkCoreItemsFeaturesDbSchemaMigrator
        : IItemsFeaturesDbSchemaMigrator, ITransientDependency
    {
        private readonly IServiceProvider _serviceProvider;

        public EntityFrameworkCoreItemsFeaturesDbSchemaMigrator(
            IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task MigrateAsync()
        {
            /* We intentionally resolving the ItemsFeaturesMigrationsDbContext
             * from IServiceProvider (instead of directly injecting it)
             * to properly get the connection string of the current tenant in the
             * current scope.
             */

            await _serviceProvider
                .GetRequiredService<ItemsFeaturesMigrationsDbContext>()
                .Database
                .MigrateAsync();
        }
    }
}