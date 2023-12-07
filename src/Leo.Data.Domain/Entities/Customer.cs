namespace Leo.Data.Domain.Entities
{
    public class Customer : IAuditableEntity
    {
        public string? Id { get; set; }

        public string? Name { get; set; }

        public string? Phone { get; set; }

        public Gender? Gender { get; set; }

        public DateTime? Birthday { get; set; }

        public string? CardNo { get; set; }

        // IAuditableEntity

        public DateTime? CreatedAt { get; set; }

        public string? CreatedBy { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public string? UpdatedBy { get; set; }

        public bool? IsDeleted { get; set; }

        public DateTime? DeletedAt { get; set; }
    }
}
