using Leo.Data.Domain.Dtos;
using MediatR;
using System.Security.Claims;

namespace Leo.Web.Data.Commands
{
    public sealed class CreateMemberRequest : IRequest<Guid>
    {
        public MemberDto MemberDto { get; set; } = null!;

        public ClaimsPrincipal? User { get; set; }
    }
}
