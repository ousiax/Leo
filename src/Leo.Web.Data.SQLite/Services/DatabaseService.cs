using Dapper;

namespace Leo.Web.Data.Services
{
    // https://sqlite.org/datatype3.html
    internal class DatabaseService : IDatabaseService
    {
        private const string CREATE_CUSTOMER_SQL = "CREATE TABLE IF NOT EXISTS customer (id text PRIMARY KEY, name text NOT NULL, phone text NOT NULL, gender text, birthday text, cardno text, created_at text, created_by text, updated_at text, updated_by text);";
        private const string CREATE_CUSTOMER_DETAIL_SQL = "CREATE TABLE IF NOT EXISTS customer_detail (id text NOT NULL, customer_id text NOT NULL, date text NOT NULL, item text NOT NULL, count integer, height number, weight number, created_at text, created_by text, updated_at text, updated_by text);";

        private readonly IDbConnectionFactory _dbConnectionManager;

        public DatabaseService(IDbConnectionFactory dbConnectionManager)
        {
            _dbConnectionManager = dbConnectionManager;
        }

        public async Task InitializeAsync()
        {
            using var conn = await _dbConnectionManager.OpenAsync().ConfigureAwait(false);
            var cmdDef = new CommandDefinition(commandText: $"{CREATE_CUSTOMER_SQL}{CREATE_CUSTOMER_DETAIL_SQL}");
            await conn.ExecuteAsync(cmdDef).ConfigureAwait(false);
        }
    }
}
