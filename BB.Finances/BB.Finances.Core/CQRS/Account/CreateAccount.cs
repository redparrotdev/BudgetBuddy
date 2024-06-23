using BB.Finances.Core.CQRS.Abstractions;
using BB.Finances.Data;
using BB.Finances.Data.Entities;
using BB.Finances.Data.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BB.Finances.Core.CQRS
{
    public class CreateAccount : AbstractCreate<Account>;

    public class CreateAccountHandler : IRequestHandler<CreateAccount, int>
    {
        private readonly FinancesDbContext _ctx;

        public CreateAccountHandler(FinancesDbContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<int> Handle(CreateAccount request, CancellationToken cancellationToken)
        {
            var presentAccount = await _ctx.Accounts
                .FirstOrDefaultAsync(a => a.Id == request.Entity.Id);

            if (presentAccount != null)
            {
                throw new RaceException("Account is already exists!");
            }

            await _ctx.Accounts.AddAsync(request.Entity);
            return await _ctx.SaveChangesAsync();
        }
    }
}
