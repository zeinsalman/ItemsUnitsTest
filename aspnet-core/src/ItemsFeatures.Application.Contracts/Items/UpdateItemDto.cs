using System;
using System.Collections.Generic;
using System.Text;

namespace ItemsFeatures.Items
{
    public class UpdateItemDto
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public Guid ? UnitId { get; set; }
    }
}
