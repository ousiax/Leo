namespace Leo.Web.Data
{
    public interface IUnitOfWork
    {
        ICustomerRepository CustomerRepository { get; }

        ICustomerDetailRepository CustomerDetailRepository { get; }
    }
}
