using AsturianuTV.Infrastructure.Data.Models.Cybersports;
using AsturianuTV.ViewModels.System.LeagueViewModels;
using AutoMapper;

namespace AsturianuTV.MapperProfile
{
    public class LeagueProfile : Profile
    {
        public LeagueProfile()
        {
            CreateMap<CreateLeagueViewModel, League>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.News, opt => opt.Ignore())
                .ForMember(dest => dest.LeagueTeams, opt => opt.Ignore());

            CreateMap<UpdateLeagueViewModel, League>()
                .ForMember(dest => dest.News, opt => opt.Ignore())
                .ForMember(dest => dest.LeagueTeams, opt => opt.Ignore());

            CreateMap<League, UpdateLeagueViewModel>();
        }
    }
}
