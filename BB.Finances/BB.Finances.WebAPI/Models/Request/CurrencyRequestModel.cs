using System.ComponentModel.DataAnnotations;

namespace BB.Finances.WebAPI.Models.Request
{
    public class CurrencyRequestModel
    {
        [Required]
        [MinLength(2)]
        [MaxLength(128)]
        public string Name { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(3)]
        public string CurrencySign { get; set; }
    }
}
