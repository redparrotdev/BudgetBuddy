using BB.Finances.Data.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BB.Finances.Data.CQRS.Abstractions
{
    public abstract class AbsCreate<T> : IRequest<int> where T: IBaseEntity 
    {
        public T Entity { get; set; }
    }
}
