using BB.Finances.Data;
using BB.Finances.Data.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BB.Finances.Core.CQRS
{
    public class GetExpensesByCategoryId : IRequest<IEnumerable<Expense>>
    {
        public Guid CategoryId { get; set; }
    }

    public class GetExpensesByCategoryIdHandler : IRequestHandler<GetExpensesByCategoryId, IEnumerable<Expense>>
    {
        private readonly FinancesDbContext _ctx;

        public GetExpensesByCategoryIdHandler(FinancesDbContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<IEnumerable<Expense>> Handle(GetExpensesByCategoryId request, CancellationToken cancellationToken)
        {
            return await _ctx.Expenses
                .AsNoTracking()
                .Where(e => e.CategoryId == request.CategoryId)
                .Include(e => e.Category)
                .Include(e => e.Account)
                .ToListAsync(cancellationToken);
        }
    }
}
