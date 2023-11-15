using Leo.Data.Domain.Entities;

namespace Leo.Web.Data
{
    public interface IMemberDetailRepository
    {
        Task<IEnumerable<MemberDetail>> GetByMemberIdAsync(string memberId);

        Task<MemberDetail?> GetByIdAsync(string id);

        Task<string> CreateAsync(MemberDetail detail);
    }
}
