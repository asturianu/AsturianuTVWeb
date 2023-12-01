using System;
using System.Collections.Generic;

namespace AsturianuTV.ViewModels.System.NewsViewModels
{
    public class CreateNewsViewModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsReady { get; set; }
        public int? LeagueId { get; set; }
        public int? PlayerId { get; set; }
        public int? TeamId { get; set; }
        public ICollection<int> Tags { get; set; }
        public ICollection<int> Materials { get; set; }
    }
}
