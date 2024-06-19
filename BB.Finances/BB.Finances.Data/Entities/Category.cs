using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BB.Finances.Data.Entities
{
    public class Category : IBaseEntity
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid UserId { get; set; }

        [Required]
        [MaxLength(128)]
        public string Name { get; set; }

        [MaxLength(128)]
        public string Description { get; set; }

        [Required]
        public DateTime CreationDate { get; set; }

        [Required]
        public Guid CurrencyId { get; set; }
        public Currency Currency { get; set; }

        public IEnumerable<Expense> Expenses { get; set; }

        public bool IsDeleted { get; set; } = false;
    }
}
