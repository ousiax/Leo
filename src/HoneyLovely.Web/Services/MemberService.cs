using Alyio.Extensions;
using HoneyLovely.Web.Models;
using System.Data;
using System.Data.SQLite;

namespace HoneyLovely.Web.Services
{
    internal sealed class MemberService : IMemberService
    {
        private readonly IDbConnectionManager _dbConnectionManager;

        public MemberService(IDbConnectionManager dbConnectionManager)
        {
            _dbConnectionManager = dbConnectionManager;
        }

        public async Task<Member> GetAsync(Guid id)
        {
            var member = (Member)null;
            using var conn = await _dbConnectionManager.OpenAsync().ConfigureAwait(false);
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT * FROM member"
                + "WHERE id = @id";
            cmd.Parameters.Add(new SQLiteParameter("@id") { DbType = DbType.String, Value = id });
            using var reader = await cmd.ExecuteReaderAsync().ConfigureAwait(false);
            if (reader.Read())
            {
                member = new Member
                {
                    Id = Guid.Parse(reader["id"].ToString()),
                    Name = reader["name"].ToString(),
                    Birthday = reader["birthday"].ToDateTime() ?? DateTime.Now,
                    CardNo = reader["cardno"].ToString(),
                    Gender = reader["gender"].ToString(),
                    Phone = reader["phone"].ToString()
                };
            }
            return member;
        }

        public async Task<List<Member>> GetAsync()
        {
            var members = new List<Member>();
            using var conn = await _dbConnectionManager.OpenAsync().ConfigureAwait(false);
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT * FROM member";
            using (var reader = await cmd.ExecuteReaderAsync().ConfigureAwait(false))
            {
                while (reader.Read())
                {
                    members.Add(new Member
                    {
                        Id = Guid.Parse(reader["id"].ToString()),
                        Name = reader["name"].ToString(),
                        Birthday = reader["birthday"].ToDateTime() ?? DateTime.Now,
                        CardNo = reader["cardno"].ToString(),
                        Gender = reader["gender"].ToString(),
                        Phone = reader["phone"].ToString()
                    });
                }
            }

            return members;
        }

        public async Task<int> CreateAsync(Member member)
        {
            if (member.Id == Guid.Empty)
            {
                member.Id = Guid.NewGuid();
            }
            using var conn = await _dbConnectionManager.OpenAsync().ConfigureAwait(false);
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "INSERT INTO member (id, name , phone , gender , birthday , cardno) "
                + "VALUES (@id, @name , @phone , @gender , @birthday , @cardno)";
            cmd.Parameters.Add(new SQLiteParameter("@id") { DbType = DbType.String, Value = member.Id });
            cmd.Parameters.Add(new SQLiteParameter("@name") { DbType = DbType.String, Value = member.Name });
            cmd.Parameters.Add(new SQLiteParameter("@phone") { DbType = DbType.String, Value = member.Phone });
            cmd.Parameters.Add(new SQLiteParameter("@gender") { DbType = DbType.String, Value = member.Gender });
            cmd.Parameters.Add(new SQLiteParameter("@birthday") { DbType = DbType.String, Value = member.Birthday });
            cmd.Parameters.Add(new SQLiteParameter("@cardno") { DbType = DbType.String, Value = member.CardNo });
            return await cmd.ExecuteNonQueryAsync().ConfigureAwait(false);
        }

        public async Task<int> UpdateAsync(Member member)
        {
            using var conn = await _dbConnectionManager.OpenAsync().ConfigureAwait(false);
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "UPDATE member "
                + "SET name=@name , phone=@phone , gender=@gender , birthday=@birthday , cardno=@cardno "
                + "WHERE id=@id";
            cmd.Parameters.Add(new SQLiteParameter("@id") { DbType = DbType.String, Value = member.Id });
            cmd.Parameters.Add(new SQLiteParameter("@name") { DbType = DbType.String, Value = member.Name });
            cmd.Parameters.Add(new SQLiteParameter("@phone") { DbType = DbType.String, Value = member.Phone });
            cmd.Parameters.Add(new SQLiteParameter("@gender") { DbType = DbType.String, Value = member.Gender });
            cmd.Parameters.Add(new SQLiteParameter("@birthday") { DbType = DbType.String, Value = member.Birthday });
            cmd.Parameters.Add(new SQLiteParameter("@cardno") { DbType = DbType.String, Value = member.CardNo });
            return await cmd.ExecuteNonQueryAsync().ConfigureAwait(false);
        }
    }
}
