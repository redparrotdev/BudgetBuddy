using BB.Finances.Data.DTO;
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
    public class GetAccountById : IRequest<Account?>
    {
        public Guid AccountId { get; set; }
    }

    public class GetAccountByIdHandler : IRequestHandler<GetAccountById, Account?>
    {
        private readonly FinancesDbContext _ctx;

        public GetAccountByIdHandler(FinancesDbContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<Account?> Handle(GetAccountById request, CancellationToken cancellationToken)
        {
            return await _ctx.Accounts
                .AsNoTracking()
                .Where(a => a.Id == request.AccountId)
                .Include(a => a.Currency)
                .SingleOrDefaultAsync(cancellationToken);
        }
    }
}
