using Leo.Data.Domain.Dtos;
using MediatR;

namespace Leo.Web.Data.Commands
{
    public sealed class UpdateMemberRequest : IRequest<Unit>
    {
        public MemberDto? MemberDto { get; set; }
    }
}
