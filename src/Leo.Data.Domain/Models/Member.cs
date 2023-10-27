namespace Leo.Data.Domain.Models
{
    public class Member
    {
        public Guid Id { get; set; }

        public string? Name { get; set; }

        public string? Phone { get; set; }

        public Gender? Gender { get; set; }

        public DateTime? Birthday { get; set; }

        public string? CardNo { get; set; }
    }
}
