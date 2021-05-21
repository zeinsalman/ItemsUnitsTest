using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace ItemsFeatures.Categories
{
    public class CreateUpdateCategoryDto 
    {
        //public Guid? Id { get; set; }
        [Required]
        [StringLength(128)]
        public string Name { get; set; }
        public Guid? ParentId { get; set; }
    }
}
