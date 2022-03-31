using BlissShopping.Core.Database;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlissShopping.Core.Infrastructure.Configuration;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static void AddCustomDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<BlissShoppingContext>(opts =>
            {
                var connectionString = configuration.GetConnectionString("umbracoDbDSN");
                var migrationAssembly = typeof(BlissShoppingContext).Assembly.GetName().Name;

                opts.UseSqlServer(connectionString, sql =>
                {
                    sql.MigrationsAssembly(migrationAssembly);
                    sql.EnableRetryOnFailure(
                        maxRetryCount: 5,
                        maxRetryDelay: TimeSpan.FromSeconds(30),
                        errorNumbersToAdd: null);
                });
            });
        }

        public static void AddServiceOptions(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddOptions();
            services.Configure<DatabaseInitializationOptions>(configuration.GetSection("DatabaseInitialization"));
        }
    }
}
