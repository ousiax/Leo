using Leo.Data.Domain.Dtos;

namespace Leo.UI
{
    public interface ICustomerDetailService
    {
        Task<CustomerDetailDto?> GetAsync(Guid id);

        Task<List<CustomerDetailDto>> GetByCustomerIdAsync(Guid customerId);

        Task<string?> CreateAsync(CustomerDetailDto detail);
    }
}
