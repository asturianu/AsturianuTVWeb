using AsturianuTV.Infrastructure.Data.Models.Cybersports;
using System.Collections.Generic;

namespace AsturianuTV.Infrastructure.Data.Models.ContentNews
{
    public class News : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsReady { get; set; }
        public int? LeagueId { get; set; }
        public League League { get; set; }
        public int? TeamId { get; set; }
        public Team Team { get; set; }
        public int? PlayerId { get; set; }
        public Player Player { get; set; }
        public ICollection<NewsTag> NewsTags { get; set; }
        public ICollection<NewsMaterial> NewsMaterials { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }
}
