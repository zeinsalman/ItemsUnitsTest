namespace ItemsFeatures.Permissions
{
    public static class ItemsFeaturesPermissions
    {
        public const string GroupName = "ItemsFeatures";

        //Add your own permission names. Example:
        //public const string MyPermission1 = GroupName + ".MyPermission1";

        public static class Item
        {
            public const string Default = GroupName + ".Items";
            public const string Create = Default + ".Create";
            public const string Edit = Default + ".Edit";
            public const string Delete = Default + ".Delete";
        }

        public static class Category
        {
            public const string Default = GroupName + ".Categories";
            public const string Create = Default + ".Create";
            public const string Edit = Default + ".Edit";
            public const string Delete = Default + ".Delete";
        }

        public static class Unit
        {
            public const string Default = GroupName + ".Units";
            public const string Create = Default + ".Create";
            public const string Edit = Default + ".Edit";
            public const string Delete = Default + ".Delete";
        }

        public static class CategoryItem
        {
            public const string Default = GroupName + ".CategoriesItems";
            public const string Create = Default + ".Create";
            public const string Edit = Default + ".Edit";
            public const string Delete = Default + ".Delete";
        }
    }
}