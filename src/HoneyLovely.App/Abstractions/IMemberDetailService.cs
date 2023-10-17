using HoneyLovely.App.Models;

namespace HoneyLovely.App
{
    public interface IMemberDetailService
    {
        Task<int> CreateAsync(MemberDetail detail);
    }
}
