using BB.Finances.Core.CQRS.Abstractions;
using BB.Finances.Data;
using BB.Finances.Data.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BB.Finances.Core.CQRS
{
    public class GetAccountsByUserId : AbstractGetByUserId<Account>;

    public class GetAccountsByUserIdHandler : IRequestHandler<GetAccountsByUserId, IEnumerable<Account>>
    {
        private readonly FinancesDbContext _ctx;

        public GetAccountsByUserIdHandler(FinancesDbContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<IEnumerable<Account>> Handle(GetAccountsByUserId request, CancellationToken cancellationToken)
        {
            return await _ctx.Accounts
                .AsNoTracking()
                .Where(a => a.UserId == request.UserId)
                .ToListAsync(cancellationToken);
        }
    }
}
