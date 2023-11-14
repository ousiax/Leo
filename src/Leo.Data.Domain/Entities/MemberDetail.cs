namespace Leo.Data.Domain.Entities
{
    public class MemberDetail : IAuditableEntity
    {
        public string? Id { get; set; }

        public string? MemberId { get; set; }

        public DateTime? Date { get; set; }

        public string? Item { get; set; }

        public int? Count { get; set; }

        public double? Height { get; set; }

        public double? Weight { get; set; }

        // IAuditableEntity

        public DateTime? CreatedAt { get; set; }

        public string? CreatedBy { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public string? UpdatedBy { get; set; }

        public bool? IsDeleted { get; set; }

        public DateTime? DeletedAt { get; set; }
    }
}
