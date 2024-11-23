// MIT License

using AutoMapper;
using Leo.Data.Domain.Dtos;
using Leo.Data.Domain.Entities;
using MediatR;

namespace Leo.Web.Data.Queries.Handlers
{
    internal sealed class GetCustomerDetailsByCustomerIdRequestHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<GetCustomerDetailsByCustomerIdRequest, List<CustomerDetailDto>>
    {
        public async Task<List<CustomerDetailDto>> Handle(GetCustomerDetailsByCustomerIdRequest request, CancellationToken cancellationToken)
        {
            if (request.CustomerId == Guid.Empty)
            {
                throw new ArgumentException(nameof(request.CustomerId));
            }

            IEnumerable<CustomerDetail> detail = await unitOfWork.CustomerDetailRepository.GetByCustomerIdAsync(request.CustomerId).ConfigureAwait(false);
            return mapper.Map<List<CustomerDetailDto>>(detail);
        }
    }
}
