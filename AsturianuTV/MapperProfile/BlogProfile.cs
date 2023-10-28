using AsturianuTV.Dto;
using AsturianuTV.Infrastructure.Data.Models.Subscriptions;
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
            CreateMap<Blog, BlogDto>()
                .ForMember(dest => dest.Plans, opt => opt.Ignore());
        }
    }
}
