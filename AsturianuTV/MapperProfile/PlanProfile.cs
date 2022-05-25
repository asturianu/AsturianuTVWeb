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
        }
    }
}
