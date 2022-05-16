﻿using AsturianuTV.Infrastructure.Data.Models;
using AsturianuTV.ViewModels.System.ItemViewModels;
using AutoMapper;

namespace AsturianuTV.MapperProfile
{
    public class ItemProfile : Profile
    {
        public ItemProfile()
        {
            CreateMap<CreateItemViewModel, Item>();
            CreateMap<UpdateItemViewModel, Item>();
        }
    }
}
