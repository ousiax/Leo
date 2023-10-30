using Leo.Data.Domain.Dtos;
using MediatR;

namespace Leo.Web.Data.Commands
{
    public sealed class CreateMemberDetailRequest : IRequest<Guid>
    {
        public MemberDetailDto? MemberDetailDto { get; set; }
    }
}
