using System;
using System.Collections.Generic;
using System.Text;

namespace ItemsFeatures.CategoriesItems
{
    public class CreateUpdateCategoryItemDto
    {
        public Guid ItemId { get; set; }
        public Guid CategoryId { get; set; }
    }
}
