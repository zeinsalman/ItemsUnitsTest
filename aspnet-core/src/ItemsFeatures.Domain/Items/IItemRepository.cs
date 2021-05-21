using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace ItemsFeatures.Items
{
    public interface IItemRepository : IRepository<Item, Guid>
    {
  

        Task<List<Item>> GetListAsync(
            int skipCount,
            int maxResultCount,
            string sorting,
            string filter = null
        );

        Task<Item> CreateAsync(Guid unitId, Guid? tenantId, string name);
        Task<long> GetItemCountAsync(Guid? tenantId);

        Task<IQueryable<Item>> GetIQueryableItems();
    }
}
