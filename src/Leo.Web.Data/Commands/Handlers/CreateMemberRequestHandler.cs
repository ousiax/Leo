using AutoMapper;
using Leo.Data.Domain;
using Leo.Data.Domain.Entities;
using MediatR;

namespace Leo.Web.Data.Commands.Handlers
{
    internal sealed class CreateMemberRequestHandler : IRequestHandler<CreateMemberRequest, string>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public CreateMemberRequestHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _uow = unitOfWork;
            _mapper = mapper;
        }

        public Task<string> Handle(CreateMemberRequest request, CancellationToken cancellationToken)
        {
            var member = _mapper.Map<Member>(request.MemberDto);
            if (request.User != null)
            {
                member.Create(request.User);
            }
            return _uow.MemberRepository.CreateAsync(member);
        }
    }
}
