using AsturianuTV.Infrastructure.Data.Models;
using AsturianuTV.ViewModels.System.TagViewModels;
using AutoMapper;

namespace AsturianuTV.MapperProfile
{
    public class TagProfile : Profile
    {
        public TagProfile()
        {
            CreateMap<CreateTagViewModel, Tag>();
            CreateMap<UpdateTagViewModel, Tag>();
        }
    }
}
