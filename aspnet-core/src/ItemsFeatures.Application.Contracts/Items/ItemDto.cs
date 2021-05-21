using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace ItemsFeatures.Items
{
    public class ItemDto : AuditedEntityDto<Guid>
    {
        //public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid? TenantId { get; set; }
        public Guid UnitId { get; set; }
        public string UnitName { get; set; }

    }
}
