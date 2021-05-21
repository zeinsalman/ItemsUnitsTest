using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace ItemsFeatures.Items
{
    public class GetItemListDto : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}
