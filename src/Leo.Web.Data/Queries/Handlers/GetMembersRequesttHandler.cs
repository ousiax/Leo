using AutoMapper;
using Leo.Data.Domain.Dtos;
using MediatR;

namespace Leo.Web.Data.Queries.Handlers
{
    internal class GetCustomersRequesttHandler : IRequestHandler<GetCustomersRequest, List<CustomerDto>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public GetCustomersRequesttHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _uow = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<CustomerDto>> Handle(GetCustomersRequest request, CancellationToken cancellationToken)
        {
            var customers = await _uow.CustomerRepository.GetAsync().ConfigureAwait(false);
            return _mapper.Map<List<CustomerDto>>(customers);
        }
    }
}
