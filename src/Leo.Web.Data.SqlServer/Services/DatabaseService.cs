namespace Leo.Web.Data.Services
{
    internal class DatabaseService : IDatabaseService
    {
        public Task InitializeAsync()
        {
            return Task.CompletedTask;
        }
    }
}
