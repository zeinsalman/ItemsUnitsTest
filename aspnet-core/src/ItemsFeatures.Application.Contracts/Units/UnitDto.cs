using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace ItemsFeatures.Units
{
    public class UnitDto : AuditedEntityDto<Guid>
    {
        public string Name { get; set; }
    }
}
