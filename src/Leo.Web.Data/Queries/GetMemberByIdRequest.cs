using Leo.Data.Domain.Dtos;
using MediatR;

namespace Leo.Web.Data.Queries
{
    public sealed class GetMemberByIdRequest : IRequest<MemberDto>
    {
        public Guid Id { get; set; }
    }
}
