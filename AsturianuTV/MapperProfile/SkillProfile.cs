using AsturianuTV.Dto;
using AsturianuTV.ViewModels.System.SkillViewModels;
using AsturianuTV.Infrastructure.Data.Models;
using AutoMapper;


namespace AsturianuTV.MapperProfile
{
    public class SkillProfile : Profile
    {
        public SkillProfile()
        {
            CreateMap<CreateSkillViewModel, Skill>();
            CreateMap<UpdateSkillViewModel, Skill>();
            CreateMap<Skill, SkillDto>()
                .ForMember(dest => dest.Characters, opt => opt.Ignore());
        }
    }
}
