namespace Leo.Web.Data.SqlServer.Services
{
    internal class DatabaseService : IDatabaseService
    {
        public Task InitializeAsync()
        {
            return Task.CompletedTask;
        }
    }
}
