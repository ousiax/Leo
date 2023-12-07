using AutoMapper;
using Leo.Data.Domain.Dtos;
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
