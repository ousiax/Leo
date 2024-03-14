// MIT License

using Leo.Data.Domain.Dtos;
using MediatR;

namespace Leo.Web.Data.Queries
{
    public sealed class GetCustomerDetailByIdRequest : IRequest<CustomerDetailDto>
    {
        public Guid Id { get; set; }
    }
}
