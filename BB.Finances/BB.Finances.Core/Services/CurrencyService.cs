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
    public class CurrencyService : ICurrencyService
    {
        private readonly IMediator _mdr;
        private readonly IMapper _mapper;

        public CurrencyService(IMediator mdr, IMapper mapper)
        {
            _mdr = mdr;
            _mapper = mapper;
        }

        public async Task<Guid> CreateNewAsync(CurrencyDTO dto)
        {
            var entity = _mapper.Map<Currency>(dto);

            var result = await _mdr.Send(new CreateCurrency()
            {
                Entity = entity
            });

            if (result != 1)
            {
                throw new Exception("Creating currency went wrong!");
            }

            return entity.Id;
        }

        public Task DeleteAsync(Guid entityId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// For this service this method is not needed. Using this throws and error
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task<IEnumerable<CurrencyDTO>> GetAllByUserIdAsync(Guid userId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<CurrencyDTO>> GetAllCurrenciesAsync()
        {
            var entities = await _mdr.Send(new GetAllCurrencies());

            var dtos = _mapper.Map<IEnumerable<CurrencyDTO>>(entities);

            return dtos;
        }

        public async Task<CurrencyDTO> GetByIdAsync(Guid entityId)
        {
            var entity = await _mdr.Send(new GetCurrencyById()
            {
                EntityId = entityId
            });

            if (entity == null)
            {
                throw new Exception("Currency not found!");
            }

            var dto = _mapper.Map<CurrencyDTO>(entity);

            return dto;
        }

        public Task PatchAsync(CurrencyDTO changed)
        {
            throw new NotImplementedException();
        }
    }
}
