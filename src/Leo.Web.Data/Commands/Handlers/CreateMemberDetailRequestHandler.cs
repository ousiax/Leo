using AutoMapper;
using Leo.Data.Domain;
using Leo.Data.Domain.Entities;
using MediatR;

namespace Leo.Web.Data.Commands.Handlers
{
    internal class CreateMemberDetailRequestHandler : IRequestHandler<CreateMemberDetailRequest, string>
    {
        private readonly IMemberDetailRepository _memberDetailService;
        private readonly IMapper _mapper;

        public CreateMemberDetailRequestHandler(IMemberDetailRepository memberDetailService, IMapper mapper)
        {
            _memberDetailService = memberDetailService;
            _mapper = mapper;
        }

        public Task<string> Handle(CreateMemberDetailRequest request, CancellationToken cancellationToken)
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
