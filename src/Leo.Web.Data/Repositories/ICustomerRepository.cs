using Leo.Data.Domain.Entities;

namespace Leo.Web.Data
{
    public interface ICustomerRepository
    {
        Task<Customer?> GetAsync(Guid id);

        Task<IEnumerable<Customer>> GetAsync();

        Task<Guid> CreateAsync(Customer customer);

        Task UpdateAsync(Customer customer);
    }
}
