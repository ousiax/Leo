using Alyio.Extensions;
using Leo.Web.Data.Models;
using System.Data;
using System.Data.SQLite;

namespace Leo.Web.Data.Services
{
    internal sealed class MemberDetailService : IMemberDetailService
    {
        private readonly IDbConnectionManager _dbConnectionManager;

        public MemberDetailService(IDbConnectionManager dbConnectionManager)
        {
            _dbConnectionManager = dbConnectionManager;
        }

        public async Task<Guid> CreateAsync(MemberDetail detail)
        {
            detail.Id = Guid.NewGuid();

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
            await cmd.ExecuteNonQueryAsync().ConfigureAwait(false);
            return detail.Id;
        }

        public async Task<MemberDetail?> GetByIdAsync(Guid id)
        {
            var detail = null as MemberDetail;
            using var conn = await _dbConnectionManager.OpenAsync().ConfigureAwait(false);
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT * FROM member_detail WHERE id = @id";
            cmd.Parameters.Add(new SQLiteParameter("@id") { DbType = DbType.String, Value = id });
            using (var reader = await cmd.ExecuteReaderAsync().ConfigureAwait(false))
            {
                if (reader.Read())
                {
                    detail = new MemberDetail
                    {
                        Id = Guid.Parse(reader["id"].ToString()),
                        Date = reader["date"].ToDateTime(),
                        Item = reader["item"].ToString(),
                        Count = reader["count"].ToInt32() ?? 0,
                        Height = reader["height"].ToDouble() ?? 0.0d,
                        Weight = reader["weight"].ToDouble() ?? 0.0d
                    };
                }
            }
            return detail;
        }

        public async Task<List<MemberDetail>> GetByMemberIdAsync(Guid memberId)
        {
            var members = new List<MemberDetail>();
            using var conn = await _dbConnectionManager.OpenAsync().ConfigureAwait(false);
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT * FROM member_detail WHERE id = @id";
            cmd.Parameters.Add(new SQLiteParameter("@id") { DbType = DbType.String, Value = memberId });
            using (var reader = await cmd.ExecuteReaderAsync().ConfigureAwait(false))
            {
                while (reader.Read())
                {
                    members.Add(new MemberDetail
                    {
                        Id = Guid.Parse(reader["id"].ToString()),
                        Date = reader["date"].ToDateTime(),
                        Item = reader["item"].ToString(),
                        Count = reader["count"].ToInt32() ?? 0,
                        Height = reader["height"].ToDouble() ?? 0.0d,
                        Weight = reader["weight"].ToDouble() ?? 0.0d
                    });
                }
            }
            return members;
        }
    }
}
