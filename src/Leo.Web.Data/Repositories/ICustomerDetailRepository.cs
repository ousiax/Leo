using Leo.Data.Domain.Entities;

namespace Leo.Web.Data
{
    public interface ICustomerDetailRepository
    {
        Task<IEnumerable<CustomerDetail>> GetByCustomerIdAsync(string customerId);

        Task<CustomerDetail?> GetByIdAsync(string id);

        Task<string> CreateAsync(CustomerDetail detail);
    }
}
