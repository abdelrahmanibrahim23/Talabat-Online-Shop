using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Talabat.Core.Entity.Identity;
using Talabat.Repository.Data;
using Talabat.Repository.Identity;
using Talabat.Repository.Identity.DataSeeding;

namespace Talabat
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            var scope=host.Services.CreateScope();
            var services = scope.ServiceProvider;
            var loggerFactory=services.GetRequiredService<ILoggerFactory>();
            try
            {
                var context =services.GetRequiredService<StoryContext>();
                await context.Database.MigrateAsync();
                await storeContextSeed.SeedAsync(context, loggerFactory);
                var contextIdentity = services.GetRequiredService<AppUserIdentityDbContext>();
                await contextIdentity.Database.MigrateAsync();
                var userManager = services.GetRequiredService<UserManager<AppUser>>();
                await AppUserSeeding.SeedAsync(userManager);
            }
            catch (Exception ex)
            {
                var logger=loggerFactory.CreateLogger<StoryContext>();
                logger.LogError(ex, " Error Migration");

            }
            host.Run();

        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
