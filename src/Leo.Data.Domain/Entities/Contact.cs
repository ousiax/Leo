namespace Leo.Data.Domain.Entities;

public partial class Contact
{
    public Guid ContactId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string? Country { get; set; }

    public string? State { get; set; }

    public string? Street { get; set; }

    public string? ZipCode { get; set; }

    public virtual ICollection<Email> Emails { get; set; } = new List<Email>();
}
