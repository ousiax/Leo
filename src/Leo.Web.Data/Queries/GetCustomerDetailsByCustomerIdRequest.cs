using Leo.Data.Domain.Dtos;
using MediatR;

namespace Leo.Web.Data.Queries
{
    public sealed class GetCustomerDetailsByCustomerIdRequest : IRequest<List<CustomerDetailDto>>
    {
        public string? CustomerId { get; set; }
    }
}
