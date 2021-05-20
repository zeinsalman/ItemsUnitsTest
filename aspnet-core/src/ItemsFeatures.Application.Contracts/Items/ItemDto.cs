using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace ItemsFeatures.Items
{
    public class ItemDto  : AuditedEntityDto<Guid>
    {
        public string Name { get; set; }
        
    }
}
