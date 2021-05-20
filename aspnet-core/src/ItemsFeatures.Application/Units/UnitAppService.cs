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
            Unit, //The Book entity
            UnitDto, //Used to show books
            Guid, //Primary key of the book entity
            PagedAndSortedResultRequestDto, //Used for paging/sorting
            CreatreOrUpdateUnitDto>, //Used to create/update a book
        IUnitAppService //implement the IBookAppService
    {
        public UnitAppService(IRepository<Unit, Guid> repository) : base(repository)
        {
        }
    }
}
