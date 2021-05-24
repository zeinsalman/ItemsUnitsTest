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

            var itemsPermission = myGroup.AddPermission(ItemsFeaturesPermissions.Item.Default, L("Permission:Items"));
            itemsPermission.AddChild(ItemsFeaturesPermissions.Item.Create, L("Permission:Items.Create"));
            itemsPermission.AddChild(ItemsFeaturesPermissions.Item.Edit, L("Permission:Items.Edit"));
            itemsPermission.AddChild(ItemsFeaturesPermissions.Item.Delete, L("Permission:Items.Delete"));


            var categoriesPermission = myGroup.AddPermission(ItemsFeaturesPermissions.Category.Default, L("Permission:Categories"));
            categoriesPermission.AddChild(ItemsFeaturesPermissions.Category.Create, L("Permission:Categories.Create"));
            categoriesPermission.AddChild(ItemsFeaturesPermissions.Category.Edit, L("Permission:Categories.Edit"));
            categoriesPermission.AddChild(ItemsFeaturesPermissions.Category.Delete, L("Permission:Categories.Delete"));

            var unitsPermission = myGroup.AddPermission(ItemsFeaturesPermissions.Unit.Default, L("Permission:Units"));
            unitsPermission.AddChild(ItemsFeaturesPermissions.Unit.Create, L("Permission:Units.Create"));
            unitsPermission.AddChild(ItemsFeaturesPermissions.Unit.Edit, L("Permission:Units.Edit"));
            unitsPermission.AddChild(ItemsFeaturesPermissions.Unit.Delete, L("Permission:Units.Delete"));

            var categoryItemsPermission = myGroup.AddPermission(ItemsFeaturesPermissions.CategoryItem.Default, L("Permission:CategoryItems"));
            categoryItemsPermission.AddChild(ItemsFeaturesPermissions.CategoryItem.Create, L("Permission:CategoryItems.Create"));
            categoryItemsPermission.AddChild(ItemsFeaturesPermissions.CategoryItem.Edit, L("Permission:CategoryItems.Edit"));
            categoryItemsPermission.AddChild(ItemsFeaturesPermissions.CategoryItem.Delete, L("Permission:CategoryItems.Delete"));
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<ItemsFeaturesResource>(name);
        }
    }
}
