using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using ItemsFeatures.Units;
namespace ItemsFeatures.Units
{
    public  interface IUnitAppService : 
        ICrudAppService<
            UnitDto,
            Guid, 
            PagedAndSortedResultRequestDto, 
            CreatreOrUpdateUnitDto> 
    {
    }
}
