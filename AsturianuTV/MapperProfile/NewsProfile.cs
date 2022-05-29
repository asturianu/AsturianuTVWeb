using AsturianuTV.Dto;
using AsturianuTV.Infrastructure.Data.Models;
using AsturianuTV.ViewModels.System.NewsViewModels;
using AutoMapper;

namespace AsturianuTV.MapperProfile
{
    public class NewsProfile : Profile
    {
        public NewsProfile()
        {;
            CreateMap<CreateNewsViewModel, News>();
            CreateMap<UpdateNewsViewModel, News>();
            CreateMap<News, NewsDto>()
                .ForMember(dest => dest.Tags, opt => opt.Ignore())
                .ForMember(dest => dest.Materials, opt => opt.Ignore());
        }
    }
}
