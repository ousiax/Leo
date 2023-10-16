using HoneyLovely.Services;
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
            Application.Run(mainFrm);
        }
    }
}
