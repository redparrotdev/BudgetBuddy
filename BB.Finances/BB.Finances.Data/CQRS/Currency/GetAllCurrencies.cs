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
    public class GetAllCurrencies: IRequest<IEnumerable<Currency>>;

    public class GetAllCurrenciesHandler : IRequestHandler<GetAllCurrencies, IEnumerable<Currency>>
    {
        private readonly FinancesDbContext _ctx;

        public GetAllCurrenciesHandler(FinancesDbContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<IEnumerable<Currency>> Handle(GetAllCurrencies request, CancellationToken cancellationToken)
        {
            return await _ctx.Currencies
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }
    }
}
