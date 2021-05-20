using Volo.Abp.Modularity;

namespace ItemsFeatures
{
    [DependsOn(
        typeof(ItemsFeaturesApplicationModule),
        typeof(ItemsFeaturesDomainTestModule)
        )]
    public class ItemsFeaturesApplicationTestModule : AbpModule
    {

    }
}