using System.Security.Cryptography.X509Certificates;
using AsturianuTV.Dto;
using AsturianuTV.Infrastructure.Data.Models;
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
