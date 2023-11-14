using Leo.Data.Domain.Dtos;
using MediatR;
using System.Security.Claims;

namespace Leo.Web.Data.Commands
{
    public sealed class UpdateMemberRequest : IRequest<Unit>
    {
        public MemberDto? MemberDto { get; set; }

        public ClaimsPrincipal? User { get; set; }
    }
}
