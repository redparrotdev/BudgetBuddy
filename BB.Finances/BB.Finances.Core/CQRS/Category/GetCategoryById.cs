using BB.Finances.Core.CQRS.Abstractions;
using BB.Finances.Data;
using BB.Finances.Data.Entities;
using BB.Finances.Data.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;


namespace BB.Finances.Core.CQRS
{
    public class GetCategoryById : AbstractGetById<Category>;

    public class GetCategoryByIdHandler : IRequestHandler<GetCategoryById, Category>
    {
        private readonly FinancesDbContext _ctx;

        public GetCategoryByIdHandler(FinancesDbContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<Category> Handle(GetCategoryById request, CancellationToken cancellationToken)
        {
            return await _ctx.Categories
                .AsNoTracking()
                .Where(c => c.Id == request.EntityId)
                .SingleOrDefaultAsync(cancellationToken)
                ?? throw new NotFoundException("Cant find category with this Id");
        }
    }
}
