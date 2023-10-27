using Alyio.AspNetCore.ApiMessages;
using AutoMapper;
using Leo.Data.Domain.Dtos;
using Leo.Data.Domain.Models;
using Leo.Web.Data;
using Microsoft.AspNetCore.Mvc;

namespace Leo.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MembersController : ControllerBase
    {
        private readonly ILogger<MembersController> _logger;
        private readonly IMemberService _memberService;
        private readonly IMemberDetailService _memberDetailService;
        private readonly IMapper _mapper;

        public MembersController(
            IMemberService memberService,
            IMemberDetailService memberDetailService,
            IMapper mapper,
            ILogger<MembersController> logger)
        {
            _memberService = memberService;
            _memberDetailService = memberDetailService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        public async Task<List<MemberDto>> GetAsync()
        {
            var members = await _memberService.GetAsync();
            return _mapper.Map<List<MemberDto>>(members);
        }

        [HttpGet("{id}")]
        public async Task<MemberDto?> GetAsync(Guid id)
        {
            var member = await _memberService.GetAsync(id) ?? throw new NotFoundMessage();
            return _mapper.Map<MemberDto?>(member);
        }

        [HttpPost]
        public async Task<CreatedMessage> CreateAsync([FromBody] MemberDto memberDto)
        {
            var member = _mapper.Map<Member>(memberDto);
            var id = await _memberService.CreateAsync(member);
            return this.CreatedMessageAtAction(nameof(GetAsync), new { id }, id.ToString());
        }

        [HttpPut]
        public Task UpdateAsync([FromBody] MemberDto memberDto)
        {
            var member = _mapper.Map<Member>(memberDto);
            return _memberService.UpdateAsync(member);
        }

        [HttpGet("{id}/details")]
        public async Task<List<MemberDetailDto>> GetByMemberIdAsync(Guid id)
        {
            var details = await _memberDetailService.GetByMemberIdAsync(id);
            return _mapper.Map<List<MemberDetailDto>>(details);
        }
    }
}