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
    public class GetCurrencyById : AbsGetById<Currency>;

    public class GetCurrencyByIdHandler : IRequestHandler<GetCurrencyById, Currency?>
    {
        private readonly FinancesDbContext _ctx;

        public GetCurrencyByIdHandler(FinancesDbContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<Currency?> Handle(GetCurrencyById request, CancellationToken cancellationToken)
        {
            return await _ctx.Currencies
                .AsNoTracking()
                .Where(c => c.Id == request.EntityId)
                .SingleOrDefaultAsync(cancellationToken);
        }
    }
}
