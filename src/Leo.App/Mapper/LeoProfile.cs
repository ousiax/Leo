using AutoMapper;
using Leo.App.ViewModels;
using Leo.Data.Domain.Dtos;

namespace Leo.App.Mapper
{
    internal sealed class LeoProfile : Profile
    {
        public LeoProfile()
        {
            CreateMap<MemberViewModel, MemberDto>().ReverseMap();
            CreateMap<MemberDetailViewModel, MemberDetailDto>().ReverseMap();
        }
    }
}
