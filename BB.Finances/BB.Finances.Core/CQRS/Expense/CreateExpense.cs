using BB.Finances.Core.CQRS.Abstractions;
using BB.Finances.Data.Entities;
using BB.Finances.Data.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;


namespace BB.Finances.Data.CQRS
{
    public class CreateExpense : AbstractCreate<Expense>;

    public class CreateExpenseHandler : IRequestHandler<CreateExpense, int>
    {
        private readonly FinancesDbContext _ctx;

        public CreateExpenseHandler(FinancesDbContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<int> Handle(CreateExpense request, CancellationToken cancellationToken)
        {
            var present = await _ctx.Expenses
                .FirstOrDefaultAsync(e => e.Id == request.Entity.Id);

            if (present != null)
            {
                throw new RaceException("Expense record is already exists");
            }

            await _ctx.Expenses.AddAsync(request.Entity);
            return await _ctx.SaveChangesAsync(cancellationToken);
        }
    }
}
