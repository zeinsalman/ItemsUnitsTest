using ItemsFeatures.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace ItemsFeatures.Permissions
{
    public class ItemsFeaturesPermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            var myGroup = context.AddGroup(ItemsFeaturesPermissions.GroupName);

            //Define your own permissions here. Example:
            //myGroup.AddPermission(ItemsFeaturesPermissions.MyPermission1, L("Permission:MyPermission1"));
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<ItemsFeaturesResource>(name);
        }
    }
}
