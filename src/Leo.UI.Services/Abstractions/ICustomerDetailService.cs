// MIT License

using Leo.UI.Services.Models;

namespace Leo.UI.Services
{
    public interface ICustomerDetailService
    {
        Task<CustomerDetailDto?> GetAsync(string id);

        Task<List<CustomerDetailDto>> GetByCustomerIdAsync(string customerId);

        Task<string?> CreateAsync(CustomerDetailDto detail);
    }
}
