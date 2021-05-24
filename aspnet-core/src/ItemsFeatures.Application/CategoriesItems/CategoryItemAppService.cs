using ItemsFeatures.Categories;
using ItemsFeatures.Items;
using ItemsFeatures.Permissions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Data;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.MultiTenancy;

namespace ItemsFeatures.CategoriesItems
{
    public class CategoryItemAppService :
         CrudAppService<
            CategoryItem,
            CategoryItemDto,
            Guid,
            PagedAndSortedResultRequestDto,
            CreateUpdateCategoryItemDto>,
        ICategoryItemAppService
    {

        public readonly IRepository<Category, Guid> _categoryRepository;
        public readonly IItemRepository _itemRepository;
        private readonly IDataFilter _dataFilter;
        
        public CategoryItemAppService(IRepository<CategoryItem, Guid> repository, IRepository<Category, Guid> categoryRepository, IItemRepository itemRepository, IDataFilter dataFilter)
          : base(repository)
        {
            _categoryRepository = categoryRepository;

            _itemRepository = itemRepository;
            _dataFilter = dataFilter;

            GetPolicyName = ItemsFeaturesPermissions.CategoryItem.Default;
            GetListPolicyName = ItemsFeaturesPermissions.CategoryItem.Default;
            CreatePolicyName = ItemsFeaturesPermissions.CategoryItem.Create;
            UpdatePolicyName = ItemsFeaturesPermissions.CategoryItem.Edit;
            DeletePolicyName = ItemsFeaturesPermissions.CategoryItem.Delete;
        }

        public override async Task<CategoryItemDto> GetAsync(Guid id)
        {

            var categoryItems = await Repository.GetQueryableAsync();
            var query = (from categoryItem in categoryItems
                         join category in _categoryRepository on categoryItem.CategoryId equals category.Id
                         join item in _itemRepository on categoryItem.ItemId equals item.Id

                         where categoryItem.Id == id
                         select new { categoryItem, category, item });





            var queryResult = await AsyncExecuter.FirstOrDefaultAsync(query);

            if (!CurrentTenant.Id.HasValue)
            {
                using (_dataFilter.Disable<IMultiTenant>())
                {

                    queryResult = await AsyncExecuter.FirstOrDefaultAsync(query);
                }
            }
            if (queryResult == null)
            {
                throw new EntityNotFoundException(typeof(CategoryItem), id);
            }

            var categoryItDto = ObjectMapper.Map<CategoryItem, CategoryItemDto>(queryResult.categoryItem);
            categoryItDto.CategoryName = queryResult.category.Name;
            categoryItDto.ItemName = queryResult.item.Name;
            
            return categoryItDto;

         
        }

        public override async Task<PagedResultDto<CategoryItemDto>> GetListAsync(PagedAndSortedResultRequestDto input)
        {
            var categoryItems = await Repository.GetQueryableAsync();
            //var query = null;
            var  query = (from categoryItem in categoryItems
                         join category in _categoryRepository on categoryItem.CategoryId equals category.Id
                         join item in _itemRepository on categoryItem.ItemId equals item.Id
                         select new
                         {
                             categoryItem,
                             category,
                             item
                         });

            query = query
               //.OrderBy(NormalizeSorting(input.Sorting))
               .Skip(input.SkipCount)
               .Take(input.MaxResultCount);
            var queryResult = await AsyncExecuter.ToListAsync(query);
            if (!CurrentTenant.Id.HasValue)
            {
                using (_dataFilter.Disable<IMultiTenant>())
                {
                     
                     queryResult = await AsyncExecuter.ToListAsync(query);
                }
            }
            
       

            //_itemRepository = _itemRepository.GetQueryableAsync();
       
        /*
            query = query
                //.OrderBy(NormalizeSorting(input.Sorting))
                .Skip(input.SkipCount)
                .Take(input.MaxResultCount);
            var queryResult = await AsyncExecuter.ToListAsync(query);


            */
            var categoryItemDtos = queryResult.Select(x =>
            {
                var categoryItemDto = ObjectMapper.Map<CategoryItem, CategoryItemDto>(x.categoryItem);
                categoryItemDto.CategoryName = x.category.Name;
                categoryItemDto.ItemName = x.item.Name;

                return categoryItemDto;
            }).ToList();

            var totalCount = await Repository.GetCountAsync();

            return new PagedResultDto<CategoryItemDto>(
                totalCount,
                categoryItemDtos
            );
        }

        private static string NormalizeSorting(string sorting)
        {
            if (sorting.IsNullOrEmpty())
            {
                return $"categoryItem.{nameof(CategoryItem.CreationTime)}";
            }

            if (sorting.Contains("itemName", StringComparison.OrdinalIgnoreCase))
            {
                return sorting.Replace(
                    "itemName",
                    "item.Name",
                    StringComparison.OrdinalIgnoreCase
                );
            }
            if (sorting.Contains("categoryName", StringComparison.OrdinalIgnoreCase))
            {
                return sorting.Replace(
                    "categoryName",
                    "category.Name",
                    StringComparison.OrdinalIgnoreCase
                );
            }

            return $"categoryItem.{sorting}";
        }
    }
}
