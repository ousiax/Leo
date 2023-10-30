using AutoMapper;
using Leo.Data.Domain.Models;
using MediatR;

namespace Leo.Web.Data.Commands.Handlers
{
    internal class CreateMemberDetailRequestHandler : IRequestHandler<CreateMemberDetailRequest, Guid>
    {
        private readonly IMemberDetailService _memberDetailService;
        private readonly IMapper _mapper;

        public CreateMemberDetailRequestHandler(IMemberDetailService memberDetailService, IMapper mapper)
        {
            _memberDetailService = memberDetailService;
            _mapper = mapper;
        }

        public Task<Guid> Handle(CreateMemberDetailRequest request, CancellationToken cancellationToken)
        {
            var detail = _mapper.Map<MemberDetail>(request.MemberDetailDto);
            return _memberDetailService.CreateAsync(detail);
        }
    }
}
