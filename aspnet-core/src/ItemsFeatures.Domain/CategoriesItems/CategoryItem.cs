using ItemsFeatures.Categories;
using ItemsFeatures.Items;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace ItemsFeatures.CategoriesItems
{
    public class CategoryItem : AuditedAggregateRoot<Guid>
    {
        public Guid ItemId { get; set; }
        public Guid CategoryId { get; set; }
       
        public Item Item { get; set; }
        
        public Category Category { get; set; }

    }
}
