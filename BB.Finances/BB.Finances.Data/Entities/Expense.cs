using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BB.Finances.Data.Entities
{
    public class Expense : IBaseEntity
    {
        [Key]
        public Guid Id { get; set; }

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
        public Account Account { get; set; }

        [Required]
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
