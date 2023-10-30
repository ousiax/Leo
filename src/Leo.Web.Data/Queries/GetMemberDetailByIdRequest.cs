using Leo.Data.Domain.Dtos;
using MediatR;

namespace Leo.Web.Data.Queries
{
    public sealed class GetMemberDetailByIdRequest : IRequest<MemberDetailDto>
    {
        public Guid Id { get; set; }
    }
}
