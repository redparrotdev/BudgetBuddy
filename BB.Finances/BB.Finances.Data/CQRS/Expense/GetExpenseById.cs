using BB.Finances.Data.CQRS.Abstractions;
using BB.Finances.Data.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BB.Finances.Data.CQRS
{
    public class GetExpenseById : AbsGetById<Expense> { }

    public class GetExpenseByIdHandler : IRequestHandler<GetExpenseById, Expense?>
    {
        private readonly FinancesDbContext _ctx;

        public GetExpenseByIdHandler(FinancesDbContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<Expense?> Handle(GetExpenseById request, CancellationToken cancellationToken)
        {
            return await _ctx.Expenses
                .AsNoTracking()
                .Where(e => e.Id == request.EntityId)
                .Include(e => e.Account)
                .ThenInclude(a => a.Currency)
                .Include(e => e.Category)
                .ThenInclude(c => c.Currency)
                .SingleOrDefaultAsync(cancellationToken);
        }
    }
}
