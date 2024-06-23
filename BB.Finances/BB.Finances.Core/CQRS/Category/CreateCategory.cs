using BB.Finances.Core.CQRS.Abstractions;
using BB.Finances.Data;
using BB.Finances.Data.Entities;
using BB.Finances.Data.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;


namespace BB.Finances.Core.CQRS
{
    public class CreateCategory : AbstractCreate<Category>;

    public class CreateCategoryHandler : IRequestHandler<CreateCategory, int>
    {
        private readonly FinancesDbContext _ctx;

        public CreateCategoryHandler(FinancesDbContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<int> Handle(CreateCategory request, CancellationToken cancellationToken)
        {
            var presentCategory = await _ctx.Categories
                .FirstOrDefaultAsync(c => c.Id == request.Entity.Id);

            if (presentCategory != null)
            {
                throw new RaceException("Category is already exists!");
            }

            await _ctx.Categories.AddAsync(request.Entity);
            return await _ctx.SaveChangesAsync();
        }
    }
}
