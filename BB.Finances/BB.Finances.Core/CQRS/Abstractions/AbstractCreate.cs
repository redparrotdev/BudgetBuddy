using BB.Finances.Data.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BB.Finances.Core.CQRS.Abstractions
{
    public abstract class AbstractCreate<T> : IRequest<int> where T: IBaseEntity 
    {
        public T Entity { get; set; }
    }
}
