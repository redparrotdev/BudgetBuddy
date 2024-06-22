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
    public class GetExpensesByAccountId : IRequest<IEnumerable<Expense>>
    {
        public Guid AccountId { get; set; }
    }

    public class GetExpensesByAccountIdHandler : IRequestHandler<GetExpensesByAccountId, IEnumerable<Expense>>
    {
        private readonly FinancesDbContext _ctx;

        public GetExpensesByAccountIdHandler(FinancesDbContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<IEnumerable<Expense>> Handle(GetExpensesByAccountId request, CancellationToken cancellationToken)
        {
            return await _ctx.Expenses
                .AsNoTracking()
                .Where(e => e.AccountId == request.AccountId)
                .Include(e => e.Category)
                .Include(e => e.Account)
                .ToListAsync(cancellationToken);
        }
    }
}
