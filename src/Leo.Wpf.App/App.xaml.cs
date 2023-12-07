using Leo.UI.Options;
using Leo.UI.Services;
using Leo.Wpf.App.Views;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.Identity.Client;
using System.IO;
using System.Windows;

namespace Leo.Wpf.App;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    private readonly IHost _host;

    public App()
    {
        _host = Host.CreateDefaultBuilder()
            .ConfigureWebHostDefaults(builder =>
            {
                builder.UseStartup<Leo.Web.Startup>();
            })
            .ConfigureServices((ctx, services) =>
            {
                services.AddLeoDomain();
                services.Configure<PublicClientApplicationOptions>(ctx.Configuration.GetSection(nameof(PublicClientApplicationOptions)));
                services.AddAppServices();
                services.AddHttpClient();
                services.AddSingleton<MainWindow>();
            })
            .ConfigureLogging((ctx, logging) =>
            {
                var logger = new StreamWriter(new FileStream(
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
    }

    protected override async void OnStartup(StartupEventArgs e)
    {
        await _host.StartAsync();

        using (var scope = _host.Services.CreateScope())
        {
            var auth = scope.ServiceProvider.GetRequiredService<IAuthenticationService>();
            await auth.ExecuteAsync();

            await scope.ServiceProvider.GetRequiredService<Leo.Web.Data.IDatabaseService>().InitializeAsync();

            var address = scope.ServiceProvider.GetRequiredService<IServer>().Features.Get<IServerAddressesFeature>()!.Addresses.First();
            scope.ServiceProvider.GetRequiredService<IOptions<WebOptions>>().Value.BaseAddress = new Uri(address);
        }

        var mainWin = _host.Services.GetRequiredService<MainWindow>();
        mainWin.Show();

        base.OnStartup(e);
    }

    protected override async void OnExit(ExitEventArgs e)
    {
        using (_host)
        {
            await _host.StopAsync();
        }
        base.OnExit(e);
    }
}

