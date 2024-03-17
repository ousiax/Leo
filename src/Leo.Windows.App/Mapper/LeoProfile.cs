// MIT License

using AutoMapper;
using Leo.UI.Services.Models;
using Leo.Windows.ViewModels;

namespace Leo.Windows.Mapper
{
    internal sealed class LeoProfile : Profile
    {
        public LeoProfile()
        {
            CreateMap<CustomerViewModel, CustomerDto>().ReverseMap();
            CreateMap<CustomerDetailViewModel, CustomerDetailDto>().ReverseMap();
        }
    }
}
