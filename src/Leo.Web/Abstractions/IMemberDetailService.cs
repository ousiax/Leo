using Leo.Web.Models;

namespace Leo.Web
{
    public interface IMemberDetailService
    {
        Task<List<MemberDetail>> GetByMemberIdAsync(Guid memberId);

        Task<MemberDetail> GetByIdAsync(Guid id);

        Task<int> CreateAsync(MemberDetail detail);
    }
}
