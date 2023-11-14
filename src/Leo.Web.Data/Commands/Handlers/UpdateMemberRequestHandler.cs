using AutoMapper;
using Leo.Data.Domain.Entities;
using MediatR;

namespace Leo.Web.Data.Commands.Handlers
{
    internal sealed class UpdateMemberRequestHandler : IRequestHandler<UpdateMemberRequest, Unit>
    {
        private readonly IMemberService _memberService;
        private readonly IMapper _mapper;

        public UpdateMemberRequestHandler(IMemberService memberService, IMapper mapper)
        {
            _memberService = memberService;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateMemberRequest request, CancellationToken cancellationToken)
        {
            var member = _mapper.Map<Member>(request.MemberDto);
            await _memberService.UpdateAsync(member).ConfigureAwait(false);
            return Unit.Value;
        }
    }
}
