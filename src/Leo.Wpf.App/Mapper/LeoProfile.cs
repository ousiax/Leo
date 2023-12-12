using AutoMapper;
using Leo.Data.Domain.Dtos;
using Leo.Wpf.App.ViewModels;
using static Leo.Wpf.App.ViewModels.CustomerViewModel;

namespace Leo.Windows.Mapper
{
    internal sealed class LeoProfile : Profile
    {
        public LeoProfile()
        {
            CreateMap<CustomerViewModel, CustomerDto>().ReverseMap();
            CreateMap<CustomerDetailViewModel, CustomerDetailDto>().ReverseMap();

            CreateMap<NewCustomerViewModel, CustomerDto>()
                .ForMember(o => o.Id!, o => o.Ignore())
                .ReverseMap();
            CreateMap<NewCustomerDetailViewModel, CustomerDetailDto>().ReverseMap();
        }
    }
}
