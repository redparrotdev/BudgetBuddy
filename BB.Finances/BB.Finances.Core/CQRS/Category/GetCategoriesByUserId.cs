using BB.Finances.Core.CQRS.Abstractions;
using BB.Finances.Data;
using BB.Finances.Data.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;


namespace BB.Finances.Core.CQRS
{
    public class GetCategoriesByUserId : AbstractGetByUserId<Category>;

    public class GetCategoriesByUserIdHandler : IRequestHandler<GetCategoriesByUserId, IEnumerable<Category>>
    {
        private readonly FinancesDbContext _ctx;

        public GetCategoriesByUserIdHandler(FinancesDbContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<IEnumerable<Category>> Handle(GetCategoriesByUserId request, CancellationToken cancellationToken)
        {
            return await _ctx.Categories
                .AsNoTracking()
                .Where(c => c.UserId == request.UserId)
                .Include(c => c.Currency)
                .ToListAsync(cancellationToken);
        }
    }
}
