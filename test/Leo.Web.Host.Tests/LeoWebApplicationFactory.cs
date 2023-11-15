using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Leo.Web.Host.Tests
{
    public sealed class LeoWebApplicationFactory<TEntryPoint> : WebApplicationFactory<TEntryPoint> where TEntryPoint : class
    {

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            //builder.UseStartup<Startup>();

            builder.ConfigureTestServices(services =>
            {
                //services.AddLeoDomain();
                services.PostConfigureAll<AuthenticationOptions>(options =>
                {
                    options.DefaultAuthenticateScheme = LeoAuthenticationHandler.AuthenticationScheme;
                    options.DefaultChallengeScheme = LeoAuthenticationHandler.AuthenticationScheme;
                    options.DefaultScheme = LeoAuthenticationHandler.AuthenticationScheme;
                });
                //services.RemoveAll<AuthenticationHandler<AuthenticationSchemeOptions>>();
                services.AddAuthentication(LeoAuthenticationHandler.AuthenticationScheme)
                .AddScheme<AuthenticationSchemeOptions, LeoAuthenticationHandler>(
                  LeoAuthenticationHandler.AuthenticationScheme,
                  options => { });
            });
        }

        protected override IHost CreateHost(IHostBuilder builder)
        {
            var host = base.CreateHost(builder);

            //using (var scope = host.Services.CreateScope())
            //{
            //    scope.ServiceProvider.GetRequiredService<Leo.Web.Data.IDatabaseService>().InitializeAsync().RunSynchronously();
            //}

            return host;
        }
    }
}
