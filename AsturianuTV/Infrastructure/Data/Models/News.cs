using System;
using System.Collections.Generic;

namespace AsturianuTV.Infrastructure.Data.Models
{
    public class News : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? LastModifiedAt { get; set; }
        public int? LastModifiedBy { get; set; }
        public DateTime? CreateAt { get; set; }
        public int? CreatedBy { get; set; }
        public bool IsReady { get; set; }
        public ICollection<NewsTag> NewsTags { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }
}
