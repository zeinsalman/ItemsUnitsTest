using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ItemsFeatures.Items
{
    public class CreateItemDto
    {
        public string Name { get; set; }
        [Required]
        public Guid UnitId { get; set; }
    }
}
