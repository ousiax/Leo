using Alyio.AspNetCore.ApiMessages;
using Leo.Data.Domain.Dtos;
using Leo.Web.Data.Commands;
using Leo.Web.Data.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Leo.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MemberDetailsController : ControllerBase
    {
        private readonly ILogger<MembersController> _logger;
        private readonly IMediator _mediator;

        public MemberDetailsController(IMediator mediator, ILogger<MembersController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet("{id}")]
        public async Task<MemberDetailDto?> GetByIdAsync(string id)
        {
            return await _mediator.Send(new GetMemberDetailByIdRequest { Id = id }, HttpContext.RequestAborted).ConfigureAwait(false) ?? throw new NotFoundMessage();
        }

        [HttpPost]
        public async Task<CreatedMessage> CreateAsync([FromBody] MemberDetailDto detailDto)
        {
            var id = await _mediator.Send(
                new CreateMemberDetailRequest
                {
                    MemberDetailDto = detailDto,
                    User = this.HttpContext.User,
                },
                HttpContext.RequestAborted).ConfigureAwait(false);
            return this.CreatedMessageAtAction(nameof(GetByIdAsync), new { id }, id.ToString());
        }
    }
}