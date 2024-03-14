// MIT License

using Leo.Data.Domain.Entities;

namespace Leo.Web.Data
{
    public interface ICustomerDetailRepository
    {
        Task<IEnumerable<CustomerDetail>> GetByCustomerIdAsync(Guid customerId);

        Task<CustomerDetail?> GetByIdAsync(Guid id);

        Task<Guid> CreateAsync(CustomerDetail detail);
    }
}
