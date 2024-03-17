// MIT License

using AutoMapper;
using Leo.Data.Domain.Dtos;
using MediatR;

namespace Leo.Web.Data.Queries.Handlers
{
    internal sealed class GetCustomerByIdRequestHandler : IRequestHandler<GetCustomerByIdRequest, CustomerDto>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public GetCustomerByIdRequestHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _uow = unitOfWork;
            _mapper = mapper;
        }

        public async Task<CustomerDto> Handle(GetCustomerByIdRequest request, CancellationToken cancellationToken)
        {
            if (request.Id == Guid.Empty)
            {
                throw new ArgumentException(nameof(request.Id));
            }

            var customer = await _uow.CustomerRepository.GetAsync(request.Id).ConfigureAwait(false);
            return _mapper.Map<CustomerDto>(customer);
        }
    }
}
