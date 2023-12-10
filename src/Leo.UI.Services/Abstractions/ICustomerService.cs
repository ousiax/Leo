using Leo.Data.Domain.Dtos;

namespace Leo.UI
{
    public interface ICustomerService
    {
        Task<CustomerDto?> GetAsync(string id);

        Task<List<CustomerDto>> GetAsync();

        Task<string?> CreateAsync(CustomerDto customer);

        Task<int> UpdateAsync(CustomerDto customer);
    }
}
