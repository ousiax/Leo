using AutoMapper;
using Leo.Data.Domain;
using Leo.Data.Domain.Entities;
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
            if (request.User != null)
            {
                detail.Create(request.User);
            }
            return _memberDetailService.CreateAsync(detail);
        }
    }
}
