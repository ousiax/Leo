// MIT License

namespace Leo.Web.Data.SqlServer.Services
{
    internal sealed class DatabaseService : IDatabaseService
    {
        public Task InitializeAsync()
        {
            return Task.CompletedTask;
        }
    }
}
