using AutoMapper;
using Leo.Data.Domain.Dtos;
using MediatR;

namespace Leo.Web.Data.Queries.Handlers
{
    internal sealed class GetCustomerDetailsByCustomerIdRequestHandler : IRequestHandler<GetCustomerDetailsByCustomerIdRequest, List<CustomerDetailDto>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public GetCustomerDetailsByCustomerIdRequestHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _uow = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<CustomerDetailDto>> Handle(GetCustomerDetailsByCustomerIdRequest request, CancellationToken cancellationToken)
        {
            if (request.CustomerId == Guid.Empty)
            {
                throw new ArgumentException(nameof(request.CustomerId));
            }

            var detail = await _uow.CustomerDetailRepository.GetByCustomerIdAsync(request.CustomerId).ConfigureAwait(false);
            return _mapper.Map<List<CustomerDetailDto>>(detail);
        }
    }
}
