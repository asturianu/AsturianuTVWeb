using System.Collections.Generic;
using AsturianuTV.Infrastructure.Data.Models;
using AsturianuTV.Infrastructure.Data.Models.ContentNews;

namespace AsturianuTV.ViewModels.System.TagViewModels
{
    public class UpdateTagViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<NewsTag> NewsTags { get; set; }
    }
}
