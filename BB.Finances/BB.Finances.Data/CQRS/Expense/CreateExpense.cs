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
    public class CreateExpense : AbsCreate<Expense> { }

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
                throw new Exception("Expense record is already exists");
            }

            await _ctx.Expenses.AddAsync(request.Entity);
            return await _ctx.SaveChangesAsync(cancellationToken);
        }
    }
}
