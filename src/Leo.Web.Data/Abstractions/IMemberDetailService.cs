using Leo.Data.Domain.Entities;

namespace Leo.Web.Data
{
    public interface IMemberDetailService
    {
        Task<List<MemberDetail>> GetByMemberIdAsync(Guid memberId);

        Task<MemberDetail?> GetByIdAsync(Guid id);

        Task<string> CreateAsync(MemberDetail detail);
    }
}
