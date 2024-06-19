using System.ComponentModel.DataAnnotations;

namespace BB.Finances.WebAPI.Models.Request
{
    public class CategoryRequestModel
    {
        [Required]
        public Guid UserId { get; set; }

        [Required]
        [MaxLength(128)]
        public string Name { get; set; }

        [MaxLength(128)]
        public string Description { get; set; }

        [Required]
        public Guid CurrencyId { get; set; }
    }
}
