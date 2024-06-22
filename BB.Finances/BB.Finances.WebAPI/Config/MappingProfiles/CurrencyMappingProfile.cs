using AutoMapper;
using BB.Finances.Data.DTO;
using BB.Finances.Data.Entities;

namespace BB.Finances.WebAPI.Config.MappingProfiles
{
    public class CurrencyMappingProfile : Profile
    {
        public CurrencyMappingProfile()
        {
            CreateMap<Currency, CurrencyDTO>()
                .ForMember(dto => dto.Id, opt => opt.MapFrom(c => c.Id))
                .ForMember(dto => dto.Name, opt => opt.MapFrom(c => c.Name))
                .ForMember(dto => dto.CurrencySign, opt => opt.MapFrom(c => c.CurrencySign));

            CreateMap<CurrencyDTO, CurrencyDTO>()
                .ForMember(c => c.Id, opt => opt.MapFrom(dto => dto.Id))
                .ForMember(c => c.Name, opt => opt.MapFrom(dto => dto.Name))
                .ForMember(c => c.CurrencySign, opt => opt.MapFrom(dto => dto.CurrencySign));
        }
    }
}
