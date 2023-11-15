using AutoMapper;
using Leo.Data.Domain.Dtos;
using MediatR;

namespace Leo.Web.Data.Queries.Handlers
{
    internal class GetMemberByIdRequestHandler : IRequestHandler<GetMemberByIdRequest, MemberDto>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public GetMemberByIdRequestHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _uow = unitOfWork;
            _mapper = mapper;
        }

        public async Task<MemberDto> Handle(GetMemberByIdRequest request, CancellationToken cancellationToken)
        {
            if (request.Id == null)
            {
                throw new ArgumentNullException(nameof(request.Id));
            }

            var member = await _uow.MemberRepository.GetAsync(request.Id).ConfigureAwait(false);
            return _mapper.Map<MemberDto>(member);
        }
    }
}
