using AutoMapper;
using BB.Finances.Data.Entities;
using BB.Finances.WebAPI.Extentions;
using BB.Finances.WebAPI.Models.Request;
using BB.Finances.WebAPI.Models.Response;

namespace BB.Finances.WebAPI.Config.MappingProfiles
{
    public class AccountMappingProfile : Profile
    {
        public AccountMappingProfile()
        {
            CreateMap<Account, AccountResponseModel>()
                .ForMember(rm => rm.Id, opt => opt.MapFrom(a => a.Id))
                .ForMember(rm => rm.UserId, opt => opt.MapFrom(a => a.UserId))
                .ForMember(rm => rm.Name, opt => opt.MapFrom(a => a.Name))
                .ForMember(rm => rm.Balance, opt => opt.MapFrom(a => a.Balance))
                .ForMember(rm => rm.CreationDate, opt => opt.MapFrom(a => a.CreationDate))
                .ForMember(rm => rm.Currency, opt => opt.MapFrom(a => a.Currency.CurrencyToString()));

            CreateMap<AccountRequestModel, Account>()
                .ForMember(a => a.UserId, opt => opt.MapFrom(rm => rm.UserId))
                .ForMember(a => a.Name, opt => opt.MapFrom(rm => rm.Name))
                .ForMember(a => a.Balance, opt => opt.MapFrom(rm => rm.Balance))
                .ForMember(a => a.Currency, opt => opt.MapFrom(rm => rm.Currency.CurrencyStringToCurrencyEnum()));
        }
    }
}
