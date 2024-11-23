// MIT License

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
    public class CustomerDetailsController : ControllerBase
    {
        private readonly ILogger<CustomersController> _logger;
        private readonly IMediator _mediator;

        public CustomerDetailsController(IMediator mediator, ILogger<CustomersController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet("{id}")]
        public async Task<CustomerDetailDto?> GetByIdAsync(Guid id)
        {
            return await _mediator.Send(new GetCustomerDetailByIdRequest { Id = id }, HttpContext.RequestAborted).ConfigureAwait(false) ?? throw new NotFoundMessage();
        }

        [HttpPost]
        public async Task<CreatedMessage> CreateAsync([FromBody] CustomerDetailDto detailDto)
        {
            Guid id = await _mediator.Send(
                new CreateCustomerDetailRequest
                {
                    CustomerDetailDto = detailDto,
                    User = HttpContext.User,
                },
                HttpContext.RequestAborted).ConfigureAwait(false);
            return this.CreatedMessageAtAction(nameof(GetByIdAsync), new { id }, id.ToString());
        }
    }
}
