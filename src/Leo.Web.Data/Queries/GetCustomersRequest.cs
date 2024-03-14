// MIT License

using Leo.Data.Domain.Dtos;
using MediatR;

namespace Leo.Web.Data.Queries
{
    public sealed class GetCustomersRequest : IRequest<List<CustomerDto>>
    {
    }
}
