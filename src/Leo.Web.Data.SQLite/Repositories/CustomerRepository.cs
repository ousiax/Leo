// MIT License

using Dapper;
using Leo.Data.Domain.Entities;
using System.Data;

namespace Leo.Web.Data.SQLite.Repositories
{
    internal sealed class CustomerRepository : ICustomerRepository
    {
        private readonly IDbConnectionFactory _dbConnectionManager;

        public CustomerRepository(IDbConnectionFactory dbConnectionManager)
        {
            _dbConnectionManager = dbConnectionManager;
        }

        public async Task<Customer?> GetAsync(Guid id)
        {
            var commandText = "SELECT * FROM customer "
                + "WHERE id = @id";
            var parameters = new DynamicParameters();
            parameters.Add("id", id, DbType.String);
            var cmdDef = new CommandDefinition(commandText, parameters);
            using var conn = await _dbConnectionManager.OpenAsync().ConfigureAwait(false);
            return await conn.QueryFirstOrDefaultAsync<Customer>(cmdDef).ConfigureAwait(false);
        }

        public async Task<IEnumerable<Customer>> GetAsync()
        {
            var commandText = "SELECT * FROM customer";
            var cmdDef = new CommandDefinition(commandText: commandText);
            using var conn = await _dbConnectionManager.OpenAsync().ConfigureAwait(false);
            return await conn.QueryAsync<Customer>(cmdDef).ConfigureAwait(false);
        }

        public async Task<Guid> CreateAsync(Customer customer)
        {
            customer.Id = Guid.NewGuid();

            var commandText = "INSERT INTO customer (id, name, phone, gender, birthday, cardno,"
                + "created_at, created_by) "
                + "VALUES (@id, @name, @phone, @gender, @birthday, @cardno, "
                + "@created_at, @created_by)";
            var parameters = new DynamicParameters();
            parameters.Add("id", customer.Id, DbType.String);
            parameters.Add("name", customer.Name);
            parameters.Add("phone", customer.Phone);
            parameters.Add("gender", customer.Gender);
            parameters.Add("birthday", customer.Birthday);
            parameters.Add("cardno", customer.CardNo);
            parameters.Add("created_at", customer.CreatedAt);
            parameters.Add("created_by", customer.CreatedBy);
            var cmdDef = new CommandDefinition(commandText, parameters);
            using var conn = await _dbConnectionManager.OpenAsync().ConfigureAwait(false);
            await conn.ExecuteAsync(cmdDef).ConfigureAwait(false);
            return customer.Id;
        }

        public async Task UpdateAsync(Customer customer)
        {
            var commandText = "UPDATE customer "
                + "SET name=@name, phone=@phone, gender=@gender, birthday=@birthday, cardno=@cardno, "
                + "updated_at = @updated_at, updated_by = @updated_by "
                + "WHERE id=@id";
            var parameters = new DynamicParameters();
            parameters.Add("id", customer.Id, DbType.String);
            parameters.Add("name", customer.Name);
            parameters.Add("phone", customer.Phone);
            parameters.Add("gender", customer.Gender);
            parameters.Add("birthday", customer.Birthday);
            parameters.Add("cardno", customer.CardNo);
            parameters.Add("updated_at", customer.UpdatedAt);
            parameters.Add("updated_by", customer.UpdatedBy);
            var cmdDef = new CommandDefinition(commandText, parameters);
            using var conn = await _dbConnectionManager.OpenAsync().ConfigureAwait(false);
            await conn.ExecuteAsync(cmdDef).ConfigureAwait(false);
        }
    }
}
