using Leo.Data.Domain.Dtos;

namespace Leo.UI
{
    public interface IMemberService
    {
        Task<MemberDto?> GetAsync(Guid id);

        Task<List<MemberDto>> GetAsync();

        Task<string?> CreateAsync(MemberDto member);

        Task<int> UpdateAsync(MemberDto member);
    }
}
