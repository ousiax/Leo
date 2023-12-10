using Leo.Data.Domain.Dtos;

namespace Leo.UI
{
    public interface ICustomerDetailService
    {
        Task<CustomerDetailDto?> GetAsync(string id);

        Task<List<CustomerDetailDto>> GetByCustomerIdAsync(string customerId);

        Task<string?> CreateAsync(CustomerDetailDto detail);
    }
}
