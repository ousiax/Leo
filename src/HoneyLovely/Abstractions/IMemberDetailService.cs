using HoneyLovely.Models;

namespace HoneyLovely
{
    public interface IMemberDetailService
    {
        Task<int> CreateAsync(MemberDetail detail);
    }
}
