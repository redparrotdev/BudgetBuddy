using BB.Finances.Data.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BB.Finances.Core.Abstractions
{
    public interface ICRUDService<T> where T : IBaseDTO
    {
        Task<T> GetByIdAsync(Guid entityId);

        Task<IEnumerable<T>> GetAllByUserIdAsync(Guid userId);

        Task<Guid> CreateNewAsync(T dto);

        Task PatchAsync(T changed);

        Task DeleteAsync(Guid entityId);
    }
}
