using Alyio.AspNetCore.ApiMessages;
using Leo.Web.Data;
using Leo.Web.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace Leo.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MemberDetailsController : ControllerBase
    {
        private readonly ILogger<MembersController> _logger;
        private readonly IMemberDetailService _memberDetailService;

        public MemberDetailsController(IMemberDetailService memberDetailService, ILogger<MembersController> logger)
        {
            _memberDetailService = memberDetailService;
            _logger = logger;
        }

        [HttpGet("{id}")]
        public async Task<MemberDetail?> GetByIdAsync(Guid id)
        {
            return await _memberDetailService.GetByIdAsync(id) ?? throw new NotFoundMessage();
        }

        [HttpPost]
        public async Task<CreatedMessage> CreateAsync([FromBody] MemberDetail detail)
        {
            var id = await _memberDetailService.CreateAsync(detail);
            return this.CreatedMessageAtAction(nameof(GetByIdAsync), new { id }, id.ToString());
        }
    }
}