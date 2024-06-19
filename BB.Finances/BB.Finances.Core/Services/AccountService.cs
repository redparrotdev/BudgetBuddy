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
    public class AccountService : IAccountService
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mdr;

        public AccountService(IMapper mapper, IMediator mdr)
        {
            _mapper = mapper;
            _mdr = mdr;
        }

        public async Task<Guid> CreateNewAsync(AccountDTO dto)
        {
            var entity = _mapper.Map<Account>(dto);

            var addingResult = await _mdr.Send(new CreateAccount()
            {
                Account = entity
            });

            if (addingResult != 1)
            {
                throw new Exception("Something went wrong while adding new Account!");
            }

            return entity.Id;
        }

        public Task DeleteAsync(Guid entityId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<AccountDTO>> GetAllByUserIdAsync(Guid userId)
        {
            throw new NotImplementedException();
        }

        public async Task<AccountDTO> GetByIdAsync(Guid entityId)
        {
            var entity = await _mdr.Send(new GetAccountById()
            {
                AccountId = entityId
            });

            if (entity == null)
            {
                throw new Exception("Entity not found!");
            }

            if (entity.IsDeleted)
            {
                throw new Exception("This account is deleted!");
            }

            var dto = _mapper.Map<AccountDTO>(entity);

            return dto;
        }

        public Task PatchAsync(AccountDTO changed)
        {
            throw new NotImplementedException();
        }
    }
}
