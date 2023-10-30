using AutoMapper;
using Leo.Data.Domain.Dtos;
using MediatR;

namespace Leo.Web.Data.Queries.Handlers
{
    internal class GetMembersRequesttHandler : IRequestHandler<GetMembersRequest, List<MemberDto>>
    {
        private readonly IMemberService _memberService;
        private readonly IMapper _mapper;

        public GetMembersRequesttHandler(IMemberService memberService, IMapper mapper)
        {
            _memberService = memberService;
            _mapper = mapper;
        }

        public async Task<List<MemberDto>> Handle(GetMembersRequest request, CancellationToken cancellationToken)
        {
            var members = await _memberService.GetAsync().ConfigureAwait(false);
            return _mapper.Map<List<MemberDto>>(members);
        }
    }
}
