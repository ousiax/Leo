using HoneyLovely.Models;
using System.Data;
using System.Data.SQLite;

namespace HoneyLovely.Services
{
    internal sealed class MemberDetailService : IMemberDetailService
    {
        private readonly IDbConnectionManager _dbConnectionManager;

        public MemberDetailService(IDbConnectionManager dbConnectionManager)
        {
            _dbConnectionManager = dbConnectionManager;
        }

        public async Task<int> CreateAsync(MemberDetail detail)
        {
            using var conn = await _dbConnectionManager.OpenAsync().ConfigureAwait(false);
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "INSERT INTO member_detail (id, date , item , count , height , weight) "
                + "VALUES (@id, @date , @item , @count , @height , @weight)";
            cmd.Parameters.Add(new SQLiteParameter("@id") { DbType = DbType.String, Value = detail.Id });
            cmd.Parameters.Add(new SQLiteParameter("@date") { DbType = DbType.String, Value = detail.Date });
            cmd.Parameters.Add(new SQLiteParameter("@item") { DbType = DbType.String, Value = detail.Item });
            cmd.Parameters.Add(new SQLiteParameter("@count") { DbType = DbType.String, Value = detail.Count });
            cmd.Parameters.Add(new SQLiteParameter("@height") { DbType = DbType.String, Value = detail.Height });
            cmd.Parameters.Add(new SQLiteParameter("@weight") { DbType = DbType.String, Value = detail.Weight });
            return await cmd.ExecuteNonQueryAsync().ConfigureAwait(false);
        }
    }
}
