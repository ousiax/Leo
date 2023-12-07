using AutoMapper;
using Leo.Data.Domain.Dtos;
using Leo.Data.Domain.Entities;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Leo.Data.Domain.Tests")]
namespace Leo.Data.Domain.Mapper
{
    internal sealed class LeoProfile : Profile
    {
        public LeoProfile()
        {
            CreateMap<Customer, CustomerDto>().ReverseMap();
            CreateMap<CustomerDetail, CustomerDetailDto>().ReverseMap();
        }
    }
}
