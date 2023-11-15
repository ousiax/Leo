using Leo.Web.Data;

namespace Leo.Web
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddLeoDomain();

            var startup = new Startup(builder.Configuration);
            startup.ConfigureServices(builder.Services);

            var app = builder.Build();

            startup.Configure(app, app.Environment);

            using (var scope = app.Services.CreateScope())
            {
                var database = scope.ServiceProvider.GetRequiredService<IDatabaseService>();
                await database.InitializeAsync();
            }

            app.Run();
        }
    }
}
