using Leo.Data.Domain.Entities;

namespace Leo.Web.Data
{
    public interface IMemberService
    {
        Task<Member?> GetAsync(string id);

        Task<IEnumerable<Member>> GetAsync();

        Task<string> CreateAsync(Member member);

        Task UpdateAsync(Member member);
    }
}
