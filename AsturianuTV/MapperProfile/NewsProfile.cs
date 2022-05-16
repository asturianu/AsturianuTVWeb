using AsturianuTV.Infrastructure.Data.Models;
using AsturianuTV.ViewModels.System.NewsViewModels;
using AutoMapper;

namespace AsturianuTV.MapperProfile
{
    public class NewsProfile : Profile
    {
        public NewsProfile()
        {
            CreateMap<CreateNewsViewModel, News>();
            CreateMap<UpdateNewsViewModel, News>();
        }
    }
}
