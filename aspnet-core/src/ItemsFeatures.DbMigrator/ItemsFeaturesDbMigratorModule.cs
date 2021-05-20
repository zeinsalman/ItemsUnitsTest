using ItemsFeatures.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Modularity;

namespace ItemsFeatures.DbMigrator
{
    [DependsOn(
        typeof(AbpAutofacModule),
        typeof(ItemsFeaturesEntityFrameworkCoreDbMigrationsModule),
        typeof(ItemsFeaturesApplicationContractsModule)
        )]
    public class ItemsFeaturesDbMigratorModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpBackgroundJobOptions>(options => options.IsJobExecutionEnabled = false);
        }
    }
}
