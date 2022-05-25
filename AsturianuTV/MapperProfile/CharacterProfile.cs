using AsturianuTV.Infrastructure.Data.Models;
using AsturianuTV.ViewModels.System.CharacterViewModels;
using AutoMapper;

namespace AsturianuTV.MapperProfile
{
    public class CharacterProfile : Profile
    {
        public CharacterProfile()
        {
            CreateMap<CreateBlogViewModel, Character>();
            CreateMap<UpdateMaterialViewModel, Character>();
        }
    }
}
