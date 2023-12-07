using Leo.Data.Domain.Entities;

namespace Leo.Web.Data
{
    public interface ICustomerRepository
    {
        Task<Customer?> GetAsync(string id);

        Task<IEnumerable<Customer>> GetAsync();

        Task<string> CreateAsync(Customer customer);

        Task UpdateAsync(Customer customer);
    }
}
