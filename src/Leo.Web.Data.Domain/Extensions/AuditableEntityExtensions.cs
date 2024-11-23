// MIT License

using System.Security.Claims;
using Leo.Data.Domain.Entities;

namespace Leo.Data.Domain
{
    public static class AuditableEntityExtensions
    {
        public static IAuditableEntity Create(this IAuditableEntity entity, ClaimsPrincipal principal)
        {
            entity.CreatedAt = DateTime.UtcNow;
            entity.CreatedBy = principal.FindFirst("preferred_username")!.Value; // TODO using oid instead of name
            return entity;
        }

        public static IAuditableEntity Update(this IAuditableEntity entity, ClaimsPrincipal principal)
        {
            entity.UpdatedAt = DateTime.UtcNow;
            entity.UpdatedBy = principal.FindFirst("preferred_username")!.Value; // TODO using oid instead of name
            return entity;
        }

        public static IAuditableEntity Delete(this IAuditableEntity entity, ClaimsPrincipal principal)
        {
            entity.DeletedAt = DateTime.UtcNow;
            entity.IsDeleted = true;
            entity.Update(principal);
            return entity;
        }
    }
}
