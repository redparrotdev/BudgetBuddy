using BB.Finances.Data.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BB.Finances.Core.Abstractions
{
    public interface ICurrencyService : ICRUDService<CurrencyDTO>
    {
        Task<IEnumerable<CurrencyDTO>> GetAllCurrenciesAsync();
    }
}
