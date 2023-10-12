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
        static void Main(string[] args)
        {
            var host = Host.CreateDefaultBuilder(args)
                .ConfigureServices(services =>
                {
                    services.AddSingleton<MainForm>();
                })
                .Build();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            var mainFrm = host.Services.GetRequiredService<MainForm>();
            Application.Run(mainFrm);
        }
    }
}
