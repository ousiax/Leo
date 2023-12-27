namespace Leo.Data.Domain.Entities;

public partial class Permission
{
    public Guid PermissionId { get; set; }

    public string RoleId { get; set; } = null!;

    public string PermissionName { get; set; } = null!;

    public string? Description { get; set; }
}
