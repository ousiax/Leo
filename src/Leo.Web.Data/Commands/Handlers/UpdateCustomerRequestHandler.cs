using AutoMapper;
using Leo.Data.Domain;
using Leo.Data.Domain.Entities;
using MediatR;

namespace Leo.Web.Data.Commands.Handlers
{
    internal sealed class UpdateCustomerRequestHandler : IRequestHandler<UpdateCustomerRequest, Unit>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public UpdateCustomerRequestHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _uow = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateCustomerRequest request, CancellationToken cancellationToken)
        {
            var customer = _mapper.Map<Customer>(request.CustomerDto);
            if (request.User != null)
            {
                customer.Update(request.User);
            }
            await _uow.CustomerRepository.UpdateAsync(customer).ConfigureAwait(false);
            return Unit.Value;
        }
    }
}
