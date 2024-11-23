// MIT License

using System.Security.Claims;
using Leo.Data.Domain.Dtos;
using MediatR;

namespace Leo.Web.Data.Commands
{
    public sealed class CreateCustomerRequest : IRequest<Guid>
    {
        public CustomerDto CustomerDto { get; set; } = null!;

        public ClaimsPrincipal? User { get; set; }
    }
}
