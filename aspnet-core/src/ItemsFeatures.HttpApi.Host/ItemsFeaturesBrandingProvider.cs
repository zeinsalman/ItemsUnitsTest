using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace ItemsFeatures
{
    [Dependency(ReplaceServices = true)]
    public class ItemsFeaturesBrandingProvider : DefaultBrandingProvider
    {
        public override string AppName => "ItemsFeatures";
    }
}
