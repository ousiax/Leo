// MIT License

using AutoMapper;
using Leo.Data.Domain;
using Leo.Data.Domain.Entities;
using MediatR;

namespace Leo.Web.Data.Commands.Handlers
{
    internal sealed class CreateCustomerDetailRequestHandler : IRequestHandler<CreateCustomerDetailRequest, Guid>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public CreateCustomerDetailRequestHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _uow = unitOfWork;
            _mapper = mapper;
        }

        public Task<Guid> Handle(CreateCustomerDetailRequest request, CancellationToken cancellationToken)
        {
            CustomerDetail detail = _mapper.Map<CustomerDetail>(request.CustomerDetailDto);
            if (request.User != null)
            {
                detail.Create(request.User);
            }
            return _uow.CustomerDetailRepository.CreateAsync(detail);
        }
    }
}
