using Leo.Data.Domain.Dtos;
using MediatR;
using System.Security.Claims;

namespace Leo.Web.Data.Commands
{
    public sealed class CreateMemberDetailRequest : IRequest<Guid>
    {
        public MemberDetailDto? MemberDetailDto { get; set; }

        public ClaimsPrincipal? User { get; set; }
    }
}
