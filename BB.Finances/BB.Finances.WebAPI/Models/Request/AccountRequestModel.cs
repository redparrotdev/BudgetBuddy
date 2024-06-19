using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace BB.Finances.WebAPI.Models.Request
{
    public class AccountRequestModel
    {
        [Required]
        public Guid UserId { get; set; }

        [Required]
        [MaxLength(128)]
        public string Name { get; set; }

        [Required]
        [Precision(2)]
        public double Balance { get; set; }

        [Required]
        public Guid CurrencyId { get; set; }
    }
}
