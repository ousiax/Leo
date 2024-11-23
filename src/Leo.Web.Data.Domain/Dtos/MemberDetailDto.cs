// MIT License

using System.ComponentModel.DataAnnotations;

namespace Leo.Data.Domain.Dtos
{
    public class CustomerDetailDto
    {
        public string? Id { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string? CustomerId { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? Date { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string? Item { get; set; }

        public int? Count { get; set; }

        public double? Height { get; set; }

        public double? Weight { get; set; }
    }
}
