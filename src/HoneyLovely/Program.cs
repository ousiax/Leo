using HoneyLovely.Services;
using HoneyLovely.Web;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace HoneyLovely
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static async Task Main(string[] args)
        {
            var host = Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(builder =>
                {
                    builder.UseStartup<Startup>();
                })
                .ConfigureServices(services =>
                {
                    services.AddSingleton<IDbConnectionManager, DbConnectionManager>();
                    services.AddTransient<IDatabaseService, DatabaseService>();
                    services.AddSingleton<IMemberService, MemberService>();
                    services.AddSingleton<IMemberDetailService, MemberDetailService>();
                    services.AddSingleton<MainForm>();
                })
                .Build();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            using (var services = host.Services.CreateScope())
            {
                await services.ServiceProvider.GetRequiredService<IDatabaseService>().InitializeAsync();
            }
            var mainFrm = host.Services.GetRequiredService<MainForm>();

            // https://learn.microsoft.com/en-us/dotnet/core/extensions/generic-host
            // When a host starts, it calls IHostedService.StartAsync on each implementation of IHostedService registered
            // in the service container's collection of hosted services. In a worker service app, all IHostedService
            // implementations that contain BackgroundService instances have their BackgroundService.ExecuteAsync methods called.
            mainFrm.Load += async (s, e) => await host.StartAsync();
            mainFrm.FormClosed += async (s, e) => await host.StopAsync();

            Application.Run(mainFrm);
        }
    }
}
