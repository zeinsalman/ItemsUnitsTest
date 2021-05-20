using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace ItemsFeatures.EntityFrameworkCore
{
    /* This class is needed for EF Core console commands
     * (like Add-Migration and Update-Database commands) */
    public class ItemsFeaturesMigrationsDbContextFactory : IDesignTimeDbContextFactory<ItemsFeaturesMigrationsDbContext>
    {
        public ItemsFeaturesMigrationsDbContext CreateDbContext(string[] args)
        {
            ItemsFeaturesEfCoreEntityExtensionMappings.Configure();

            var configuration = BuildConfiguration();

            var builder = new DbContextOptionsBuilder<ItemsFeaturesMigrationsDbContext>()
                .UseSqlServer(configuration.GetConnectionString("Default"));

            return new ItemsFeaturesMigrationsDbContext(builder.Options);
        }

        private static IConfigurationRoot BuildConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../ItemsFeatures.DbMigrator/"))
                .AddJsonFile("appsettings.json", optional: false);

            return builder.Build();
        }
    }
}
