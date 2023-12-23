using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data.Common;

namespace Leo.Web.Data.SqlServer.Services
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
            var conn = GetDbConnection();
            conn.Open();
            return conn;
        }

        public async Task<DbConnection> OpenAsync()
        {
            var conn = GetDbConnection();
            await conn.OpenAsync().ConfigureAwait(false);

            return conn;
        }

        private SqlConnection GetDbConnection()
        {
            return new SqlConnection(_configuration.GetConnectionString("mssql"));
        }
    }
}
