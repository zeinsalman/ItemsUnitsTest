using AutoMapper;
using ItemsFeatures.Categories;
using ItemsFeatures.CategoriesItems;
using ItemsFeatures.Items;
using ItemsFeatures.Units;

namespace ItemsFeatures
{
    public class ItemsFeaturesApplicationAutoMapperProfile : Profile
    {
        public ItemsFeaturesApplicationAutoMapperProfile()
        {
            /* You can configure your AutoMapper mapping configuration here.
             * Alternatively, you can split your mapping configurations
             * into multiple profile classes for a better organization. */
            CreateMap<Unit, UnitDto>();
            CreateMap<Unit, CreatreOrUpdateUnitDto>();

            CreateMap <Category, CategoryDto>();
            CreateMap<Category, CreateUpdateCategoryDto>();

            CreateMap<Item, ItemDto>();
                  //.ForMember(x => x.UnitName, map => map.MapFrom(y=> y.Unit != null ? y.Unit.Name : null));

            CreateMap<Item, GetItemListDto>();

            CreateMap<CategoryItem, CategoryItemDto>();
            CreateMap<CategoryItem, CreateUpdateCategoryItemDto>();

        }
    }
}
