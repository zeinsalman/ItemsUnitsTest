using AutoMapper;
using ItemsFeatures.Categories;
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

        }
    }
}
