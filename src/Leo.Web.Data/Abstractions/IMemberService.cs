using Leo.Data.Domain.Entities;

namespace Leo.Web.Data
{
    public interface IMemberService
    {
        Task<Member?> GetAsync(Guid id);

        Task<List<Member>> GetAsync();

        Task<Guid> CreateAsync(Member member);

        Task UpdateAsync(Member member);
    }
}
