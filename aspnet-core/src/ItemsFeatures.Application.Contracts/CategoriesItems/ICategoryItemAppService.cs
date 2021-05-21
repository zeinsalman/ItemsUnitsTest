using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace ItemsFeatures.CategoriesItems
{
    public interface ICategoryItemAppService :
        ICrudAppService<
            CategoryItemDto,
            Guid,
            PagedAndSortedResultRequestDto,
            CreateUpdateCategoryItemDto
            >
    {
    }
}
