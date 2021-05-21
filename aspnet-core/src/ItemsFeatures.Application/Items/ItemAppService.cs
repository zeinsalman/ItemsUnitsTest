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
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;

namespace ItemsFeatures.Items
{
    public class ItemAppService : ItemsFeaturesAppService, IItemAppService
     
    {
        private readonly IItemRepository _itemRepository;
        private readonly IDistributedCache<ItemDto, Guid> _cache;
        private readonly IUnitRepository _unitRepository;

        private readonly ItemManager _itemManager;
        public ItemAppService(
            IItemRepository itemRepository,
            ItemManager itemManager, IDistributedCache<ItemDto, Guid> cache, IUnitRepository unitRepository)
        {
            _itemRepository = itemRepository;
            _itemManager = itemManager;
            _cache = cache;
            _unitRepository = unitRepository;
        }


        public async Task<ItemDto> CreateAsync(CreateItemDto input)
        {
            /*
            var item = await _itemManager.CreateAsync(
                 input.Name,
                 input.UnitId
            
             );
            */

           var item = await _itemRepository.CreateAsync(input.UnitId , CurrentTenant.Id , input.Name);

            return ObjectMapper.Map<Item, ItemDto>(item);
        }


        public async Task<ItemDto> GetAsync(Guid id)
        {
            return await _cache.GetOrAddAsync(
               id, //Guid type used as the cache key
               async () => await GetAsyncFromDatabase(id),
               () => new DistributedCacheEntryOptions
               {
                   AbsoluteExpiration = DateTimeOffset.Now.AddHours(1)
               }
           );
            /*
            var item = await _itemRepository.GetAsync(id);
            return ObjectMapper.Map<Item, ItemDto>(item);
            */

        }

        public async Task<ItemDto> GetAsyncFromDatabase(Guid id)
        {
            
           
            var items = await _itemRepository.GetIQueryableItems();

            var query = from item in items
                        join unit in _unitRepository on item.UnitId equals unit.Id
                        where item.Id == id
                        select new { item, unit };

            //Execute the query and get the book with author
            var queryResult = await AsyncExecuter.FirstOrDefaultAsync(query);
            if (queryResult == null)
            {
                throw new EntityNotFoundException(typeof(Item), id);
            }

            var itemDto = ObjectMapper.Map<Item, ItemDto>(queryResult.item);
            itemDto.UnitName = queryResult.unit.Name;
            return itemDto;
           /*
            var item = await _itemRepository.GetAsync(id);

            return ObjectMapper.Map<Item, ItemDto>(item);
           */
        }

        public async Task<long> GetItemCountAsync(Guid? tenantId)
        {
            return  await _itemRepository.GetItemCountAsync(tenantId);
        }

        public async Task<PagedResultDto<ItemDto>> GetListAsync(GetItemListDto input)
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

             

            var totalCount = input.Filter == null
                ? await _itemRepository.CountAsync()
                : await _itemRepository.CountAsync(
                    item => item.Name.Contains(input.Filter));

            return new PagedResultDto<ItemDto>(
                totalCount,
                ObjectMapper.Map<List<Item>, List<ItemDto>>(items)
            );
        }
        
        

        public async Task<ItemDto> UpdateAsync(Guid id, UpdateItemDto input)
        {
            var item = await _itemRepository.GetAsync(id);

            item.Name = input.Name;

            item.UnitId = input.UnitId;

            item = await _itemRepository.UpdateAsync(item);
            // item = await _itemRepository.UpdateAsync(item.Id , item.UnitId, item.TenantId , item.Name);
            return ObjectMapper.Map<Item, ItemDto>(item);
        }
        
    }
}
