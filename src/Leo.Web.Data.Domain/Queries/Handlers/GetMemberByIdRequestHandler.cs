// MIT License

using AutoMapper;
using Leo.Data.Domain.Dtos;
using Leo.Data.Domain.Entities;
using MediatR;

namespace Leo.Web.Data.Queries.Handlers
{
    internal sealed class GetCustomerByIdRequestHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<GetCustomerByIdRequest, CustomerDto>
    {
        public async Task<CustomerDto> Handle(GetCustomerByIdRequest request, CancellationToken cancellationToken)
        {
            if (request.Id == Guid.Empty)
            {
                throw new ArgumentException(nameof(request.Id));
            }

            Customer? customer = await unitOfWork.CustomerRepository.GetAsync(request.Id).ConfigureAwait(false);
            return mapper.Map<CustomerDto>(customer);
        }
    }
}
