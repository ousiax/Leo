﻿using AutoMapper;
using Leo.Data.Domain.Dtos;
using MediatR;

namespace Leo.Web.Data.Queries.Handlers
{
    internal class GetMemberDetailsByMemberIdRequestHandler : IRequestHandler<GetMemberDetailsByMemberIdRequest, List<MemberDetailDto>>
    {
        private readonly IMemberDetailService _memberDetailService;
        private readonly IMapper _mapper;

        public GetMemberDetailsByMemberIdRequestHandler(IMemberDetailService memberDetailService, IMapper mapper)
        {
            _memberDetailService = memberDetailService;
            _mapper = mapper;
        }

        public async Task<List<MemberDetailDto>> Handle(GetMemberDetailsByMemberIdRequest request, CancellationToken cancellationToken)
        {
            var detail = await _memberDetailService.GetByMemberIdAsync(request.MemberId).ConfigureAwait(false);
            return _mapper.Map<List<MemberDetailDto>>(detail);
        }
    }
}