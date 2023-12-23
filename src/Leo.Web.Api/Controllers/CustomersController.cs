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
    public class CustomersController : ControllerBase
    {
        private readonly ILogger<CustomersController> _logger;
        private readonly IMediator _mediator;

        public CustomersController(
            IMediator mediator,
            ILogger<CustomersController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet]
        public Task<List<CustomerDto>> GetAsync()
        {
            return _mediator.Send(new GetCustomersRequest(), this.HttpContext.RequestAborted);
        }

        [HttpGet("{id}")]
        public async Task<CustomerDto?> GetAsync(Guid id)
        {
            return await _mediator.Send(new GetCustomerByIdRequest { Id = id }, this.HttpContext.RequestAborted).ConfigureAwait(false) ?? throw new NotFoundMessage();
        }

        [HttpPost]
        public async Task<CreatedMessage> CreateAsync([FromBody] CustomerDto customerDto)
        {
            var id = await _mediator.Send(
                new CreateCustomerRequest
                {
                    CustomerDto = customerDto,
                    User = this.HttpContext.User,
                },
                this.HttpContext.RequestAborted).ConfigureAwait(false);
            return this.CreatedMessageAtAction(nameof(GetAsync), new { id }, id.ToString());
        }

        [HttpPut]
        public async Task UpdateAsync([FromBody] CustomerDto customerDto)
        {
            await _mediator.Send(
                new UpdateCustomerRequest
                {
                    CustomerDto = customerDto,
                    User = this.HttpContext.User,
                },
            this.HttpContext.RequestAborted).ConfigureAwait(false);
        }

        [HttpGet("{id}/details")]
        public Task<List<CustomerDetailDto>> GetByCustomerIdAsync(Guid id)
        {
            return _mediator.Send(new GetCustomerDetailsByCustomerIdRequest { CustomerId = id }, this.HttpContext.RequestAborted);
        }
    }
}