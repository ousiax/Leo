using Leo.Data.Domain.Dtos;
using MediatR;

namespace Leo.Web.Data.Commands
{
    public sealed class CreateMemberRequest : IRequest<Guid>
    {
        public MemberDto MemberDto { get; set; } = null!;
    }
}
