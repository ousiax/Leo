using AutoMapper;
using Leo.Data.Domain.Dtos;
using Leo.Data.Domain.Models;
using Leo.Data.Domain.ViewModels;

namespace Leo.Data.Domain.Mapper
{
    internal sealed class LeoProfile : Profile
    {
        public LeoProfile()
        {
            CreateMap<Member, MemberDto>().ReverseMap();
            CreateMap<MemberDetail, MemberDetailDto>().ReverseMap();
            CreateMap<MemberViewModel, MemberDto>().ReverseMap();
            CreateMap<MemberDetailViewModel, MemberDetailDto>().ReverseMap();
        }
    }
}
