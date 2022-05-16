﻿using System.Collections.Generic;
using AsturianuTV.Infrastructure.Data.Models;

namespace AsturianuTV.ViewModels.System.TagViewModels
{
    public class CreateTagViewModel
    {
        public string Name { get; set; }
        public ICollection<NewsTag> NewsTags { get; set; }
    }
}