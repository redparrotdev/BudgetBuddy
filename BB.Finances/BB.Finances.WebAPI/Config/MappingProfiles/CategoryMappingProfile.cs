using AutoMapper;
using BB.Finances.Data.Entities;
using BB.Finances.WebAPI.Extentions;
using BB.Finances.WebAPI.Models.Request;
using BB.Finances.WebAPI.Models.Response;

namespace BB.Finances.WebAPI.Config.MappingProfiles
{
    public class CategoryMappingProfile : Profile
    {
        public CategoryMappingProfile()
        {
            CreateMap<Category, CategoryResponseModel>()
                .ForMember(rm => rm.Id, opt => opt.MapFrom(c => c.Id))
                .ForMember(rm => rm.UserId, opt => opt.MapFrom(c => c.UserId))
                .ForMember(rm => rm.Name, opt => opt.MapFrom(c => c.Name))
                .ForMember(rm => rm.Description, opt => opt.MapFrom(c => c.Description))
                .ForMember(rm => rm.CreationDate, opt => opt.MapFrom(c => c.CreationDate))
                .ForMember(rm => rm.Currency, opt => opt.MapFrom(c => c.Currency.CurrencyToString()));

            CreateMap<CategoryRequestModel, Category>()
                .ForMember(c => c.UserId, opt => opt.MapFrom(rm => rm.UserId))
                .ForMember(c => c.Name, opt => opt.MapFrom(rm => rm.Name))
                .ForMember(c => c.Description, opt => opt.MapFrom(rm => rm.Description))
                .ForMember(c => c.Currency, opt => opt.MapFrom(rm => rm.Currency.CurrencyStringToCurrencyEnum()));
        }
    }
}
