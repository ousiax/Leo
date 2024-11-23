// MIT License

namespace Leo.Web.Data.SqlServer.Services
{
    internal sealed class UnitOfWork(
        ICustomerRepository customerRepository,
        ICustomerDetailRepository customerDetailRepository) : IUnitOfWork
    {
        public ICustomerRepository CustomerRepository { get; } = customerRepository;

        public ICustomerDetailRepository CustomerDetailRepository { get; } = customerDetailRepository;
    }
}
