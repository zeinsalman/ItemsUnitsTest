using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Data;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.MultiTenancy;

namespace ItemsFeatures.Items
{
    public class ItemManager : DomainService
    {
        private readonly IItemRepository _itemRepository;
        private readonly IDataFilter _dataFilter;

        public ItemManager(IItemRepository itemRepository , IDataFilter dataFilter)
        {
            _itemRepository = itemRepository;
            _dataFilter = dataFilter;

        }
        /*
        public async Task<Item> CreateAsync(string name, Guid unitId)
        {
            var item = new Item(unitId, CurrentTenant.Id , name);
            return await _itemRepository.InsertAsync(item);
        }
        */

        public async Task<Item> CreateAsync(
      [NotNull] string name,
     
      [NotNull] Guid unitId)
        {
            Check.NotNullOrWhiteSpace(name, nameof(name));

         

            return new Item(
               unitId , CurrentTenant.Id , name
                
            );
        }
        /*

        public async Task<long> GetItemCountAsync(Guid? tenantId)
        {

            if (tenantId.HasValue)
            {
                using (CurrentTenant.Change(tenantId))
                {
                    return await _itemRepository.GetCountAsync();
                }
            }
            else
            {
                using (_dataFilter.Disable<IMultiTenant>())
                {
                    return await _itemRepository.GetCountAsync();
                }
            }
        }

        */
        /*
        public async Task<long> GetAllItemCountAsync()
        {
            using (_dataFilter.Disable<IMultiTenant>())
            {
                return await _itemRepository.GetCountAsync();
            }
        }
        */
    }
}
