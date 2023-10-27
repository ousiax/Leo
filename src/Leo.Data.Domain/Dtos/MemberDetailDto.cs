namespace Leo.Data.Domain.Dtos
{
    public class MemberDetailDto
    {
        public Guid Id { get; set; }

        public Guid MemberId { get; set; }

        public DateTime? Date { get; set; }

        public string? Item { get; set; }

        public int? Count { get; set; }

        public double? Height { get; set; }

        public double? Weight { get; set; }
    }
}