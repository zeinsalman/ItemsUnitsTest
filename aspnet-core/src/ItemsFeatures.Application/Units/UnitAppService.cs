using ItemsFeatures.Permissions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace ItemsFeatures.Units
{
    public class UnitAppService :
            CrudAppService<
            Unit, 
            UnitDto, 
            Guid, 
            PagedAndSortedResultRequestDto, 
            CreatreOrUpdateUnitDto>, 
        IUnitAppService 
    {
        public UnitAppService(IRepository<Unit, Guid> repository) : base(repository)
        {
               
            GetPolicyName = ItemsFeaturesPermissions.Unit.Default;
            GetListPolicyName = ItemsFeaturesPermissions.Unit.Default;
            CreatePolicyName = ItemsFeaturesPermissions.Unit.Create;
            UpdatePolicyName = ItemsFeaturesPermissions.Unit.Edit;
            DeletePolicyName = ItemsFeaturesPermissions.Unit.Delete;
            
            
        }
    }
}
