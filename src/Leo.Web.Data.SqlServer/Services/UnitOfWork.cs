// MIT License

namespace Leo.Web.Data.SqlServer.Services
{
    internal sealed class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(
            ICustomerRepository customerRepository,
            ICustomerDetailRepository customerDetailRepository)
        {
            CustomerRepository = customerRepository;
            CustomerDetailRepository = customerDetailRepository;
        }

        public ICustomerRepository CustomerRepository { get; }

        public ICustomerDetailRepository CustomerDetailRepository { get; }
    }
}
