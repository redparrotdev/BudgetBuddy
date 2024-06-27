using BB.Finances.Core.CQRS.Abstractions;
using BB.Finances.Data;
using BB.Finances.Data.Entities;
using BB.Finances.Data.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;


namespace BB.Finances.Core.CQRS
{
    public class GetAccountById : AbstractGetById<Account>;

    public class GetAccountByIdHandler : IRequestHandler<GetAccountById, Account>
    {
        private readonly FinancesDbContext _ctx;

        public GetAccountByIdHandler(FinancesDbContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<Account> Handle(GetAccountById request, CancellationToken cancellationToken)
        {
            return await _ctx.Accounts
                .AsNoTracking()
                .Where(a => a.Id == request.EntityId)
                .SingleOrDefaultAsync(cancellationToken)
                ?? throw new NotFoundException("Cant find account with this Id");
        }
    }
}
