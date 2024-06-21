using BB.Finances.Data.CQRS.Abstractions;
using BB.Finances.Data.DTO;
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
    public class GetExpensesByUserId : AbsGetByUserId<Expense> { }

    public class GetExpensesByUserIdHandler : IRequestHandler<GetExpensesByUserId, IEnumerable<Expense>>
    {
        private readonly FinancesDbContext _ctx;

        public GetExpensesByUserIdHandler(FinancesDbContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<IEnumerable<Expense>> Handle(GetExpensesByUserId request, CancellationToken cancellationToken)
        {
            return await _ctx.Expenses
                .AsNoTracking()
                .Where(e => e.UserId == request.UserId)
                .Include(e => e.Account)
                .ThenInclude(a => a.Currency)
                .Include(e => e.Category)
                .ThenInclude(c => c.Currency)
                .ToListAsync(cancellationToken);
        }
    }
}
