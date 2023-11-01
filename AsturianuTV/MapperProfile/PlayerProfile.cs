using AsturianuTV.Infrastructure.Data.Models.Cybersports;
using AsturianuTV.ViewModels.System.PlayerViewModels;
using AutoMapper;

namespace AsturianuTV.MapperProfile
{
    public class PlayerProfile : Profile
    {
        public PlayerProfile()
        {
            CreateMap<CreatePlayerViewModel, Team>();
            CreateMap<UpdatePlayerViewModel, Team>();
        }
    }
}
