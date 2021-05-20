using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace ItemsFeatures.Data
{
    /* This is used if database provider does't define
     * IItemsFeaturesDbSchemaMigrator implementation.
     */
    public class NullItemsFeaturesDbSchemaMigrator : IItemsFeaturesDbSchemaMigrator, ITransientDependency
    {
        public Task MigrateAsync()
        {
            return Task.CompletedTask;
        }
    }
}