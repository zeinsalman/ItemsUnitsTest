using ItemsFeatures.Categories;
using ItemsFeatures.CategoriesItems;
using ItemsFeatures.Permissions;
using ItemsFeatures.Units;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Caching;
using Volo.Abp.Data;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.MultiTenancy;

namespace ItemsFeatures.Items
{
    public class ItemAppService : ItemsFeaturesAppService, IItemAppService
     
    {
        private readonly IItemRepository _itemRepository;
        private readonly IDistributedCache<ItemDto, string> _cache;
        private readonly IDistributedCache<List<ItemDto>, string> _cache_paged;

        private readonly IRepository<Unit , Guid> _unitRepository;
        private readonly IRepository<CategoryItem, Guid> _categoryItemsRepository;
        private readonly IRepository<Category, Guid> _categoryRepository;
        private readonly IDataFilter _dataFilter;

        //private readonly ICacheManager _cacheManager;

        private readonly ItemManager _itemManager;
        public ItemAppService(
            IItemRepository itemRepository,
            ItemManager itemManager, IDistributedCache<ItemDto, string> cache, IUnitRepository unitRepository, IRepository<CategoryItem, Guid> categoryItemsRepository, IRepository<Category, Guid> categoryRepository, IDataFilter dataFilter = null, IDistributedCache<List<ItemDto>, string> cache_paged = null)
        {
            _itemRepository = itemRepository;
            _itemManager = itemManager;
            _cache = cache;
            _unitRepository = unitRepository;
            _categoryItemsRepository = categoryItemsRepository;
            _categoryRepository = categoryRepository;
            _dataFilter = dataFilter;
            _cache_paged = cache_paged;
        }


        public async Task<ItemDto> CreateAsync(CreateItemDto input)
        {
            /*
            var item = await _itemManager.CreateAsync(
                 input.Name,
                 input.UnitId
            
             );
            */
            if(input.UnitId == Guid.Empty)
            {
                throw new Exception("unit id field is required");
            }
           var item = await _itemRepository.CreateAsync(input.UnitId , CurrentTenant.Id , input.Name);
            var itemDto = await GetAsyncFromDatabase(item.Id);

            await _cache.SetAsync(item.Id.ToString(), itemDto);   
            //var mappedItem  = ObjectMapper.Map<Item, ItemDto>(item);
            return itemDto;
        }


        public async Task<ItemDto> GetAsync(Guid id)
        {
            
            return await _cache.GetOrAddAsync(
               id.ToString(), //Guid type used as the cache key
               async () => await GetAsyncFromDatabase(id),
               () => new DistributedCacheEntryOptions
               {
                   AbsoluteExpiration = DateTimeOffset.Now.AddHours(1)
               }
           );
           

        }

        private async Task<ItemDto> GetAsyncFromDatabase(Guid id)
        {
            
           
            var items = await _itemRepository.GetIQueryableItems();

            var query = from item in items
                        join unit in _unitRepository on item.UnitId equals unit.Id
                        
                        where item.Id == id
                        select new { Item = item, Unit = unit 
                          
                        };

       
            var categoriesItems = await _categoryItemsRepository.GetQueryableAsync();

            var query1 = from categoryItem in categoriesItems
                    join category in _categoryRepository on categoryItem.CategoryId equals category.Id
                    join item in  _itemRepository on categoryItem.ItemId equals item.Id
                    where categoryItem.ItemId == id
                    select new CategoryItemDto
                    {
                        Id = categoryItem.Id,
                        ItemId = item.Id,
                        ItemName = item.Name ,
                        CategoryId =category.Id,
                        CategoryName = category.Name,
                        CreationTime = categoryItem.CreationTime,
                        CreatorId = categoryItem.CreatorId

                    };
            var itemDto = new ItemDto();
            if (!CurrentTenant.Id.HasValue)
            {
                using (_dataFilter.Disable<IMultiTenant>())
                {
                    var queryResult = await AsyncExecuter.FirstOrDefaultAsync(query);
                    if (queryResult == null)
                    {
                        throw new EntityNotFoundException(typeof(Item), id);
                    }

                     itemDto = ObjectMapper.Map<Item, ItemDto>(queryResult.Item);
                    itemDto.UnitName = queryResult.Unit.Name;
                    var queryResult1 = await AsyncExecuter.ToListAsync(query1);
                    itemDto.ItemCategories = queryResult1;

                }
            }
            else
            {
                var queryResult = await AsyncExecuter.FirstOrDefaultAsync(query);
                if (queryResult == null)
                {
                    throw new EntityNotFoundException(typeof(Item), id);
                }

                 itemDto = ObjectMapper.Map<Item, ItemDto>(queryResult.Item);
                itemDto.UnitName = queryResult.Unit.Name;
                var queryResult1 = await AsyncExecuter.ToListAsync(query1);
                itemDto.ItemCategories = queryResult1;

            }

            return itemDto;
          
        }

