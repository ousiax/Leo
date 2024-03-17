// MIT License

using System.Runtime.CompilerServices;
using AutoMapper;
using Leo.Data.Domain.Dtos;
using Leo.Data.Domain.Entities;

[assembly: InternalsVisibleTo("Leo.Web.Data.Domain.Tests")]
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
