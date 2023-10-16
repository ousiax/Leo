namespace HoneyLovely.Web.Services
{
    internal class DatabaseService : IDatabaseService
    {
        private const string CREATE_MEMBER_SQL = "CREATE TABLE IF NOT EXISTS member (id text PRIMARY KEY, name text NOT NULL, phone text NOT NULL, gender text, birthday text, cardno text);";
        private const string CREATE_MEMBER_DETAIL_SQL = "CREATE TABLE IF NOT EXISTS member_detail (id text NOT NULL, date text NOT NULL, item text NOT NULL, count integer, height number, weight number);";

        private readonly IDbConnectionManager _dbConnectionManager;

        public DatabaseService(IDbConnectionManager dbConnectionManager)
        {
            _dbConnectionManager = dbConnectionManager;
        }

        public async Task InitializeAsync()
        {
            using var conn = await _dbConnectionManager.OpenAsync().ConfigureAwait(false);
            using var cmd = conn.CreateCommand();

            cmd.CommandText = CREATE_MEMBER_SQL;
            await cmd.ExecuteNonQueryAsync().ConfigureAwait(false);

            cmd.CommandText = CREATE_MEMBER_DETAIL_SQL;
            await cmd.ExecuteNonQueryAsync().ConfigureAwait(false);
        }
    }
}
