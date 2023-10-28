using AsturianuTV.Dto;
using AsturianuTV.Infrastructure.Data.Models.Subscriptions;
using AsturianuTV.ViewModels.System.PlanViewModels;
using AutoMapper;

namespace AsturianuTV.MapperProfile
{
    public class PlanProfile : Profile
    {
        public PlanProfile()
        {
            CreateMap<CreatePlanViewModel, Plan>();
            CreateMap<UpdatePlanViewModel, Plan>();
            CreateMap<Plan, PlanDto>()
                .ForMember(dest => dest.Subscriptions, opt => opt.Ignore());
        }
    }
}
