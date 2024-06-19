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
    public class CreateAccount : AbsCreate<Account> { }

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
                throw new Exception("Account is already exists!");
            }

            await _ctx.Accounts.AddAsync(request.Entity);
            return await _ctx.SaveChangesAsync();
        }
    }
}
