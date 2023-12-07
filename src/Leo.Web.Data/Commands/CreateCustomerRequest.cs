using Leo.Data.Domain.Dtos;
using MediatR;
using System.Security.Claims;

namespace Leo.Web.Data.Commands
{
    public sealed class CreateCustomerRequest : IRequest<string>
    {
        public CustomerDto CustomerDto { get; set; } = null!;

        public ClaimsPrincipal? User { get; set; }
    }
}
