using System;
using System.Collections.Generic;
using System.Text;

namespace ItemsFeatures.Categories
{
    public class CreateUpdateCategoryDto
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public Guid? ParentId { get; set; }
    }
}
