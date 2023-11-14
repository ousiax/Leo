using AutoMapper;
using Leo.Data.Domain.Entities;
using MediatR;

namespace Leo.Web.Data.Commands.Handlers
{
    internal sealed class CreateMemberRequestHandler : IRequestHandler<CreateMemberRequest, Guid>
    {
        private readonly IMemberService _memberService;
        private readonly IMapper _mapper;

        public CreateMemberRequestHandler(IMemberService memberService, IMapper mapper)
        {
            _memberService = memberService;
            _mapper = mapper;
        }

        public Task<Guid> Handle(CreateMemberRequest request, CancellationToken cancellationToken)
        {
            var member = _mapper.Map<Member>(request.MemberDto);
            return _memberService.CreateAsync(member);
        }
    }
}
