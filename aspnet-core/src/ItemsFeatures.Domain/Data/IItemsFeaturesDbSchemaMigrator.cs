using System.Threading.Tasks;

namespace ItemsFeatures.Data
{
    public interface IItemsFeaturesDbSchemaMigrator
    {
        Task MigrateAsync();
    }
}
