using Leo.Data.Domain.Dtos;
using MediatR;

namespace Leo.Web.Data.Queries
{
    public sealed class GetMemberDetailsByMemberIdRequest : IRequest<List<MemberDetailDto>>
    {
        public Guid MemberId { get; set; }
    }
}
