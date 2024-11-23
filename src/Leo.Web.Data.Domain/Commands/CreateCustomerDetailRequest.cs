// MIT License

using System.Security.Claims;
using Leo.Data.Domain.Dtos;
using MediatR;

namespace Leo.Web.Data.Commands
{
    public sealed class CreateCustomerDetailRequest : IRequest<Guid>
    {
        public CustomerDetailDto? CustomerDetailDto { get; set; }

        public ClaimsPrincipal? User { get; set; }
    }
}
