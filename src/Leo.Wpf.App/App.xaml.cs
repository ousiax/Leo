// MIT License

using System.Globalization;
using System.IO;
using System.Windows;
using Leo.UI.Services;
using Leo.UI.Services.Options;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.Identity.Client;

namespace Leo.Wpf.App;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    private readonly IHost _host;

#if DEBUG
    static App()
    {
        CultureInfo.CurrentUICulture = CultureInfo.GetCultureInfo("en-US");
    }
#endif

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
                services.AddLeoViewModels();
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

        LoadApplicationResources();

        var mainWin = _host.Services.GetRequiredService<IMainWindowService>();
        mainWin.Show();

        base.OnStartup(e);
    }

    private void LoadApplicationResources()
    {
        var cultureInfo = CultureInfo.CurrentUICulture;
        while (!string.IsNullOrEmpty(cultureInfo.Name))
        {
            var uri = $"Resources/{cultureInfo.Name}/Localization.xaml";
            try
            {
                var localization = new ResourceDictionary
                {
                    Source = new Uri(uri, UriKind.RelativeOrAbsolute)
                };
                this.Resources.MergedDictionaries.Clear();
                this.Resources.MergedDictionaries.Add(localization);
                break;
            }
            catch
            {
                cultureInfo = cultureInfo.Parent;
            }
        }
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

