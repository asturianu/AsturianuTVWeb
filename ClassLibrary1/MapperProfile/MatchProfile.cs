using AsturianuTV.Infrastructure.Data.Models.Cybersports;
using AsturianuTV.ViewModels.System.MatchViewModels;
using AutoMapper;

namespace AsturianuTV.MapperProfile
{
    public class MatchProfile : Profile
    {
        public MatchProfile()
        {
            CreateMap<CreateMatchViewModel, Match>();
            CreateMap<UpdateMatchViewModel, Match>();

            CreateMap<CreateMatchStatsViewModel, PlayerMatchStats>();
            CreateMap<UpdateMatchStatsViewModel, PlayerMatchStats>();
        }
    }
}
