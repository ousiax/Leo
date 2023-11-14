using AutoMapper;
using Leo.Data.Domain.Dtos;
using MediatR;

namespace Leo.Web.Data.Queries.Handlers
{
    internal class GetMemberDetailByIdRequestHandler : IRequestHandler<GetMemberDetailByIdRequest, MemberDetailDto>
    {
        private readonly IMemberDetailService _memberDetailService;
        private readonly IMapper _mapper;

        public GetMemberDetailByIdRequestHandler(IMemberDetailService memberDetailService, IMapper mapper)
        {
            _memberDetailService = memberDetailService;
            _mapper = mapper;
        }

        public async Task<MemberDetailDto> Handle(GetMemberDetailByIdRequest request, CancellationToken cancellationToken)
        {
            if (request.Id == null)
            {
                throw new ArgumentNullException(nameof(request.Id));
            }

            var detail = await _memberDetailService.GetByIdAsync(request.Id).ConfigureAwait(false);
            return _mapper.Map<MemberDetailDto>(detail);
        }
    }
}
