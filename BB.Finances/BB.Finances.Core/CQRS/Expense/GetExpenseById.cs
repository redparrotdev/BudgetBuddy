using BB.Finances.Core.CQRS.Abstractions;
using BB.Finances.Data;
using BB.Finances.Data.Entities;
using BB.Finances.Data.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;


namespace BB.Finances.Core.CQRS
{
    public class GetExpenseById : AbstractGetById<Expense>;

    public class GetExpenseByIdHandler : IRequestHandler<GetExpenseById, Expense>
    {
        private readonly FinancesDbContext _ctx;

        public GetExpenseByIdHandler(FinancesDbContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<Expense> Handle(GetExpenseById request, CancellationToken cancellationToken)
        {
            return await _ctx.Expenses
                .AsNoTracking()
                .Where(e => e.Id == request.EntityId)
                .Include(e => e.Account)
                .ThenInclude(a => a.Currency)
                .Include(e => e.Category)
                .ThenInclude(c => c.Currency)
                .SingleOrDefaultAsync(cancellationToken)
                ?? throw new NotFoundException("Cant find expense record with this Id");
        }
    }
}
