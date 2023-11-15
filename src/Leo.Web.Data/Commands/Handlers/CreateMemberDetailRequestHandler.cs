using AutoMapper;
using Leo.Data.Domain;
using Leo.Data.Domain.Entities;
using MediatR;

namespace Leo.Web.Data.Commands.Handlers
{
    internal class CreateMemberDetailRequestHandler : IRequestHandler<CreateMemberDetailRequest, string>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public CreateMemberDetailRequestHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _uow = unitOfWork;
            _mapper = mapper;
        }

        public Task<string> Handle(CreateMemberDetailRequest request, CancellationToken cancellationToken)
        {
            var detail = _mapper.Map<MemberDetail>(request.MemberDetailDto);
            if (request.User != null)
            {
                detail.Create(request.User);
            }
            return _uow.MemberDetailRepository.CreateAsync(detail);
        }
    }
}
