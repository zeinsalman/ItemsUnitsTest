using Volo.Abp.Settings;

namespace ItemsFeatures.Settings
{
    public class ItemsFeaturesSettingDefinitionProvider : SettingDefinitionProvider
    {
        public override void Define(ISettingDefinitionContext context)
        {
            //Define your own settings here. Example:
            //context.Add(new SettingDefinition(ItemsFeaturesSettings.MySetting1));
        }
    }
}
