using ItemsFeatures.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace ItemsFeatures.Units
{
    public class EfCoreUnitRepository : EfCoreRepository<ItemsFeaturesDbContext, Unit, Guid>,
            IUnitRepository
    {
        public EfCoreUnitRepository(
          IDbContextProvider<ItemsFeaturesDbContext> dbContextProvider)
          : base(dbContextProvider)
        {
           
        }
    }
}
