using AutoMapper;
using BB.Finances.Data.Entities;
using BB.Finances.WebAPI.Models.Request;
using BB.Finances.WebAPI.Models.Response;

namespace BB.Finances.WebAPI.Config.MappingProfiles
{
    public class ExpenseMappingProfile : Profile
    {
        public ExpenseMappingProfile()
        {
            CreateMap<Expense, ExpenseResponseModel>()
                .ForMember(rm => rm.Id, opt => opt.MapFrom(e => e.Id))
                .ForMember(rm => rm.UserId, opt => opt.MapFrom(e => e.UserId))
                .ForMember(rm => rm.ValueOut, opt => opt.MapFrom(e => e.ValueIn))
                .ForMember(rm => rm.Description, opt => opt.MapFrom(e => e.Description))
                .ForMember(rm => rm.Date, opt => opt.MapFrom(e => e.Date))
                .ForMember(rm => rm.AccountId, opt => opt.MapFrom(e => e.AccountId))
                .ForMember(rm => rm.CategoryId, opt => opt.MapFrom(e => e.CategoryId));

            CreateMap<ExpenseRequestModel, Expense>()
                .ForMember(e => e.UserId, opt => opt.MapFrom(rm => rm.UserId))
                .ForMember(e => e.ValueOut, opt => opt.MapFrom(rm => rm.ValueOut))
                .ForMember(e => e.ValueIn, opt => opt.MapFrom(rm => rm.ValueIn))
                .ForMember(e => e.Description, opt => opt.MapFrom(rm => rm.Description))
                .ForMember(e => e.AccountId, opt => opt.MapFrom(rm => rm.AccountId))
                .ForMember(e => e.CategoryId, opt => opt.MapFrom(rm => rm.CategoryId));
        }
    }
}
