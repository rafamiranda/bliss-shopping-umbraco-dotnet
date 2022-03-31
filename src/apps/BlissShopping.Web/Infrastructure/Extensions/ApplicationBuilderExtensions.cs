using BlissShopping.Core.Database;
using BlissShopping.Core.Infrastructure.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Serilog;

namespace Microsoft.AspNetCore.Builder
{
    public static class ApplicationBuilderExtensions
    {
        public static void InitializeDb(this IApplicationBuilder app)
        {
            if (app is null) throw new ArgumentNullException(nameof(app));

            using var scope = app.ApplicationServices.CreateScope();
            var opts = scope.ServiceProvider.GetRequiredService<IOptions<DatabaseInitializationOptions>>().Value;
            var logger = scope.ServiceProvider.GetRequiredService<ILogger>().ForContext("SourceContext", "BlissShopping.DatabaseInitialization");
            InitializeBlissShoppingDb(scope, opts, logger);
        }

        private static void InitializeBlissShoppingDb(IServiceScope scope, DatabaseInitializationOptions opts, ILogger logger)
        {
            logger.Information("Checking config for AspNetCoreIdentity Database Initialization ...");
            var ctx = scope.ServiceProvider.GetRequiredService<BlissShoppingContext>();

            if (opts.BlissShopping.MigrationsEnabled)
            {
                logger.Information("Will try to apply migrations on AspNetIdentity DbContext");
                ctx.Database.Migrate();
            }
        }
    }
}
