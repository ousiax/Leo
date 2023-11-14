using Dapper;
using Leo.Data.Domain.Entities;
using System.Data;

namespace Leo.Web.Data.Services
{
    internal sealed class MemberDetailService : IMemberDetailService
    {
        private readonly IDbConnectionManager _dbConnectionManager;

        public MemberDetailService(IDbConnectionManager dbConnectionManager)
        {
            _dbConnectionManager = dbConnectionManager;
        }

        public async Task<string> CreateAsync(MemberDetail detail)
        {
            detail.Id = Guid.NewGuid().ToString();

            using var conn = await _dbConnectionManager.OpenAsync().ConfigureAwait(false);
            var parameters = new DynamicParameters();
            parameters.Add("id", detail.Id, dbType: DbType.String);
            parameters.Add("member_id", detail.MemberId);
            parameters.Add("date", detail.Date);
            parameters.Add("item", detail.Item);
            parameters.Add("count", detail.Count);
            parameters.Add("height", detail.Height);
            parameters.Add("weight", detail.Weight);
            parameters.Add("created_at", detail.CreatedAt);
            parameters.Add("created_by", detail.CreatedBy);
            var commandText = "INSERT INTO member_detail (id, member_id, date, item, count, height, weight,"
                + "created_at, created_by) "
                + "VALUES (@id, @member_id, @date, @item, @count, @height, @weight, "
                + "@created_at, @created_by)";
            var cmdDef = new CommandDefinition(commandText, parameters);
            await conn.ExecuteAsync(cmdDef).ConfigureAwait(false);
            return detail.Id;
        }

        public async Task<MemberDetail?> GetByIdAsync(string id)
        {
            var commandText = "SELECT * FROM member_detail WHERE id = @id";
            var parameters = new DynamicParameters();
            parameters.Add("id", id);
            var cmdDef = new CommandDefinition(commandText, parameters);
            using var conn = await _dbConnectionManager.OpenAsync().ConfigureAwait(false);
            return await conn.QueryFirstAsync<MemberDetail>(cmdDef).ConfigureAwait(false);
        }

        public async Task<IEnumerable<MemberDetail>> GetByMemberIdAsync(string memberId)
        {
            var commandText = "SELECT * FROM member_detail WHERE member_id = @member_id";
            var parameters = new DynamicParameters();
            parameters.Add("member_id", memberId);
            var cmdDef = new CommandDefinition(commandText, parameters);
            using var conn = await _dbConnectionManager.OpenAsync().ConfigureAwait(false);
            return await conn.QueryAsync<MemberDetail>(cmdDef).ConfigureAwait(false);
        }
    }
}