        public async Task<long> GetItemCountAsync(Guid? tenantId)
        {
            return  await _itemRepository.GetItemCountAsync(tenantId);
        }

        public async Task<PagedResultDto<ItemDto>> GetListAsync(GetItemListDto input)
        {
            var  keyBuilder = new StringBuilder();

            foreach(var prop in input.GetType().GetProperties().OrderBy(x=>x.Name))
            {
                keyBuilder.Append($"|{prop.Name}-{prop.GetValue(input)}" );
            }
            string key = "All_Items_List" + ( keyBuilder.Length > 0 ? keyBuilder.ToString():  "" );
            var cashedItems = _cache_paged.Get(key);
            if (cashedItems == null)
            {
                return await GetListAsyncFromDatabase(input , key);
            }
            else
            {
                return new PagedResultDto<ItemDto>(
              cashedItems.Count,
              cashedItems
          );
            }

          

            /*

            if (input.Sorting.IsNullOrWhiteSpace())
            {
                input.Sorting = nameof(Item.Name);
            }

            var items = await _itemRepository.GetListAsync(
                input.SkipCount,
                input.MaxResultCount,
                input.Sorting,
                input.Filter
            );

            var totalCount = 0;
            if (!CurrentTenant.Id.HasValue)
            {
                using (_dataFilter.Disable<IMultiTenant>())
                {
                    totalCount = input.Filter == null
                ? await _itemRepository.CountAsync()
                : await _itemRepository.CountAsync(
                    item => item.Name.Contains(input.Filter));
                }
            }
            else
            {
                totalCount = input.Filter == null
                ? await _itemRepository.CountAsync()
                : await _itemRepository.CountAsync(
                    item => item.Name.Contains(input.Filter));
            }
            

            return new PagedResultDto<ItemDto>(
                totalCount,
                ObjectMapper.Map<List<Item>, List<ItemDto>>(items)
            );
            */
        }

        private async Task<PagedResultDto<ItemDto>> GetListAsyncFromDatabase(GetItemListDto input , string key)
        {

            if (input.Sorting.IsNullOrWhiteSpace())
            {
                input.Sorting = nameof(Item.Name);
            }

            var items = await _itemRepository.GetListAsync(
                input.SkipCount,
                input.MaxResultCount,
                input.Sorting,
                input.Filter
            );

            var totalCount = 0;
            if (!CurrentTenant.Id.HasValue)
            {
                using (_dataFilter.Disable<IMultiTenant>())
                {
                    totalCount = input.Filter == null
                ? await _itemRepository.CountAsync()
                : await _itemRepository.CountAsync(
                    item => item.Name.Contains(input.Filter));
                }
            }
            else
            {
                totalCount = input.Filter == null
                ? await _itemRepository.CountAsync()
                : await _itemRepository.CountAsync(
                    item => item.Name.Contains(input.Filter));
            }

            await _cache_paged.SetAsync(key, ObjectMapper.Map<List<Item>, List<ItemDto>>(items));
            return new PagedResultDto<ItemDto>(
                totalCount,
                ObjectMapper.Map<List<Item>, List<ItemDto>>(items)
            );
        }



        public async Task<ItemDto> UpdateAsync(Guid id, UpdateItemDto input)
        {
            var item = await _itemRepository.GetAsync(id);

            item.Name = input.Name;
            if (input.UnitId.HasValue) //a9f2c34e-408b-b71c-7034-39fcb0947410
            {
                item.UnitId = Guid.Parse(input.UnitId.ToString());
            }
           
            item.TenantId = CurrentTenant.Id;
          

            await _itemRepository.UpdateAsync(item);
           // item = 
            var cashedItem = _cache.Get(id.ToString());
            if(cashedItem != null)
            {
                await _cache.RemoveAsync(id.ToString());
            }
            var itemDto = await GetAsyncFromDatabase(id);

            await _cache.SetAsync(id.ToString(), itemDto);

            // item = await _itemRepository.UpdateAsync(item.Id , item.UnitId, item.TenantId , item.Name);
            return itemDto;
        }
        /*
        private async Task<IQueryable<CategoryItemDto>> GetCategoryItemsForItem()
        {
            var categoryItems = await _categoryItemsRepository.GetQueryableAsync();
            
            var query = from categoryItem in categoryItems
                        join category in _categoryRepository on categoryItem.CategoryId equals category.Id
                        join item in _itemRepository on categoryItem.ItemId equals item.Id

                        
                        select  CategoryItem {
                           Id = categoryItem.Id,
                           CategoryId = category.Id,
                           CategoryName = category.Name,
                           ItemId = item.Id,
                           ItemName = item.Name,
                           CreatorId = categoryItem.CreatorId
                        };

            
            // item = await _itemRepository.UpdateAsync(item.Id , item.UnitId, item.TenantId , item.Name);
            return query;
        }
        */

    }
}
