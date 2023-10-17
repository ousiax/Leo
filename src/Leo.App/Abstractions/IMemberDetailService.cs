using Leo.App.Models;

namespace Leo.App
{
    public interface IMemberDetailService
    {
        Task<int> CreateAsync(MemberDetail detail);
    }
}
