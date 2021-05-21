using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace ItemsFeatures.CategoriesItems
{
    public class CategoryItemAppService :
         CrudAppService<
            CategoryItem,
            CategoryItemDto,
            Guid,
            PagedAndSortedResultRequestDto,
            CreateUpdateCategoryItemDto>,
        ICategoryItemAppService
    {
        public CategoryItemAppService(IRepository<CategoryItem, Guid> repository)
          : base(repository)
        {

        }
    }
}
