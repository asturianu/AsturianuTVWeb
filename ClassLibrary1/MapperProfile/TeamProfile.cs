using AsturianuTV.Infrastructure.Data.Models.Cybersports;
using AsturianuTV.ViewModels.System.TeamViewModels;
using AutoMapper;

namespace AsturianuTV.MapperProfile
{
    public class TeamProfile : Profile
    {
        public TeamProfile()
        {
            CreateMap<CreateTeamViewModel, Team>();
            CreateMap<UpdateTeamViewModel, Team>();
        }
    }
}
