// MIT License

using AutoMapper;
using Leo.Data.Domain.Dtos;
using Leo.Data.Domain.Entities;
using MediatR;

namespace Leo.Web.Data.Queries.Handlers
{
    internal sealed class GetCustomersRequestHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<GetCustomersRequest, List<CustomerDto>>
    {
        public async Task<List<CustomerDto>> Handle(GetCustomersRequest request, CancellationToken cancellationToken)
        {
            IEnumerable<Customer> customers = await unitOfWork.CustomerRepository.GetAsync().ConfigureAwait(false);
            return mapper.Map<List<CustomerDto>>(customers);
        }
    }
}
