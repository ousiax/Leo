using AutoMapper;
using Leo.Data.Domain.Dtos;
using MediatR;

namespace Leo.Web.Data.Queries.Handlers
{
    internal class GetMemberDetailByIdRequestHandler : IRequestHandler<GetMemberDetailByIdRequest, MemberDetailDto>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public GetMemberDetailByIdRequestHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _uow = unitOfWork;
            _mapper = mapper;
        }

        public async Task<MemberDetailDto> Handle(GetMemberDetailByIdRequest request, CancellationToken cancellationToken)
        {
            if (request.Id == null)
            {
                throw new ArgumentNullException(nameof(request.Id));
            }

            var detail = await _uow.MemberDetailRepository.GetByIdAsync(request.Id).ConfigureAwait(false);
            return _mapper.Map<MemberDetailDto>(detail);
        }
    }
}
