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
    public class GetCategoriesByUserId : AbsGetByUserId<Category>;

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
