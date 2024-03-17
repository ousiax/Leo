// MIT License

using Leo.Data.Domain.Dtos;
using MediatR;
using System.Security.Claims;

namespace Leo.Web.Data.Commands
{
    public sealed class CreateCustomerDetailRequest : IRequest<Guid>
    {
        public CustomerDetailDto? CustomerDetailDto { get; set; }

        public ClaimsPrincipal? User { get; set; }
    }
}
