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
    public class CategoryService : ICategoryService
    {
        private readonly IMediator _mdr;
        private readonly IMapper _mapper;

        public CategoryService(IMediator mdr, IMapper mapper)
        {
            _mdr = mdr;
            _mapper = mapper;
        }

        public async Task<Guid> CreateNewAsync(CategoryDTO dto)
        {
            var entity = _mapper.Map<Category>(dto);

            var result = await _mdr.Send(new CreateCategory()
            {
                Entity = entity
            });

            if (result != 1)
            {
                throw new Exception("Category creation went wrong!");
            }

            return entity.Id;
        }

        public Task DeleteAsync(Guid entityId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<CategoryDTO>> GetAllByUserIdAsync(Guid userId)
        {
            var entities = await _mdr.Send(new GetCategoriesByUserId()
            {
                UserId = userId
            });

            var dtos = _mapper.Map<IEnumerable<CategoryDTO>>(entities);

            return dtos;
        }

        public async Task<CategoryDTO> GetByIdAsync(Guid entityId)
        {
            var entity = await _mdr.Send(new GetCategoryById()
            {
                EntityId = entityId
            });

            if (entity == null)
            {
                throw new Exception("Entity not found");
            }

            if (entity.IsDeleted)
            {
                throw new Exception("entity is deleted");
            }

            var result = _mapper.Map<CategoryDTO>(entity);

            return result;
        }

        public Task PatchAsync(CategoryDTO changed)
        {
            throw new NotImplementedException();
        }
    }
}
