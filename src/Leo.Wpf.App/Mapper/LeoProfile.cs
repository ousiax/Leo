using AutoMapper;
using Leo.Data.Domain.Dtos;
using Leo.Wpf.App.ViewModels;

namespace Leo.Windows.Mapper
{
    internal sealed class LeoProfile : Profile
    {
        public LeoProfile()
        {
            CreateMap<CustomerViewModel, CustomerDto>().ReverseMap();
            CreateMap<CustomerDetailViewModel, CustomerDetailDto>().ReverseMap();

            CreateMap<NewCustomerViewModel, CustomerDto>();
        }
    }
}
