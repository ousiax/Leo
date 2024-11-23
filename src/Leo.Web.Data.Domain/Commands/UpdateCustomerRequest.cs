// MIT License

using System.Security.Claims;
using Leo.Data.Domain.Dtos;
using MediatR;

namespace Leo.Web.Data.Commands
{
    public sealed class UpdateCustomerRequest : IRequest<Unit>
    {
        public CustomerDto? CustomerDto { get; set; }

        public ClaimsPrincipal? User { get; set; }
    }
}
