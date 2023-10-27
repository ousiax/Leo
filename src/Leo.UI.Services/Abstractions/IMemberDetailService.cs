using Leo.Data.Domain.Dtos;

namespace Leo.UI
{
    public interface IMemberDetailService
    {
        Task<string?> CreateAsync(MemberDetailDto detail);
    }
}
