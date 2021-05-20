using ItemsFeatures.Categories;
using ItemsFeatures.CategoriesItems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace ItemsFeatures.Items
{
    public class Item : AuditedAggregateRoot<Guid>, IMultiTenant
    {
        public string Name { get; set; }

        public Guid UnitId { get; set; }
        public Guid? TenantId { get; set; }

        public ICollection<CategoryItem> CategoriesItems { get; set; }

        public Item()
        {

        }

        public Item(Guid unitId , Guid? tenantId , string name)
        {
            Name = name;
            TenantId = tenantId;
            UnitId = unitId;
        }



    }
}
