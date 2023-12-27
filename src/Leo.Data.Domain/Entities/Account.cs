namespace Leo.Data.Domain.Entities;

public partial class Account
{
    public Guid AccountId { get; set; }

    public Guid? Oid { get; set; }

    public string? Username { get; set; }

    public byte[] Password { get; set; } = null!;

    public byte[]? Salt { get; set; }

    public byte AccountStatusId { get; set; }

    public Guid? CustomerId { get; set; }

    public AccountStatus AccountStatus { get; set; } = null!;

    public virtual Customer? Customer { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual ICollection<Role> Roles { get; set; } = new List<Role>();
}
