using Microsoft.Extensions.Configuration;
using System.Data.Common;
using System.Data.SQLite;

namespace Leo.Web.Data.Services
{
    internal class DbConnectionFactory : IDbConnectionFactory
    {
        private readonly IConfiguration _configuration;

        public DbConnectionFactory(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public DbConnection Open()
        {
            return GetDbConnection().OpenAndReturn();
        }

        public async Task<DbConnection> OpenAsync()
        {
            var conn = GetDbConnection();
            await conn.OpenAsync().ConfigureAwait(false);

            return conn;
        }

        private SQLiteConnection GetDbConnection()
        {
            return new SQLiteConnection(_configuration.GetConnectionString("sqlite"));
        }
    }
}
