// MIT License

using AutoMapper;
using Leo.Data.Domain.Dtos;
using Leo.Data.Domain.Entities;
using MediatR;

namespace Leo.Web.Data.Queries.Handlers
{
    internal sealed class GetCustomerDetailByIdRequestHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<GetCustomerDetailByIdRequest, CustomerDetailDto>
    {
        public async Task<CustomerDetailDto> Handle(GetCustomerDetailByIdRequest request, CancellationToken cancellationToken)
        {
            if (request.Id == Guid.Empty)
            {
                throw new ArgumentException(nameof(request.Id));
            }

            CustomerDetail? detail = await unitOfWork.CustomerDetailRepository.GetByIdAsync(request.Id).ConfigureAwait(false);
            return mapper.Map<CustomerDetailDto>(detail);
        }
    }
}
