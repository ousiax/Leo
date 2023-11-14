using AutoMapper;
using Leo.Data.Domain.Dtos;
using MediatR;

namespace Leo.Web.Data.Queries.Handlers
{
    internal class GetMemberByIdRequestHandler : IRequestHandler<GetMemberByIdRequest, MemberDto>
    {
        private readonly IMemberService _memberService;
        private readonly IMapper _mapper;

        public GetMemberByIdRequestHandler(IMemberService memberService, IMapper mapper)
        {
            _memberService = memberService;
            _mapper = mapper;
        }

        public async Task<MemberDto> Handle(GetMemberByIdRequest request, CancellationToken cancellationToken)
        {
            if (request.Id == null)
            {
                throw new ArgumentNullException(nameof(request.Id));
            }

            var member = await _memberService.GetAsync(request.Id).ConfigureAwait(false);
            return _mapper.Map<MemberDto>(member);
        }
    }
}
