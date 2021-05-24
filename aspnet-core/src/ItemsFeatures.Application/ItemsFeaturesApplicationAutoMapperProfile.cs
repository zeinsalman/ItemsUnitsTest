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
            CreateMap< CreatreOrUpdateUnitDto , Unit>();
                
            CreateMap <Category, CategoryDto>();
            CreateMap<CreateUpdateCategoryDto ,Category>();

            CreateMap<Item, ItemDto>()
                  .ForMember(x => x.UnitName, map => map.MapFrom(y=> y.Unit != null ? y.Unit.Name : null))
                  .ForMember(x => x.ItemCategories, map => map.MapFrom(y => y.CategoriesItems != null ? y.CategoriesItems : null))
                  //.ForMember(x => x.UnitName, map => map.MapFrom(y => y.Unit != null ? y.Unit.Name : null))
                  ;

            CreateMap<Item, GetItemListDto>();

            CreateMap<CategoryItem, CategoryItemDto>()
                .ForMember(x => x.CategoryName, map => map.MapFrom(y => y.Category != null ? y.Category.Name : null))
                .ForMember(x => x.ItemName, map => map.MapFrom(y => y.Item != null ? y.Item.Name : null))
                  ;
            CreateMap<CreateUpdateCategoryItemDto ,CategoryItem>();

        }
    }
}
