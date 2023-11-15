using Dapper;
using Leo.Data.Domain.Entities;
using System.Data;

namespace Leo.Web.Data.SQLite.Repositories
{
    internal sealed class MemberRepository : IMemberRepository
    {
        private readonly IDbConnectionFactory _dbConnectionManager;

        public MemberRepository(IDbConnectionFactory dbConnectionManager)
        {
            _dbConnectionManager = dbConnectionManager;
        }

        public async Task<Member?> GetAsync(string id)
        {
            var commandText = "SELECT * FROM member "
                + "WHERE id = @id";
            var parameters = new DynamicParameters();
            parameters.Add("id", id, dbType: DbType.String);
            var cmdDef = new CommandDefinition(commandText, parameters);
            using var conn = await _dbConnectionManager.OpenAsync().ConfigureAwait(false);
            return await conn.QueryFirstOrDefaultAsync<Member>(cmdDef).ConfigureAwait(false);
        }

        public async Task<IEnumerable<Member>> GetAsync()
        {
            var commandText = "SELECT * FROM member";
            var cmdDef = new CommandDefinition(commandText: commandText);
            using var conn = await _dbConnectionManager.OpenAsync().ConfigureAwait(false);
            return await conn.QueryAsync<Member>(cmdDef).ConfigureAwait(false);
        }

        public async Task<string> CreateAsync(Member member)
        {
            member.Id = Guid.NewGuid().ToString();

            var commandText = "INSERT INTO member (id, name, phone, gender, birthday, cardno,"
                + "created_at, created_by) "
                + "VALUES (@id, @name, @phone, @gender, @birthday, @cardno, "
                + "@created_at, @created_by)";
            var parameters = new DynamicParameters();
            parameters.Add("id", member.Id, dbType: DbType.String);
            parameters.Add("name", member.Name);
            parameters.Add("phone", member.Phone);
            parameters.Add("gender", member.Gender);
            parameters.Add("birthday", member.Birthday);
            parameters.Add("cardno", member.CardNo);
            parameters.Add("created_at", member.CreatedAt);
            parameters.Add("created_by", member.CreatedBy); ;
            var cmdDef = new CommandDefinition(commandText, parameters);
            using var conn = await _dbConnectionManager.OpenAsync().ConfigureAwait(false);
            await conn.ExecuteAsync(cmdDef).ConfigureAwait(false);
            return member.Id;
        }

        public async Task UpdateAsync(Member member)
        {
            var commandText = "UPDATE member "
                + "SET name=@name, phone=@phone, gender=@gender, birthday=@birthday, cardno=@cardno, "
                + "updated_at = @updated_at, updated_by = @updated_by "
                + "WHERE id=@id";
            var parameters = new DynamicParameters();
            parameters.Add("id", member.Id, dbType: DbType.String);
            parameters.Add("name", member.Name);
            parameters.Add("phone", member.Phone);
            parameters.Add("gender", member.Gender);
            parameters.Add("birthday", member.Birthday);
            parameters.Add("cardno", member.CardNo);
            parameters.Add("updated_at", member.UpdatedAt);
            parameters.Add("updated_by", member.UpdatedBy);
            var cmdDef = new CommandDefinition(commandText, parameters);
            using var conn = await _dbConnectionManager.OpenAsync().ConfigureAwait(false);
            await conn.ExecuteAsync(cmdDef).ConfigureAwait(false);
        }
    }
}
