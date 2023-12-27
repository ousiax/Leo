namespace Leo.Data.Domain.Entities;

public partial class AccountStatus
{
    public byte AccountStatusId { get; set; }

    public string Status { get; set; } = null!;

    public virtual ICollection<Account> Accounts { get; set; } = new List<Account>();
}
