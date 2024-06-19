using AutoMapper;
using BB.Finances.Data.DTO;
using BB.Finances.Data.Entities;

namespace BB.Finances.WebAPI.Config.MappingProfiles
{
    public class CategoryMappingProfile : Profile
    {
        public CategoryMappingProfile()
        {
            CreateMap<Category, CategoryDTO>()
                .ForMember(dto => dto.Id, opt => opt.MapFrom(e => e.Id))
                .ForMember(dto => dto.UserId, opt => opt.MapFrom(e => e.UserId))
                .ForMember(dto => dto.Name, opt => opt.MapFrom(e => e.Name))
                .ForMember(dto => dto.Description, opt => opt.MapFrom(e => e.Description))
                .ForMember(dto => dto.CreationDate, opt => opt.MapFrom(e => e.CreationDate))
                .ForMember(dto => dto.CurrencyId, opt => opt.MapFrom(e => e.CurrencyId))
                .ForMember(dto => dto.CurrencySign, opt => opt.MapFrom(e => e.Currency.CurrencySign))
                .ForMember(dto => dto.IsDeleted, opt => opt.MapFrom(e => e.IsDeleted));

            CreateMap<CategoryDTO, Category>()
                .ForMember(e => e.Id, opt => opt.MapFrom(dto => dto.Id))
                .ForMember(e => e.UserId, opt => opt.MapFrom(dto => dto.UserId))
                .ForMember(e => e.Name, opt => opt.MapFrom(dto => dto.Name))
                .ForMember(e => e.Description, opt => opt.MapFrom(dto => dto.Description))
                .ForMember(e => e.CreationDate, opt => opt.MapFrom(dto => dto.CreationDate))
                .ForMember(e => e.CurrencyId, opt => opt.MapFrom(dto => dto.CurrencyId))
                .ForMember(e => e.IsDeleted, opt => opt.MapFrom(dto => dto.IsDeleted));
        }
    }
}
