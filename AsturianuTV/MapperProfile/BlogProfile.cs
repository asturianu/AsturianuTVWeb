using AsturianuTV.Infrastructure.Data.Models;
using AsturianuTV.ViewModels.System.BlogViewModels;
using AutoMapper;

namespace AsturianuTV.MapperProfile
{
    public class BlogProfile : Profile
    {
        public BlogProfile()
        {
            CreateMap<CreateBlogViewModel, Blog>();
            CreateMap<UpdateBlogViewModel, Blog>();
        }
    }
}
