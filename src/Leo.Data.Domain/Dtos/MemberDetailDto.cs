namespace Leo.Data.Domain.Dtos
{
    public class CustomerDetailDto
    {
        public string? Id { get; set; }

        public string? CustomerId { get; set; }

        public DateTime? Date { get; set; }

        public string? Item { get; set; }

        public int? Count { get; set; }

        public double? Height { get; set; }

        public double? Weight { get; set; }
    }
}