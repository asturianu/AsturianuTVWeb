using AsturianuTV.Dto;
using AsturianuTV.ViewModels.System.SkillViewModels;
using AutoMapper;
using AsturianuTV.Infrastructure.Data.Models.BaseKnowledges;

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
