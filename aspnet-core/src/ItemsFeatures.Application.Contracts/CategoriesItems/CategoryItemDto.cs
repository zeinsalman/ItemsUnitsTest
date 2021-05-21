using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace ItemsFeatures.CategoriesItems
{
    public class CategoryItemDto : AuditedEntityDto<Guid>
    {
        //public Guid Id { get; set; }
        public Guid ItemId { get; set; }
        public Guid CategoryId { get; set; }
        public string ItemName { get; set; }
        public string CategoryName { get; set; }
    }
}
