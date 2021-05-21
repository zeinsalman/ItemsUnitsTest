using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using ItemsFeatures.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.MultiTenancy;

namespace ItemsFeatures.Items
{
    public class EfCoreItemRepository : EfCoreRepository<ItemsFeaturesDbContext, Item, Guid>,
            IItemRepository
    {
        private readonly IDataFilter _dataFilter;
        public EfCoreItemRepository(
            IDbContextProvider<ItemsFeaturesDbContext> dbContextProvider, IDataFilter dataFilter)
            : base(dbContextProvider)
        {
            _dataFilter = dataFilter;
        }

        public async Task<Item> CreateAsync(Guid unitId, Guid? tenantId, string name)
        {
            var item = new Item(unitId, tenantId, name);
            return await InsertAsync(item);
         
        }

        public async Task<long> GetItemCountAsync(Guid? tenantId)
        {

            if (tenantId.HasValue)
            {
                using (CurrentTenant.Change(tenantId))
                {
                    return await GetCountAsync();
                }
            }
            else
            {
                using (_dataFilter.Disable<IMultiTenant>())
                {
                    return await GetCountAsync();
                }
            }
        }

        public async Task<List<Item>> GetListAsync(
            int skipCount,
            int maxResultCount,
            string sorting,
            string filter = null)
        {
            try
            {
                if (!CurrentTenant.Id.HasValue)
                {
                    using (_dataFilter.Disable<IMultiTenant>())
                    {
                        DbSet<Item> dbSet = await GetDbSetAsync();
                        /*
                        return await dbSet
                           .WhereIf(
                               !filter.IsNullOrWhiteSpace(),
                               item => item.Name.Contains(filter)
                            )
                           //.Include(x => x.Unit)
                           .OrderBy(sorting)
                           .Skip(skipCount)
                           .Take(maxResultCount)
                           .AsQueryable()
                           
                        
                       
                        */
                        return await dbSet
                           .WhereIf(
                               !filter.IsNullOrWhiteSpace(),
                               item => item.Name.Contains(filter)
                            )
                           
                           .OrderBy(sorting)
                           .Skip(skipCount)
                           .Take(maxResultCount)
                           .ToListAsync();
                    }
                }
                else
                {
                    DbSet<Item> dbSet = await GetDbSetAsync();
                    /*
                    return await dbSet
                           .WhereIf(
                               !filter.IsNullOrWhiteSpace(),
                               item => item.Name.Contains(filter)
                            )
                           //.Include(x => x.Unit)
                           .OrderBy(sorting)
                           .Skip(skipCount)
                           .Take(maxResultCount)
                           .AsQueryable()
                           
                    
                  

                    */

                    return await dbSet
                           .WhereIf(
                               !filter.IsNullOrWhiteSpace(),
                               item => item.Name.Contains(filter)
                            )
                           .OrderBy(sorting)
                           .Skip(skipCount)
                           .Take(maxResultCount)
                           .ToListAsync();
                }

            }
            catch (Exception ex)
            {

                throw;
            }
                

        }

        public async Task<Item> UpdateAsync(Guid itemId, Guid unitId, Guid? tenantId, string name)
        {
            var item = await GetAsync(itemId);
            item.UnitId = unitId;
            item.TenantId = tenantId;
            item.Name = name;
            item = await UpdateAsync(item, true);
            return item;
        }

        public async Task<IQueryable<Item>> GetIQueryableItems()
        {
            if (CurrentTenant.Id.HasValue)
            {
                return await GetQueryableAsync();
            }
            else
            {
                using (_dataFilter.Disable<IMultiTenant>())
                {
                    return await GetQueryableAsync();
                }
            }
        }
    }
}