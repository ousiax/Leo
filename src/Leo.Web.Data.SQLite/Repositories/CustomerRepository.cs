// MIT License

using System.Data;
using System.Data.Common;
using Dapper;
using Leo.Data.Domain.Entities;

namespace Leo.Web.Data.SQLite.Repositories
{
    internal sealed class CustomerRepository(IDbConnectionFactory dbConnectionManager) : ICustomerRepository
    {
        public async Task<Customer?> GetAsync(Guid id)
        {
            string commandText = "SELECT * FROM customer "
                + "WHERE id = @id";
            var parameters = new DynamicParameters();
            parameters.Add("id", id, DbType.String);
            var cmdDef = new CommandDefinition(commandText, parameters);
            using DbConnection conn = await dbConnectionManager.OpenAsync().ConfigureAwait(false);
            return await conn.QueryFirstOrDefaultAsync<Customer>(cmdDef).ConfigureAwait(false);
        }

        public async Task<IEnumerable<Customer>> GetAsync()
        {
            string commandText = "SELECT * FROM customer";
            var cmdDef = new CommandDefinition(commandText: commandText);
            using DbConnection conn = await dbConnectionManager.OpenAsync().ConfigureAwait(false);
            return await conn.QueryAsync<Customer>(cmdDef).ConfigureAwait(false);
        }

        public async Task<Guid> CreateAsync(Customer customer)
        {
            customer.Id = Guid.NewGuid();

            string commandText = "INSERT INTO customer (id, name, phone, gender, birthday, cardno,"
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
            using DbConnection conn = await dbConnectionManager.OpenAsync().ConfigureAwait(false);
            await conn.ExecuteAsync(cmdDef).ConfigureAwait(false);
            return customer.Id;
        }

        public async Task UpdateAsync(Customer customer)
        {
            string commandText = "UPDATE customer "
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
            using DbConnection conn = await dbConnectionManager.OpenAsync().ConfigureAwait(false);
            await conn.ExecuteAsync(cmdDef).ConfigureAwait(false);
        }
    }
}
