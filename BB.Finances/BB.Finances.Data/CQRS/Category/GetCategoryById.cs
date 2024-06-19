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
    public class GetCategoryById : AbsGetById<Category> { }

    public class GetCategoryByIdHandler : IRequestHandler<GetCategoryById, Category?>
    {
        private readonly FinancesDbContext _ctx;

        public GetCategoryByIdHandler(FinancesDbContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<Category?> Handle(GetCategoryById request, CancellationToken cancellationToken)
        {
            return await _ctx.Categories
                .AsNoTracking()
                .Where(c => c.Id == request.EntityId)
                .Include(c => c.Currency)
                .SingleOrDefaultAsync(cancellationToken);
        }
    }
}
