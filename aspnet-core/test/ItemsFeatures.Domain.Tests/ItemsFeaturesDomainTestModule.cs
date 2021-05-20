using ItemsFeatures.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace ItemsFeatures
{
    [DependsOn(
        typeof(ItemsFeaturesEntityFrameworkCoreTestModule)
        )]
    public class ItemsFeaturesDomainTestModule : AbpModule
    {

    }
}