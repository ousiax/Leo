using Leo.Data.Domain.Dtos;
using MediatR;

namespace Leo.Web.Data.Queries
{
    public sealed class GetMembersRequest : IRequest<List<MemberDto>>
    {
    }
}
