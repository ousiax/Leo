using Leo.Data.Domain.Dtos;

namespace Leo.UI
{
    public interface IMemberDetailService
    {
        Task<MemberDetailDto?> GetAsync(Guid id);

        Task<List<MemberDetailDto>> GetByMemberIdAsync(Guid memberId);

        Task<string?> CreateAsync(MemberDetailDto detail);
    }
}
