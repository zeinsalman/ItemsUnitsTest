using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;

namespace ItemsFeatures.EntityFrameworkCore
{
    [DependsOn(
        typeof(ItemsFeaturesEntityFrameworkCoreModule)
        )]
    public class ItemsFeaturesEntityFrameworkCoreDbMigrationsModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAbpDbContext<ItemsFeaturesMigrationsDbContext>();
        }
    }
}
