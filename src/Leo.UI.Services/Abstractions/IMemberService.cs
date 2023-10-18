using Leo.UI.Models;

namespace Leo.UI
{
    public interface IMemberService
    {
        Task<Member> GetAsync(Guid id);

        Task<List<Member>> GetAsync();

        Task<int> CreateAsync(Member member);

        Task<int> UpdateAsync(Member member);
    }
}
