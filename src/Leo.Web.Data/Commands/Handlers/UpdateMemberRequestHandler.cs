using AutoMapper;
using Leo.Data.Domain;
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
            if (request.User != null)
            {
                member.Update(request.User);
            }
            await _memberService.UpdateAsync(member).ConfigureAwait(false);
            return Unit.Value;
        }
    }
}
