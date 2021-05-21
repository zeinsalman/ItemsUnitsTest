using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace ItemsFeatures.Items
{
    public interface IItemAppService : IApplicationService
    {
        Task<ItemDto> GetAsync(Guid id);

        Task<PagedResultDto<ItemDto>> GetListAsync(GetItemListDto input);

        Task<ItemDto> CreateAsync(CreateItemDto input);

        Task<ItemDto> UpdateAsync(Guid id, UpdateItemDto input);

       // Task DeleteAsync(Guid id);
        Task<long> GetItemCountAsync(Guid? tenantId);
    }
}
