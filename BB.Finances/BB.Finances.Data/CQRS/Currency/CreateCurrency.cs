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
    public class CreateCurrency : AbsCreate<Currency>;

    public class CreateCurrencyHandler : IRequestHandler<CreateCurrency, int>
    {
        private readonly FinancesDbContext _ctx;

        public CreateCurrencyHandler(FinancesDbContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<int> Handle(CreateCurrency request, CancellationToken cancellationToken)
        {
            var present = await _ctx.Currencies
                .FirstOrDefaultAsync(c => c.Id == request.Entity.Id);

            if (present != null)
            {
                throw new Exception("Currency is already exists!");
            }

            await _ctx.Currencies.AddAsync(request.Entity);
            return await _ctx.SaveChangesAsync(cancellationToken);
        }
    }
}
