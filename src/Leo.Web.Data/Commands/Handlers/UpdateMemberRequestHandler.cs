using AutoMapper;
using Leo.Data.Domain;
using Leo.Data.Domain.Entities;
using MediatR;

namespace Leo.Web.Data.Commands.Handlers
{
    internal sealed class UpdateMemberRequestHandler : IRequestHandler<UpdateMemberRequest, Unit>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public UpdateMemberRequestHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _uow = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateMemberRequest request, CancellationToken cancellationToken)
        {
            var member = _mapper.Map<Member>(request.MemberDto);
            if (request.User != null)
            {
                member.Update(request.User);
            }
            await _uow.MemberRepository.UpdateAsync(member).ConfigureAwait(false);
            return Unit.Value;
        }
    }
}
