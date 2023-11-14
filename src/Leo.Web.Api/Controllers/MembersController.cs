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
    public class MembersController : ControllerBase
    {
        private readonly ILogger<MembersController> _logger;
        private readonly IMediator _mediator;

        public MembersController(
            IMediator mediator,
            ILogger<MembersController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet]
        public Task<List<MemberDto>> GetAsync()
        {
            return _mediator.Send(new GetMembersRequest(), this.HttpContext.RequestAborted);
        }

        [HttpGet("{id}")]
        public async Task<MemberDto?> GetAsync(Guid id)
        {
            return await _mediator.Send(new GetMemberByIdRequest { Id = id }, this.HttpContext.RequestAborted).ConfigureAwait(false) ?? throw new NotFoundMessage();
        }

        [HttpPost]
        public async Task<CreatedMessage> CreateAsync([FromBody] MemberDto memberDto)
        {
            var id = await _mediator.Send(
                new CreateMemberRequest
                {
                    MemberDto = memberDto,
                    User = this.HttpContext.User,
                },
                this.HttpContext.RequestAborted).ConfigureAwait(false);
            return this.CreatedMessageAtAction(nameof(GetAsync), new { id }, id.ToString());
        }

        [HttpPut]
        public async Task UpdateAsync([FromBody] MemberDto memberDto)
        {
            await _mediator.Send(
                new UpdateMemberRequest
                {
                    MemberDto = memberDto,
                    User = this.HttpContext.User,
                },
            this.HttpContext.RequestAborted).ConfigureAwait(false);
        }

        [HttpGet("{id}/details")]
        public Task<List<MemberDetailDto>> GetByMemberIdAsync(Guid id)
        {
            return _mediator.Send(new GetMemberDetailsByMemberIdRequest { MemberId = id }, this.HttpContext.RequestAborted);
        }
    }
}