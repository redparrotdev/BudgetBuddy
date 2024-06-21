using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BB.Finances.Data.DTO
{
    public class ExpenseDTO : IBaseDTO
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public double ValueOut { get; set; }

        public double ValueIn { get; set; }

        public string Description { get; set; }

        public DateTime Date { get; set; } = DateTime.Now;

        public Guid AccountId { get; set; }

        public Guid CategoryId { get; set; }
    }
}
