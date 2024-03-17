// MIT License

using Leo.Data.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Leo.Data.Domain.Dtos
{
    public class CustomerDto
    {
        public string? Id { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string? Name { get; set; }

        public string? Phone { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Gender? Gender { get; set; }

        [DataType(DataType.Date)]
        public DateTime? Birthday { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string? CardNo { get; set; }
    }
}
