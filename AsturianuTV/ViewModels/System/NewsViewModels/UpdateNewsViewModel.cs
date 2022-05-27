using System;
using System.Collections.Generic;
using AsturianuTV.Infrastructure.Data.Models;

namespace AsturianuTV.ViewModels.System.NewsViewModels
{
    public class UpdateNewsViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsBlog { get; set; }
        public bool IsReady { get; set; }
        public ICollection<Tag> Tags { get; set; }
        public ICollection<Material> Materials { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }
}
