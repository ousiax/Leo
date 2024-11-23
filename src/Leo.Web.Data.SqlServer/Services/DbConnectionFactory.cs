// MIT License

using System.Data.Common;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Leo.Web.Data.SqlServer.Services
{
    internal sealed class DbConnectionFactory(IConfiguration configuration) : IDbConnectionFactory
    {
        public DbConnection Open()
        {
            SqlConnection conn = GetDbConnection();
            conn.Open();
            return conn;
        }

        public async Task<DbConnection> OpenAsync()
        {
            SqlConnection conn = GetDbConnection();
            await conn.OpenAsync().ConfigureAwait(false);

            return conn;
        }

        private SqlConnection GetDbConnection()
        {
            return new SqlConnection(configuration.GetConnectionString("mssql"));
        }
    }
}
