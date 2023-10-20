using Leo.Web.Data.Models;

namespace Leo.Web.Data
{
    public interface IMemberDetailService
    {
        Task<List<MemberDetail>> GetByMemberIdAsync(Guid memberId);

        Task<MemberDetail?> GetByIdAsync(Guid id);

        Task<Guid> CreateAsync(MemberDetail detail);
    }
}
