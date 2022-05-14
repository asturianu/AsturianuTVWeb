using System.Collections.Generic;

namespace AsturianuTV.Infrastructure.Data.Models
{
    public class Tag : BaseEntity
    {
        public string Name { get; set; }
        public ICollection<NewsTag> NewsTags { get; set; }
    }
}
