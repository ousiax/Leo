// MIT License

using System.Data.Common;
using System.Data.SQLite;
using Microsoft.Extensions.Configuration;

namespace Leo.Web.Data.SQLite.Services
{
    internal sealed class DbConnectionFactory(IConfiguration configuration) : IDbConnectionFactory
    {
        public DbConnection Open()
        {
            return GetDbConnection().OpenAndReturn();
        }

        public async Task<DbConnection> OpenAsync()
        {
            SQLiteConnection conn = GetDbConnection();
            await conn.OpenAsync().ConfigureAwait(false);

            return conn;
        }

        private SQLiteConnection GetDbConnection()
        {
            return new SQLiteConnection(configuration.GetConnectionString("sqlite"));
        }
    }
}
