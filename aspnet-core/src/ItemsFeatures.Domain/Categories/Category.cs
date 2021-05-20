using ItemsFeatures.CategoriesItems;
using ItemsFeatures.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace ItemsFeatures.Categories
{
    public class Category : AuditedAggregateRoot<Guid>
    {
        public string Name { get; set; }
        public Guid ? ParentId { get; set; }
        public Category ParentCategory { get; set; }
        public ICollection<CategoryItem> CategoriesItems { get; set; }
        public ICollection<Category> SubCategories { get; set; }


    }
}
