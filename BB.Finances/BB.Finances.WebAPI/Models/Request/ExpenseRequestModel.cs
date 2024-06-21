using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace BB.Finances.WebAPI.Models.Request
{
    public class ExpenseRequestModel
    {
        [Required]
        public Guid UserId { get; set; }

        [Required]
        [Precision(2)]
        public double ValueOut { get; set; }

        [Required]
        [Precision(2)]
        public double ValueIn { get; set; }

        [MaxLength(128)]
        public string Description { get; set; }

        [Required]
        public Guid AccountId { get; set; }

        [Required]
        public Guid CategoryId { get; set; }
    }
}
