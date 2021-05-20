using System;
using System.Collections.Generic;
using System.Text;
using ItemsFeatures.Localization;
using Volo.Abp.Application.Services;

namespace ItemsFeatures
{
    /* Inherit your application services from this class.
     */
    public abstract class ItemsFeaturesAppService : ApplicationService
    {
        protected ItemsFeaturesAppService()
        {
            LocalizationResource = typeof(ItemsFeaturesResource);
        }
    }
}
