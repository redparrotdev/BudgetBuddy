using AutoMapper;
using BB.Finances.Data.DTO;
using BB.Finances.Data.Entities;
using BB.Finances.WebAPI.Models.Request;
using BB.Finances.WebAPI.Models.Response;

namespace BB.Finances.WebAPI.Config.MappingProfiles
{
    public class ExpenseMappingProfile : Profile
    {
        public ExpenseMappingProfile()
        {
            CreateMap<Expense, ExpenseDTO>()
                .ForMember(dto => dto.Id, opt => opt.MapFrom(e => e.Id))
                .ForMember(dto => dto.UserId, opt => opt.MapFrom(e => e.UserId))
                .ForMember(dto => dto.ValueOut, opt => opt.MapFrom(e => e.ValueOut))
                .ForMember(dto => dto.ValueIn, opt => opt.MapFrom(e => e.ValueIn))
                .ForMember(dto => dto.Description, opt => opt.MapFrom(e => e.Description))
                .ForMember(dto => dto.Date, opt => opt.MapFrom(e => e.Date))
                .ForMember(dto => dto.AccountId, opt => opt.MapFrom(e => e.AccountId))
                .ForMember(dto => dto.CategoryId, opt => opt.MapFrom(e => e.CategoryId));

            CreateMap<ExpenseDTO, Expense>()
                .ForMember(e => e.Id, opt => opt.MapFrom(dto => dto.Id))
                .ForMember(e => e.UserId, opt => opt.MapFrom(dto => dto.UserId))
                .ForMember(e => e.ValueOut, opt => opt.MapFrom(dto => dto.ValueOut))
                .ForMember(e => e.ValueIn, opt => opt.MapFrom(dto => dto.ValueIn))
                .ForMember(e => e.Description, opt => opt.MapFrom(dto => dto.Description))
                .ForMember(e => e.Date, opt => opt.MapFrom(dto => dto.Date))
                .ForMember(e => e.AccountId, opt => opt.MapFrom(dto => dto.AccountId))
                .ForMember(e => e.CategoryId, opt => opt.MapFrom(dto => dto.CategoryId));

            CreateMap<ExpenseDTO, ExpenseResponseModel>()
                .ForMember(rm => rm.Id, opt => opt.MapFrom(dto => dto.Id))
                .ForMember(rm => rm.UserId, opt => opt.MapFrom(dto => dto.UserId))
                .ForMember(rm => rm.ValueOut, opt => opt.MapFrom(dto => dto.ValueOut))
                .ForMember(rm => rm.ValueIn, opt => opt.MapFrom(dto => dto.ValueIn))
                .ForMember(rm => rm.Description, opt => opt.MapFrom(dto => dto.Description))
                .ForMember(rm => rm.Date, opt => opt.MapFrom(dto => dto.Date))
                .ForMember(rm => rm.AccountId, opt => opt.MapFrom(dto => dto.AccountId))
                .ForMember(rm => rm.CategoryId, opt => opt.MapFrom(dto => dto.CategoryId));

            CreateMap<ExpenseRequestModel, ExpenseDTO>()
                .ForMember(dto => dto.Id, opt => Guid.NewGuid())
                .ForMember(dto => dto.UserId, opt => opt.MapFrom(rm => rm.UserId))
                .ForMember(dto => dto.ValueOut, opt => opt.MapFrom(rm => rm.ValueOut))
                .ForMember(dto => dto.ValueIn, opt => opt.MapFrom(rm => rm.ValueIn))
                .ForMember(dto => dto.Description, opt => opt.MapFrom(rm => rm.Description))
                .ForMember(dto => dto.AccountId, opt => opt.MapFrom(rm => rm.AccountId))
                .ForMember(dto => dto.CategoryId, opt => opt.MapFrom(rm => rm.CategoryId));
        }
    }
}
