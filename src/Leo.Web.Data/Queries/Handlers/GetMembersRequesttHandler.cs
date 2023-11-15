using AutoMapper;
using Leo.Data.Domain.Dtos;
using MediatR;

namespace Leo.Web.Data.Queries.Handlers
{
    internal class GetMembersRequesttHandler : IRequestHandler<GetMembersRequest, List<MemberDto>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public GetMembersRequesttHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _uow = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<MemberDto>> Handle(GetMembersRequest request, CancellationToken cancellationToken)
        {
            var members = await _uow.MemberRepository.GetAsync().ConfigureAwait(false);
            return _mapper.Map<List<MemberDto>>(members);
        }
    }
}
