using AutoMapper;
using BB.Finances.Core.Abstractions;
using BB.Finances.Data.CQRS;
using BB.Finances.Data.DTO;
using BB.Finances.Data.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BB.Finances.Core.Services
{
    public class ExpenseService : IExpenseService
    {
        private readonly IMediator _mdr;
        private readonly IMapper _mapper;

        public ExpenseService(IMediator mdr, IMapper mapper)
        {
            _mdr = mdr;
            _mapper = mapper;
        }

        public async Task<Guid> CreateNewAsync(ExpenseDTO dto)
        {
            var entity = _mapper.Map<Expense>(dto);

            var result = await _mdr.Send(new CreateExpense()
            {
                Entity = entity
            });

            if (result != 1)
            {
                throw new Exception("Adding new Expense went wrong!");
            }

            return entity.Id;
        }

        public Task DeleteAsync(Guid entityId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ExpenseDTO>> GetAllByUserIdAsync(Guid userId)
        {
            var entities = await _mdr.Send(new GetExpensesByUserId()
            {
                UserId = userId
            });

            var result = _mapper.Map<IEnumerable<ExpenseDTO>>(entities);

            return result;
        }

        public async Task<ExpenseDTO> GetByIdAsync(Guid entityId)
        {
            var entity = await _mdr.Send(new GetExpenseById()
            {
                EntityId = entityId
            });

            if (entity == null)
            {
                throw new Exception("Expense not found");
            }

            var result = _mapper.Map<ExpenseDTO>(entity);

            return result;
        }

        public Task PatchAsync(ExpenseDTO changed)
        {
            throw new NotImplementedException();
        }
    }
}
