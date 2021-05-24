using ItemsFeatures.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace ItemsFeatures.Units
{
    public class Unit : AuditedAggregateRoot<Guid>
    {
        public string Name { get; set; }
        public ICollection<Item> Items { get; set; }

    }
}
