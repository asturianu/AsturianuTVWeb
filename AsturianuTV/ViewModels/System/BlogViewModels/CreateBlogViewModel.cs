﻿using System.Collections.Generic;
using AsturianuTV.Infrastructure.Data.Models;

namespace AsturianuTV.ViewModels.System.BlogViewModels
{
    public class CreateBlogViewModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public ICollection<int?> Materials { get; set; }
        public int PlanId { get; set; }
    }
}
