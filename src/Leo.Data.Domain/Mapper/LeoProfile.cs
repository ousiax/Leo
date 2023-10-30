using AutoMapper;
using Leo.Data.Domain.Dtos;
using Leo.Data.Domain.Models;

namespace Leo.Data.Domain.Mapper
{
    internal sealed class LeoProfile : Profile
    {
        public LeoProfile()
        {
            CreateMap<Member, MemberDto>().ReverseMap();
            CreateMap<MemberDetail, MemberDetailDto>().ReverseMap();
        }
    }
}
