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
    public class GetAccountsByUserId : IRequest<IEnumerable<Account>>
    {
        public Guid UserId { get; set; }
    }

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
                .Include(a => a.Currency)
                .ToListAsync(cancellationToken);
        }
    }
}
