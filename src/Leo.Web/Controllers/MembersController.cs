using Leo.Web.Data;
using Leo.Web.Data.Models;
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

        public MembersController(IMemberService memberService, IMemberDetailService memberDetailService, ILogger<MembersController> logger)
        {
            _memberService = memberService;
            _memberDetailService = memberDetailService;
            _logger = logger;
        }

        [HttpGet]
        public Task<List<Member>> GetAsync()
        {
            return _memberService.GetAsync();
        }

        [HttpGet("{id}")]
        public Task<Member?> GetAsync(Guid id)
        {
            return _memberService.GetAsync(id);
        }

        [HttpPost]
        public Task<int> CreateAsync([FromBody] Member member)
        {
            return _memberService.CreateAsync(member);
        }

        [HttpPut]
        public Task<int> UpdateAsync([FromBody] Member member)
        {
            return _memberService.UpdateAsync(member);
        }

        [HttpGet("{id}/details")]
        public Task<List<MemberDetail>> GetByMemberIdAsync(Guid id)
        {
            return _memberDetailService.GetByMemberIdAsync(id);
        }
    }
}