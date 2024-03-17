// MIT License

using Leo.UI.Services.Models;

namespace Leo.UI.Services
{
    public interface ICustomerService
    {
        Task<CustomerDto?> GetAsync(string id);

        Task<List<CustomerDto>> GetAsync();

        Task<string?> CreateAsync(CustomerDto customer);

        Task<int> UpdateAsync(CustomerDto customer);
    }
}
