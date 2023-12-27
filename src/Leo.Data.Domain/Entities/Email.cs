namespace Leo.Data.Domain.Entities;

public partial class Email
{
    public Guid EmailId { get; set; }

    public Guid ContactId { get; set; }

    public string EmailAddress { get; set; } = null!;

    public bool? IsPrimary { get; set; }

    public virtual Contact Contact { get; set; } = null!;
}
