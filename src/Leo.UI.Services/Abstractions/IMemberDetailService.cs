using Leo.UI.Models;

namespace Leo.UI
{
    public interface IMemberDetailService
    {
        Task<int> CreateAsync(MemberDetail detail);
    }
}
