using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using OnlineStore.DB;
using OnlineStore.Models.IdentityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            //CreateHostBuilder(args).Build().Run();
            IHost host = CreateHostBuilder(args).Build();
            using (IServiceScope scope = host.Services.CreateScope())
            {
                IServiceProvider services = scope.ServiceProvider;

                try
                {
                    OnlineStoreContext context = services.GetRequiredService<OnlineStoreContext>();
                    IWebHostEnvironment environment = services.GetRequiredService<IWebHostEnvironment>();

                    //var userManager = services.GetRequiredService<UserManager<User>>();
                    //var rolesManager = services.GetRequiredService<RoleManager<IdentityRole>>();

                    await InitializeData.InitializeAsync(context, environment/*, userManager, rolesManager*/);

                    var userManager = services.GetRequiredService<UserManager<User>>();
                    var rolesManager = services.GetRequiredService<RoleManager<IdentityRole>>();
                    IConfiguration configuration = services.GetRequiredService<IConfiguration>();
                    await RoleInitializer.InitializeAsync(userManager, rolesManager, configuration);
                }
                catch(Exception ex)
                {
                    ILogger logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred seeding the DB.");
                }
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
