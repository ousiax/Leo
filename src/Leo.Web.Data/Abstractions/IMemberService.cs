using Leo.Web.Data.Models;

namespace Leo.Web.Data
{
    public interface IMemberService
    {
        Task<Member> GetAsync(Guid id);

        Task<List<Member>> GetAsync();

        Task<int> CreateAsync(Member member);

        Task<int> UpdateAsync(Member member);
    }
}
