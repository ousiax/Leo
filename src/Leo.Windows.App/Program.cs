using Leo.UI.Options;
using Leo.UI.Services;
using Leo.Windows.Forms;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace Leo.Windows
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
                    builder.UseStartup<Leo.Web.Startup>();
                })
                .ConfigureServices(services =>
                {
                    services.AddLeoDomain();
                    services.AddLeoViewModels();
                    services.AddAppServices().AddOptions<WebOptions>();
                    services.AddSingleton<MainForm>();
                    services.AddHttpClient();
                })
                .ConfigureLogging((ctx, logging) =>
                {
                    var logger = new StreamWriter(
                        new FileStream(
                            Path.Combine(ctx.HostingEnvironment.ContentRootPath, "app.log"),
                            FileMode.OpenOrCreate))
                    {
                        AutoFlush = true
                    };
                    // Redirect stdout, stderr to file stream.
                    Console.SetOut(logger);
                    Console.SetError(logger);
                })
                .Build();

            await host.StartAsync();

            using (var scope = host.Services.CreateScope())
            {
                await scope.ServiceProvider.GetRequiredService<Leo.Web.Data.IDatabaseService>().InitializeAsync();

                var address = scope.ServiceProvider.GetRequiredService<IServer>().Features.Get<IServerAddressesFeature>()!.Addresses.First();
                scope.ServiceProvider.GetRequiredService<IOptions<WebOptions>>().Value.BaseAddress = new Uri(address);
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
