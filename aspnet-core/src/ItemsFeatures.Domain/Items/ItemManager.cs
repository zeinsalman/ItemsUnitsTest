using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.MultiTenancy;

namespace ItemsFeatures.Items
{
    public class ItemManager : DomainService
    {
        private readonly IRepository<Item, Guid> _itemRepository;
        private readonly IDataFilter _dataFilter;

        public ItemManager(IRepository<Item, Guid> itemRepository , IDataFilter dataFilter)
        {
            _itemRepository = itemRepository;
            _dataFilter = dataFilter;

        }

        public async Task<Item> CreateAsync(string name, Guid unitId)
        {
            var product = new Item(unitId, CurrentTenant.Id , name);
            return await _itemRepository.InsertAsync(product);
        }
        public async Task<long> GetItemCountAsync(Guid? tenantId)
        {
            using (CurrentTenant.Change(tenantId))
            {
                return await _itemRepository.GetCountAsync();
            }
        }

        public async Task<long> GetAllItemCountAsync()
        {
            using (_dataFilter.Disable<IMultiTenant>())
            {
                return await _itemRepository.GetCountAsync();
            }
        }
    }
}
