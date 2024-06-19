using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BB.Finances.Data.DTO
{
    public class AccountDTO : IBaseDTO
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public string Name { get; set; }

        public double Balance { get; set; }

        public DateTime CreationDate { get; set; } = DateTime.Now;

        public Guid CurrencyId { get; set; }

        public string CurrencySing { get; set; }

        public bool IsDeleted { get; set; }
    }
}
