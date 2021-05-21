using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace ItemsFeatures.Categories
{
    public class CategoryDto : AuditedEntityDto<Guid>
    {
        [Required]
        [StringLength(128)]
        public string Name { get; set; }
        public Guid? ParentId { get; set; }
       // public string ParentName { get; set; }



    }
}
