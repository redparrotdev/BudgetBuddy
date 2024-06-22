using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BB.Finances.Data.DTO
{
    public class CurrencyDTO : IBaseDTO
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string CurrencySign { get; set; }
    }
}
