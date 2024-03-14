// MIT License

using Leo.Data.Domain.Dtos;
using MediatR;

namespace Leo.Web.Data.Queries
{
    public sealed class GetCustomerByIdRequest : IRequest<CustomerDto>
    {
        public Guid Id { get; set; }
    }
}
