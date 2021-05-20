using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace ItemsFeatures.Units
{
    public  interface IUnitAppService :
        ICrudAppService< //Defines CRUD methods
            UnitDto, //Used to show books
            Guid, //Primary key of the book entity
            PagedAndSortedResultRequestDto, //Used for paging/sorting
            CreatreOrUpdateUnitDto> //Used to create/update a book
    {
    }
}
