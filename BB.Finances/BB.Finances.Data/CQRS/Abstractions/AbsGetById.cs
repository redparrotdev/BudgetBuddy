using BB.Finances.Data.DTO;
using BB.Finances.Data.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BB.Finances.Data.CQRS.Abstractions
{
    public abstract class AbsGetById<T> : IRequest<T?> where T : IBaseEntity
    {
        public Guid EntityId { get; set; }
    }
}
