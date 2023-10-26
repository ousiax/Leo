using Leo.UI.Models;

namespace Leo.UI
{
    public interface IMemberDetailService
    {
        Task<string?> CreateAsync(MemberDetail detail);
    }
}
