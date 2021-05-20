using ItemsFeatures.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace ItemsFeatures.Controllers
{
    /* Inherit your controllers from this class.
     */
    public abstract class ItemsFeaturesController : AbpController
    {
        protected ItemsFeaturesController()
        {
            LocalizationResource = typeof(ItemsFeaturesResource);
        }
    }
}