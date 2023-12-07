using AutoMapper;
using Leo.Data.Domain;
using Leo.Data.Domain.Entities;
using MediatR;

namespace Leo.Web.Data.Commands.Handlers
{
    internal sealed class CreateCustomerRequestHandler : IRequestHandler<CreateCustomerRequest, string>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public CreateCustomerRequestHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _uow = unitOfWork;
            _mapper = mapper;
        }

        public Task<string> Handle(CreateCustomerRequest request, CancellationToken cancellationToken)
        {
            var customer = _mapper.Map<Customer>(request.CustomerDto);
            if (request.User != null)
            {
                customer.Create(request.User);
            }
            return _uow.CustomerRepository.CreateAsync(customer);
        }
    }
}
