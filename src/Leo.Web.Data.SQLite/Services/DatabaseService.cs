// MIT License

using Dapper;

namespace Leo.Web.Data.SQLite.Services
{
    // https://sqlite.org/datatype3.html
    internal sealed class DatabaseService(IDbConnectionFactory dbConnectionManager) : IDatabaseService
    {
        private const string CREATE_CUSTOMER_SQL = "CREATE TABLE IF NOT EXISTS customer (id text PRIMARY KEY, name text NOT NULL, phone text NOT NULL, gender text, birthday text, cardno text, created_at text, created_by text, updated_at text, updated_by text);";
        private const string CREATE_CUSTOMER_DETAIL_SQL = "CREATE TABLE IF NOT EXISTS customer_detail (id text NOT NULL, customer_id text NOT NULL, date text NOT NULL, item text NOT NULL, count integer, height number, weight number, created_at text, created_by text, updated_at text, updated_by text);";

        public async Task InitializeAsync()
        {
            using System.Data.Common.DbConnection conn = await dbConnectionManager.OpenAsync().ConfigureAwait(false);
            var cmdDef = new CommandDefinition(commandText: $"{CREATE_CUSTOMER_SQL}{CREATE_CUSTOMER_DETAIL_SQL}");
            await conn.ExecuteAsync(cmdDef).ConfigureAwait(false);
        }
    }
}
