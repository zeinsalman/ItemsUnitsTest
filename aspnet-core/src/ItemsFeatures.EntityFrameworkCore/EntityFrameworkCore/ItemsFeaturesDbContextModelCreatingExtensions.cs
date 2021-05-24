using ItemsFeatures.Categories;
using ItemsFeatures.CategoriesItems;
using ItemsFeatures.Items;
using ItemsFeatures.Units;
using Microsoft.EntityFrameworkCore;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace ItemsFeatures.EntityFrameworkCore
{
    public static class ItemsFeaturesDbContextModelCreatingExtensions
    {
        public static void ConfigureItemsFeatures(this ModelBuilder builder)
        {
            Check.NotNull(builder, nameof(builder));

            /* Configure your own tables/entities inside here */

            //builder.Entity<YourEntity>(b =>
            //{
            //    b.ToTable(ItemsFeaturesConsts.DbTablePrefix + "YourEntities", ItemsFeaturesConsts.DbSchema);
            //    b.ConfigureByConvention(); //auto configure for the base class props
            //    //...
            //});

            builder.Entity<Category>(b =>
            {
                b.ToTable(ItemFeaturesConsts.DbTablePrefix + "Categories", ItemFeaturesConsts.DbSchema);
                b.ConfigureByConvention(); //auto configure for the base class props
                b.Property(x => x.Name).IsRequired().HasMaxLength(128);

                // ADD THE MAPPING FOR THE RELATION
                b.HasMany(j => j.SubCategories)
                .WithOne(j=>j.ParentCategory)
                .HasForeignKey(j => j.ParentId);

                b.HasMany(j => j.CategoriesItems)
                .WithOne(j => j.Category)
                .HasForeignKey(j => j.CategoryId);
            });

            builder.Entity<Item>(b =>
            {
                b.ToTable(ItemFeaturesConsts.DbTablePrefix + "Items", ItemFeaturesConsts.DbSchema);
                b.ConfigureByConvention(); //auto configure for the base class props
                b.Property(x => x.Name).IsRequired().HasMaxLength(128);

                // ADD THE MAPPING FOR THE RELATION
              

                b.HasMany(j => j.CategoriesItems)
                .WithOne(j => j.Item)
                .HasForeignKey(j => j.ItemId);

                b.HasOne<Unit>(x=>x.Unit).WithMany(x=>x.Items).HasForeignKey(x => x.UnitId).IsRequired();

            });

            builder.Entity<Unit>(b =>
            {
                b.ToTable(ItemFeaturesConsts.DbTablePrefix + "Units", ItemFeaturesConsts.DbSchema);
                b.ConfigureByConvention(); //auto configure for the base class props
                b.Property(x => x.Name).IsRequired().HasMaxLength(128);

                // ADD THE MAPPING FOR THE RELATION


              
            });


            builder.Entity<CategoryItem>(b =>
            {
                b.ToTable(ItemFeaturesConsts.DbTablePrefix + "CategoriesItems", ItemFeaturesConsts.DbSchema);
                b.ConfigureByConvention(); //auto configure for the base class props

                // ADD THE MAPPING FOR THE RELATION



            });
        }
    }
}