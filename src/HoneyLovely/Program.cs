using HoneyLovely.Options;
using HoneyLovely.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

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
                    builder.UseStartup<HoneyLovely.Web.Startup>();
                })
                .ConfigureAppConfiguration((ctx, conf) =>
                {
                })
                .ConfigureServices(services =>
                {
                    services.Configure<WebHostAddressOptions>(_ => { });
                    //services.AddSingleton<IWebHostAddressProvider, WebHostAddressProvider>();
                    services.AddSingleton<IMemberService, MemberService>();
                    services.AddSingleton<IMemberDetailService, MemberDetailService>();
                    services.AddSingleton<MainForm>();
                    services.AddHttpClient();
                })
                .Build();

            await host.StartAsync();

            using (var scope = host.Services.CreateScope())
            {
                await scope.ServiceProvider.GetRequiredService<HoneyLovely.Web.IDatabaseService>().InitializeAsync();

                var address = scope.ServiceProvider.GetRequiredService<IServer>().Features.Get<IServerAddressesFeature>().Addresses.First();
                scope.ServiceProvider.GetRequiredService<IOptions<WebHostAddressOptions>>().Value.BaseAddress = new Uri(address);
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            var mainFrm = host.Services.GetRequiredService<MainForm>();
            // https://learn.microsoft.com/en-us/dotnet/core/extensions/generic-host
            // When a host starts, it calls IHostedService.StartAsync on each implementation of IHostedService registered
            // in the service container's collection of hosted services. In a worker service app, all IHostedService
            // implementations that contain BackgroundService instances have their BackgroundService.ExecuteAsync methods called.
            //
            // mainFrm.Load += async (s, e) => await host.StartAsync();
            mainFrm.FormClosed += async (s, e) => await host.StopAsync();
            Application.Run(mainFrm);
        }
    }
}
