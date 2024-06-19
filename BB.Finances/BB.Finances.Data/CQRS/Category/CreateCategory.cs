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
    public class CreateCategory : AbsCreate<Category> { }

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
                throw new Exception("Category is already exists!");
            }

            await _ctx.Categories.AddAsync(request.Entity);
            return await _ctx.SaveChangesAsync();
        }
    }
}
