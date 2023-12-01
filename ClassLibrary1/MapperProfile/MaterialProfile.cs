using AsturianuTV.Dto;
using AsturianuTV.Infrastructure.Data.Models;
using AsturianuTV.ViewModels.System.MaterialViewModels;
using AutoMapper;

namespace AsturianuTV.MapperProfile
{
    public class MaterialProfile : Profile
    {
        public MaterialProfile()
        {
            CreateMap<CreateMaterialViewModel, Material>();
        }
    }
}
